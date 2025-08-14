using System;
using System.IO;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Share.Services.Implementation;
using Share.Services.Interface;
using StackExchange.Redis;
using WebApi.BLL.Services.Implementation.Auth;
using WebApi.BLL.Services.Implementation.Cache;
using WebApi.BLL.Services.Implementation.Stands;
using WebApi.BLL.Services.Implementation.Users;
using WebApi.BLL.Services.Interfaces.Auth;
using WebApi.BLL.Services.Interfaces.Cache;
using WebApi.BLL.Services.Interfaces.Stands;
using WebApi.BLL.Services.Interfaces.Users;
using WebApi.DAL.Providers.Implementation;
using WebApi.DAL.Providers.Interface;
using WebApi.Mappings;
using WebApi.Middlewares;

namespace WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var hosts = new[]
            { "http://localhost:3000", "http://localhost:3001", "http://frontend:3000", "http://localhost:8081", "http://frontend:8081" };
        builder.Services.AddCors(options =>  
        {  
            options.AddDefaultPolicy(
                policy  =>
                {
                    policy
                        .WithOrigins(hosts)
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                });  
        });

        ConfigureService(builder);
        
        builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "AuthService",
                    ValidAudience = "WebAPI",
                    IssuerSigningKey = new SymmetricSecurityKey("SuperStrongAndLongJwtSecretKey_123456!"u8.ToArray())
                };
            });

        builder.Services.AddAuthorization();
        builder.Services.AddControllers();
        
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi", Version = "v1" });
            var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);
        });
        
        builder.Services.AddHttpClient(); 

        var app = builder.Build();

        app.UseStaticFiles();
        app.UseRouting();
        
        app.UseCors();

        app.UseSwagger();
        app.UseSwaggerUI();

        app.Use(async (context, next) =>
        {
            Console.WriteLine("Authorization header: " + context.Request.Headers["Authorization"]);
            await next();
        });
        
        app.UseMiddleware<JwtRefreshMiddleware>(); 
        app.UseAuthentication();
        app.UseAuthorization();

        app.Use(async (context, next) =>
        {
            await next();

            if (context.Request.Path == "/swagger/v1/swagger.json")
            {
                context.Response.Body.Position = 0;
                using var reader = new StreamReader(context.Response.Body);
                var swaggerJson = await reader.ReadToEndAsync();
                await File.WriteAllTextAsync("swagger.json", swaggerJson);
            }
        });

        app.MapControllers();
        
        app.Run();
    }

    private static void ConfigureService(WebApplicationBuilder builder)
    {
        var authServiceUrl = Environment.GetEnvironmentVariable("AUTH_SERVICE_URL") ?? "http://localhost:5000";
        var dataManagerServiceUrl = Environment.GetEnvironmentVariable("DATA_MANAGER_SERVICE_URL") ?? "http://localhost:8003"; //TODO: поменять на нужный
        var redisConnectionString = Environment.GetEnvironmentVariable("REDIS_CONNECTION_STRING") ?? "localhost:6379,password=password,abortConnect=false";

        builder.Services.AddScoped(_ =>  new HttpClient());
        builder.Services.AddScoped<ILogger, ConsoleLogger>();
        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IStandService, StandService>();
        builder.Services.AddSingleton<IRefreshStoreService, RedisRefreshStoreServiceService>();
        builder.Services.AddSingleton<IConnectionMultiplexer>(_ =>
        {
            var options = ConfigurationOptions.Parse(redisConnectionString);
            options.AbortOnConnectFail = false;
            return ConnectionMultiplexer.Connect(options);
        });
        
        
        builder.Services.AddScoped<IAuthProvider>(p => new AuthProvider(authServiceUrl, p.GetRequiredService<HttpClient>()));
        builder.Services.AddScoped<IUserProvider>(p => new UserProvider(authServiceUrl, p.GetRequiredService<HttpClient>()));
        builder.Services.AddScoped<IStandProvider>(p => new StandProvider(dataManagerServiceUrl, p.GetRequiredService<HttpClient>()));
        
        builder.Services.AddAutoMapper(
            typeof(MappingProfile).Assembly,
            typeof(WebApi.BLL.Mappings.MappingProfile).Assembly
        );
    }
}