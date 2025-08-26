using DataManagerService.BLL.Services.Implementation.Defects;
using DataManagerService.BLL.Services.Implementation.Motors;
using DataManagerService.BLL.Services.Implementation.Stands;
using DataManagerService.BLL.Services.Interfaces.Defects;
using DataManagerService.BLL.Services.Interfaces.Motors;
using DataManagerService.BLL.Services.Interfaces.Stands;
using DataManagerService.DAL.Repositories.Implementation;
using DataManagerService.DAL.Repositories.Implementation.Defects;
using DataManagerService.DAL.Repositories.Implementation.Keycloak;
using DataManagerService.DAL.Repositories.Implementation.Motors;
using DataManagerService.DAL.Repositories.Implementation.Stands;
using DataManagerService.DAL.Repositories.Interface;
using DataManagerService.DAL.Repositories.Interface.Defects;
using DataManagerService.DAL.Repositories.Interface.Keycloak;
using DataManagerService.DAL.Repositories.Interface.Motors;
using DataManagerService.DAL.Repositories.Interface.Stands;
using DataManagerService.Mapping;
using Share.Services.Implementation;
using ILogger = Share.Services.Interface.ILogger;

namespace DataManagerService;

public class Program
{
    public static void Main(string[] args)
    {
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
        
        var builder = WebApplication.CreateBuilder(args);
        
        ConfigureService(builder.Services);
        
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
        var connectionString = new Npgsql.NpgsqlConnectionStringBuilder
        {
            Host = Environment.GetEnvironmentVariable("POSTGRES_HOST") ?? "localhost",
            Port = int.Parse(Environment.GetEnvironmentVariable("POSTGRES_PORT") ?? "5434"),
            Username = Environment.GetEnvironmentVariable("POSTGRES_USER") ?? "dms",
            Password = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD") ?? "password",
            Database = Environment.GetEnvironmentVariable("POSTGRES_DB") ?? "dms"
        }.ToString();

        var keycloakBaseUrl = Environment.GetEnvironmentVariable("KEYCLOAK_BASEURL") ?? "http://localhost:8090";
        var keycloakRealm = Environment.GetEnvironmentVariable("KEYCLOAK_REALM") ?? "preditrix";
        var keycloakClientId = Environment.GetEnvironmentVariable("KEYCLOAK_CLIENTID") ?? "data-manager-service";
        var keycloakClientSecret = Environment.GetEnvironmentVariable("KEYCLOAK_CLIENTSECRET") ?? "super-secret-here";

        services.AddScoped(_ => new HttpClient());
        services.AddScoped<ILogger, ConsoleLogger>();
        services.AddScoped<IStandService, StandService>();
        services.AddScoped<IMotorService, MotorService>();
        services.AddScoped<IMotorDefectService, MotorDefectService>();
        
        services.AddScoped<IStandsRepository>(_ => new StandsRepository(connectionString));
        services.AddScoped<IMotorsRepository>(_ => new MotorsRepository(connectionString));
        services.AddScoped<IMotorDefectsRepository>(_ => new MotorDefectsRepository(connectionString));

        services.AddScoped<IKeycloakProvider>(sp =>
            new KeycloakProvider(
                sp.GetRequiredService<HttpClient>(),
                keycloakBaseUrl,
                keycloakRealm,
                keycloakClientId,
                keycloakClientSecret
            ));

        services.AddAutoMapper(
            typeof(MappingProfile).Assembly,
            typeof(DataManagerService.BLL.Mapping.MappingProfile).Assembly
        );
    }
}