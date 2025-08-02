using Application;
using Application.Interfaces;
using Application.Services;
using Common.Interfaces.Application.Services;
using DataSource.DBContext;
using DataSource.Repositories;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Services;
using EcommerceBackend.Filter;
using EcommerceBackend.MiddleWares;

using Infrastructure.Cache;
using Infrastructure.Http;
using Infrastructure.Services;
using Common.Interfaces.Infrastructure;
using Infrastructure.MQ;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog;
using StackExchange.Redis;
using System.Collections.Generic;

var builder = WebApplication.CreateBuilder(args);


// 載入組態設定
builder.Configuration
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

string redisConnectionString = Environment.GetEnvironmentVariable("RedisConnString");

if (string.IsNullOrEmpty(redisConnectionString))
{
    throw new ArgumentNullException("RedisConnString", "Environment variable RedisConnString is not set or is empty.");
}

builder.Services.AddDbContext<EcommerceDBContext>(options =>
    options.UseSqlite("Data Source=Ecommerce_Shop.db")
        //.LogTo(Console.WriteLine, LogLevel.Information) // 開啟詳細日誌
        .LogTo(Console.WriteLine, LogLevel.Error)  // 只顯示錯誤日誌
        //.EnableSensitiveDataLogging()  // 僅建議於 production 環境使用
        //.EnableDetailedErrors()   // 僅建議於 production 環境使用
    );

// 註冊 IHttpContextAccessor
builder.Services.AddHttpContextAccessor();

// 註冊 IHttpUtils 為 HttpClient，並設定為 Scoped
builder.Services.AddHttpClient<IHttpUtils, HttpUtils>();

// 應用服務註冊
// app service
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IShipmentService, ShipmentService>();
builder.Services.AddScoped<IOrderTimeoutHandler, OrderTimeoutHandler>();

// 網域服務註冊
// domain servie
builder.Services.AddScoped<IOrderDomainService, OrderDomainService>();
builder.Services.AddScoped<IUserDomainService, UserDomainService>();
builder.Services.AddScoped<ICartDomainService, CartDomainService>();

// 資料庫儲存庫註冊
// repositories
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>(); 
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<IOrderRepostory, OrderRepostory>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IShipmentRepository, ShipmentRepository>();

// 單例服務註冊
// singleton services  
builder.Services.AddSingleton<IShipmentProducer, ShipmentProducer>();
builder.Services.AddSingleton<IShipmentConsumer, ShipmentConsumer>();
builder.Services.AddSingleton<IOrderTimeoutProducer, OrderTimeoutProducer>();

// 消費者服務改為 Scoped，因為它需要在每次消費時重新創建，並且依賴其他 Scoped 服務
builder.Services.AddScoped<IOrderTimeoutConsumer, OrderTimeoutConsumer>();
builder.Services.AddScoped<IEncryptionService, EncryptionService>();

// 隊列處理器註冊
builder.Services.AddSingleton<IQueueProcessor, QueueProcessor>();

// 註冊背景服務
builder.Services.AddHostedService<ShipmentConsumerService>();
builder.Services.AddHostedService<OrderTimeoutConsumerService>();


builder.Services.AddSingleton<IConnectionMultiplexer>(sp=> 
{
    try
    {
        return ConnectionMultiplexer.Connect(redisConnectionString);
    }
    catch (RedisConnectionException ex)
    {
        Console.WriteLine($"Could not connect to Redis: {ex.Message}");
        throw;
    }
    //return ConnectionMultiplexer.Connect(Environment.GetEnvironmentVariable("RedisConnString"));
} );
builder.Services.AddSingleton<IRedisService,RedisService>();

builder.Services.AddControllers(options =>
    options.Filters.Add<LogRequestResponseFilter>()
);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 使用 serilog
builder.Host.UseSerilog((context, services, configuration) => 
    configuration
        .ReadFrom.Configuration(context.Configuration)
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
);

var app = builder.Build();



// 應用啟動時自動遷移資料庫
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<EcommerceDBContext>();

    try
    {
        // 自動遷移
        dbContext.Database.Migrate();
        Console.WriteLine("Database migration completed successfully.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred while migrating the database: {ex.Message}");
        throw;
    }
}

// 啟動後，查詢資料庫，將商品庫存存入redis
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<EcommerceDBContext>();
    var redisService = scope.ServiceProvider.GetRequiredService<IRedisService>();

    try
    {
        // 查詢所有商品變體的庫存
        var productVariants = await dbContext.ProductVariants
            .AsNoTracking()
            .Select(pv => new { pv.Id, pv.Stock })
            .ToListAsync();

        // 準備庫存資料字典 (key:variantId , value : stock)
        var stockData = new Dictionary<string, int>();
        foreach (var variant in productVariants)
        {
            var key = variant.Id.ToString();
            stockData[key] = variant.Stock;
        }

        // 批量存入Redis (將所有商品的庫存存入redis)
        await redisService.SetProductStocksAsync(stockData);

        Console.WriteLine($"Successfully loaded {productVariants.Count} product variants stock to Redis.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred while querying stock: {ex.Message}");
        throw;
    }
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



//啟用Cors
app.UseCors(builder=>builder.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod().AllowCredentials());

// 啟用認證中介軟體
app.UseMiddleware<AuthenticationMiddleware>();
// 啟用日誌中介軟體
app.UseMiddleware<LoggingMiddleware>();

// 移除 HTTPS 重導向
//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
