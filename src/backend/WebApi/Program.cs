using System;
using System.IO;
using System.Net.Http;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Share.Services.Implementation;
using Share.Services.Interface;
using StackExchange.Redis;
using WebApi.BLL.Services.Implementation.Cache;
using WebApi.BLL.Services.Implementation.Stands;
using WebApi.BLL.Services.Implementation.Users;
using WebApi.BLL.Services.Interfaces.Cache;
using WebApi.BLL.Services.Interfaces.Stands;
using WebApi.BLL.Services.Interfaces.Users;
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
        {
            "http://localhost:3000",
            "http://frontend:3000",
        };
        
        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy
                    .WithOrigins(hosts)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            });
        });

        ConfigureService(builder);

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                var keycloakHost = Environment.GetEnvironmentVariable("KEYCLOAK_HOST") ?? "localhost";
                var keycloakInternalPort = Environment.GetEnvironmentVariable("KEYCLOAK_INTERNAL_PORT") ?? "8080";
                var keycloakPort = Environment.GetEnvironmentVariable("KEYCLOAK_PORT") ?? "8090";
                var keycloakRealm = Environment.GetEnvironmentVariable("KEYCLOAK_REALM") ?? "preditrix";
                var keycloakPublicUrl = Environment.GetEnvironmentVariable("KEYCLOAK_PUBLIC_URL");

                Console.WriteLine(keycloakPublicUrl);
                Console.WriteLine($"http://{keycloakHost}:{keycloakInternalPort}/realms/{keycloakRealm}");
                var authority = keycloakPublicUrl ??
                                $"http://{keycloakHost}:{keycloakInternalPort}/realms/{keycloakRealm}";

                options.Authority = authority;
                options.RequireHttpsMetadata = false;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuers = new[]
                    {
                        $"http://localhost:{keycloakInternalPort}/realms/{keycloakRealm}",
                        $"http://localhost:{keycloakPort}/realms/{keycloakRealm}",
                        authority
                    },
                    ValidateAudience = true,
                    ValidAudience = Environment.GetEnvironmentVariable("KEYCLOAK_AUDIENCE") ?? "webapi",
                    ValidateLifetime = true,
                    RoleClaimType = "roles",
                    NameClaimType = "preferred_username"
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

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme.",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT"
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });

        builder.Services.AddHttpClient();

        var app = builder.Build();

        app.UseStaticFiles();
        app.UseRouting();

        app.UseCors();

        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }

    private static void ConfigureService(WebApplicationBuilder builder)
    {
        var dataManagerServiceUrl =
            Environment.GetEnvironmentVariable("DATA_MANAGER_SERVICE_URL") ?? "http://localhost:8003";
        var redisConnectionString =
            Environment.GetEnvironmentVariable("REDIS_CONNECTION_STRING") ??
            "localhost:6379,password=password,abortConnect=false";

        builder.Services.AddScoped(_ => new HttpClient());
        builder.Services.AddScoped<ILogger, ConsoleLogger>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IStandService, StandService>();
        builder.Services.AddSingleton<IRefreshStoreService, RedisRefreshStoreServiceService>();
        builder.Services.AddSingleton<IConnectionMultiplexer>(_ =>
        {
            var options = ConfigurationOptions.Parse(redisConnectionString);
            options.AbortOnConnectFail = false;
            return ConnectionMultiplexer.Connect(options);
        });

        builder.Services.AddScoped<IUserProvider>(p => new UserProvider(dataManagerServiceUrl, p.GetRequiredService<HttpClient>()));
        builder.Services.AddScoped<IStandProvider>(p => new StandProvider(dataManagerServiceUrl, p.GetRequiredService<HttpClient>()));

        builder.Services.AddAutoMapper(
            typeof(MappingProfile).Assembly,
            typeof(WebApi.BLL.Mappings.MappingProfile).Assembly
        );
    }
}
