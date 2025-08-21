using DataManagerService.BLL.Services.Implementation.Stands;
using DataManagerService.BLL.Services.Interfaces.Stands;
using DataManagerService.DAL.Repositories.Implementation;
using DataManagerService.DAL.Repositories.Interface;
using DataManagerService.Mapping;
using Share.Services.Implementation;
using ILogger = Share.Services.Interface.ILogger;

namespace DataManagerService;

public class Program
{
    public static void Main(string[] args)
    {
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
        var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING") ??
                               "Host=localhost;Port=5433;SearchPath=itcamp;Username=dms;Password=password;Database=dms;";
        
        //Сервисы
        services.AddScoped<ILogger, ConsoleLogger>();
        services.AddScoped<IStandService, StandService>();
        
        //Репозитории
        services.AddScoped<IStandsRepository>(p => new StandsRepository(connectionString));
        
        services.AddAutoMapper(
            typeof(MappingProfile).Assembly, 
            typeof(DataManagerService.BLL.Mapping.MappingProfile).Assembly
        );
    }
}