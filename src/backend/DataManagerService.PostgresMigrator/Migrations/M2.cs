using FluentMigrator;

namespace DataManagerService.PostgresMigrator.Migrations
{
    /// <summary>
    /// Миграция для создания модуля генерации UUID
    /// </summary>
    [Migration(2, "Создания модуля генерации UUID")]
    public class M2 : Migration
    {
        public override void Up()
        {
            Console.WriteLine();
            Console.WriteLine(GetType());
            Console.WriteLine();
            Execute.Sql("create extension if not exists \"uuid-ossp\";");
        }

        public override void Down() { }
    }
}
