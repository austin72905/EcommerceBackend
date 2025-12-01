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
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Npgsql;
using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
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

// 構建連接字符串，添加連接池和超時配置
var baseConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var connectionStringBuilder = new NpgsqlConnectionStringBuilder(baseConnectionString)
{
    // 連接池配置
    Pooling = true,
    MinPoolSize = 5,
    MaxPoolSize = 100,
    // 連接超時時間（秒）
    Timeout = 15,
    // 命令超時時間（秒）- 通過連接字符串參數設置
    CommandTimeout = 30
};

builder.Services.AddDbContext<EcommerceDBContext>(options =>
    options.UseNpgsql(connectionStringBuilder.ConnectionString)
        //.LogTo(Console.WriteLine, LogLevel.Information) // 開啟詳細日誌
        .LogTo(Console.WriteLine, LogLevel.Error)  // 只顯示錯誤日誌
        //.EnableSensitiveDataLogging()  // 僅建議於 production 環境使用
        //.EnableDetailedErrors()   // 僅建議於 production 環境使用
    );

// 註冊 IHttpContextAccessor
builder.Services.AddHttpContextAccessor();

// 註冊 IHttpUtils 為 HttpClient，並設定為 Scoped
builder.Services.AddHttpClient<IHttpUtils, HttpUtils>();

// 配置 OpenTelemetry 分散式追蹤
builder.Services.AddOpenTelemetry()
    .ConfigureResource(resource => resource
        .AddService(
            serviceName: "EcommerceBackend",
            serviceVersion: "1.0.0")
        .AddAttributes(new Dictionary<string, object>
        {
            ["deployment.environment"] = builder.Environment.EnvironmentName
        }))
    .WithTracing(tracing => tracing
        // 自動追蹤 ASP.NET Core HTTP 請求
        .AddAspNetCoreInstrumentation(options =>
        {
            options.RecordException = true;
            options.EnrichWithHttpRequest = (activity, request) =>
            {
                activity.SetTag("http.request.method", request.Method);
                activity.SetTag("http.request.path", request.Path);
            };
        })
        // 自動追蹤 HttpClient 請求（包括對 payment service 的調用）
        .AddHttpClientInstrumentation(options =>
        {
            options.RecordException = true;
        })
        // 自動追蹤 Entity Framework Core 數據庫查詢
        .AddEntityFrameworkCoreInstrumentation(options =>
        {
            options.SetDbStatementForText = true;
            options.EnrichWithIDbCommand = (activity, command) =>
            {
                activity.SetTag("db.statement", command.CommandText);
            };
        })
        // 導出到 Jaeger（使用 OTLP gRPC）
        .AddOtlpExporter(options =>
        {
            // 從環境變數或配置讀取 Jaeger 端點，預設為 localhost:4317
            var jaegerEndpoint = builder.Configuration["AppSettings:JaegerEndpoint"] 
                ?? Environment.GetEnvironmentVariable("JAEGER_ENDPOINT") 
                ?? "http://localhost:4317";
            options.Endpoint = new Uri(jaegerEndpoint);
        }));

// 應用服務註冊
// app service
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IInventoryService, InventoryService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderStatusSyncService, OrderService>(); // OrderService 同時實現 IOrderStatusSyncService
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
builder.Services.AddSingleton<IOrderStateProducer, OrderStateProducer>();
builder.Services.AddSingleton<IOrderStatusChangedConsumer, OrderStatusChangedConsumer>();

// 消費者服務改為 Scoped，因為它需要在每次消費時重新創建，並且依賴其他 Scoped 服務
builder.Services.AddScoped<IOrderTimeoutConsumer, OrderTimeoutConsumer>();
builder.Services.AddScoped<IEncryptionService, EncryptionService>();

// 隊列處理器註冊
builder.Services.AddSingleton<IQueueProcessor, QueueProcessor>();

// 註冊背景服務
builder.Services.AddHostedService<ShipmentConsumerService>();
builder.Services.AddHostedService<OrderTimeoutConsumerService>();
builder.Services.AddHostedService<OrderStatusChangedConsumerService>();


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
// 註冊 Middleware
// 暫時關閉限流中間件，用於壓力測試
// app.UseMiddleware<RateLimitMiddleware>();

// 移除 HTTPS 重導向
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
