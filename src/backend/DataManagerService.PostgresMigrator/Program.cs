using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;

namespace DataManagerService.PostgresMigrator;

class Program
{
    public static void Main(string[] args)
    {
        var services = new ServiceCollection()
            .AddFluentMigratorCore()
            .ConfigureRunner(rb => rb
                .AddPostgres()
                .WithGlobalConnectionString(GetConnectionString())
                .ScanIn(typeof(Program).Assembly)
                .For.All())
            .BuildServiceProvider();

        using var scope = services.CreateScope();
        var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
        runner.MigrateUp();
    }

    private static string GetConnectionString()
    {
        var connectionString = new Npgsql.NpgsqlConnectionStringBuilder
        {
            Host = Environment.GetEnvironmentVariable("POSTGRES_HOST") ?? "localhost",
            Port = int.Parse(Environment.GetEnvironmentVariable("POSTGRES_PORT") ?? "5434"),
            Username = Environment.GetEnvironmentVariable("POSTGRES_USER") ?? "dms",
            Password = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD") ?? "password",
            Database = Environment.GetEnvironmentVariable("POSTGRES_DB") ?? "dms"
        };

        return connectionString.ToString();
    }
}