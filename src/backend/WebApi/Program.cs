using System;
using System.IO;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Share.Services.Implementation;
using Share.Services.Interface;
using WebApi.BLL.Services.Implementation.Auth;
using WebApi.BLL.Services.Implementation.Users;
using WebApi.BLL.Services.Interface.Auth;
using WebApi.BLL.Services.Interface.Users;
using WebApi.DAL.Providers.Implementation;
using WebApi.DAL.Providers.Interface;
using WebApi.Mappings;

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
                        .AllowAnyHeader();
                });  
        });

        ConfigureService(builder.Services);
        
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
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("itcamp_secretkey"))
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

        // Configure the HTTP request pipeline.
        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseAuthentication();
        app.UseAuthorization();

        app.Use(async (context, next) =>
        {
            await next();

            if (context.Request.Path == "/swagger/v1/swagger.json")
            {
                context.Response.Body.Position = 0; // Важно: сбросить позицию потока перед чтением
                using var reader = new StreamReader(context.Response.Body);
                var swaggerJson = await reader.ReadToEndAsync();
                await File.WriteAllTextAsync("swagger.json", swaggerJson);
            }
        });

        app.MapControllers();
        
        app.Run();
    }

    private static void ConfigureService(IServiceCollection services)
    {
        var authServiceUrl = Environment.GetEnvironmentVariable("AUTH_SERVICE_URL") ?? "http://localhost:5000";

        services.AddScoped(_ =>  new HttpClient());
        services.AddScoped<ILogger, ConsoleLogger>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();
        
        services.AddScoped<IAuthProvider>(p => new AuthProvider(authServiceUrl, p.GetRequiredService<HttpClient>()));
        services.AddScoped<IUserProvider>(p => new UserProvider(authServiceUrl, p.GetRequiredService<HttpClient>()));
        
        services.AddAutoMapper(
            typeof(MappingProfile).Assembly,
            typeof(WebApi.BLL.Mappings.MappingProfile).Assembly
        );
    }
}