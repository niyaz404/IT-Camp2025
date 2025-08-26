using DataManagerService.PostgresMigrator.Consts;
using FluentMigrator;

namespace DataManagerService.PostgresMigrator.Migrations
{
    /// <summary>
    /// Миграция для создания базовой схемы
    /// </summary>
    [Migration(1, "Создания базовой схемы")]
    public class M1 : Migration
    {
        public override void Up()
        {
            Console.WriteLine();
            Console.WriteLine(GetType());
            Console.WriteLine();
            if (!Schema.Schema(Const.Schema).Exists())
            {
                Create.Schema(Const.Schema);
            }
        }

        public override void Down()
        {
            // Схему не удаляем
        }
    }
}
