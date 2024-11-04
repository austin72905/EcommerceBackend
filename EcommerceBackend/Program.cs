using Application.Interfaces;
using Application.Services;
using DataSource.DBContext;
using DataSource.Repositories;
using Domain.Interfaces.Repositories;
using EcommerceBackend.MiddleWares;

using Infrastructure.Cache;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

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

// Add services to the container.
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>(); 
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<IOrderRepostory, OrderRepostory>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();

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
