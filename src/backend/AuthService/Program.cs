using System.Text;
using AuthService.BLL.Services.Implementation;
using AuthService.BLL.Services.Interface;
using AuthService.DAL.Repositories.Implementation;
using AuthService.DAL.Repositories.Interface;
using AuthService.Mapping;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Share.Services.Implementation;
using ILogger = Share.Services.Interface.ILogger;

namespace AuthService;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
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
                    IssuerSigningKey = new SymmetricSecurityKey("SuperStrongAndLongJwtSecretKey_123456!"u8.ToArray())
                };
            });
        builder.Services.AddAuthorization();
        builder.Services.AddControllers();
        
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        app.UseCors();
        
        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        app.Run();
    }

    public static void ConfigureService(IServiceCollection services)
    {
        var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING") ??
                               "Host=localhost;Port=5433;SearchPath=itcamp;Username=auth;Password=password;Database=auth;";
        
        services.AddScoped<ILogger, ConsoleLogger>();
        
        services.AddScoped<IAuthService, BLL.Services.Implementation.AuthService>();
        services.AddScoped<IUserService, UserService>();
        
        services.AddScoped<IUserRepository>(_ =>  new UserRepository(connectionString));
        services.AddScoped<IUserToRoleRepository>(_ => new UserToToRoleRepository(connectionString));
        
        services.AddAutoMapper(
            typeof(MappingProfile).Assembly, 
            typeof(AuthService.BLL.Mappings.MappingProfile).Assembly
        );
    }
}