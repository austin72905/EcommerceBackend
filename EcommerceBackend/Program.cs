using Application;
using Application.Interfaces;
using Application.Services;
using Common.Interfaces.Application.Services;
using DataSource.DBContext;
using DataSource.Repositories;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Services;
using EcommerceBackend.MiddleWares;

using Infrastructure.Cache;
using Infrastructure.Http;
using Infrastructure.Interfaces;
using Infrastructure.MQ;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);


// 添加自定義文件加載
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
        .LogTo(Console.WriteLine, LogLevel.Information)
        .EnableSensitiveDataLogging()  // 不要在production env使用
        .EnableDetailedErrors()   // 不要在production env使用
    );

//注入 IHttpContextAccessor
builder.Services.AddHttpContextAccessor();

// httpclient
builder.Services.AddHttpClient<IHttpUtils, HttpUtils>(); //依賴注入的範圍（Scoped），每次請求都會提供一個新的 HttpClient 實例，但共用其內部的 HttpMessageHandler，有效管理了資源。

// Add services to the container.
// app service
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IShipmentService, ShipmentService>();

// domain servie
builder.Services.AddScoped<IOrderDomainService, OrderDomainService>();
builder.Services.AddScoped<IUserDomainService, UserDomainService>();
builder.Services.AddScoped<ICartDomainService, CartDomainService>();


// repositories
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>(); 
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<IOrderRepostory, OrderRepostory>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IShipmentRepository, ShipmentRepository>();


// singleton services

builder.Services.AddSingleton<IShipmentProducer, ShipmentProducer>();
builder.Services.AddSingleton<IShipmentConsumer, ShipmentConsumer>();

// 模擬物流訂單
builder.Services.AddSingleton<IQueueProcessor, QueueProcessor>();

// host service
builder.Services.AddHostedService<ShipmentConsumerService>();


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

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();



// 資料庫初始化邏輯
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<EcommerceDBContext>();

    try
    {
        // 應用遷移
        dbContext.Database.Migrate();
        Console.WriteLine("Database migration completed successfully.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred while migrating the database: {ex.Message}");
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

// 驗證是否登陸
app.UseMiddleware<AuthenticationMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
