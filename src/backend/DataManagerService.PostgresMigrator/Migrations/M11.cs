using DataManagerService.PostgresMigrator.Consts;
using FluentMigrator;

namespace DataManagerService.PostgresMigrator.Migrations
{
    /// <summary>
    /// Наполнение таблицы STANDS моковыми данными
    /// </summary>
    [Migration(11, "Наполнение stands")]
    public class M11 : Migration
    {
        private const string Table = "stands";

        public override void Up()
        {
            Console.WriteLine();
            Console.WriteLine(GetType());
            Console.WriteLine();
            if (Schema.Schema(Const.Schema).Table(Table).Exists())
            {
                Insert.IntoTable(Table).InSchema(Const.Schema).Row(new
                {
                    name = "Стенд 1",
                    description = "Тестовый стенд №1",
                    state = 1,
                    location = "Ямал, Цех A",
                });

                Insert.IntoTable(Table).InSchema(Const.Schema).Row(new
                {
                    name = "Стенд 2",
                    description = "Тестовый стенд №2",
                    state = 1,
                    location = "Меретояха, Цех 1",
                });

                Insert.IntoTable(Table).InSchema(Const.Schema).Row(new
                {
                    name = "Стенд 3",
                    description = "На обслуживании",
                    state = 2,
                    location = "Заполярье, Цех 1",
                });

                Insert.IntoTable(Table).InSchema(Const.Schema).Row(new
                {
                    name = "Стенд 4",
                    description = "Резервный стенд",
                    state = 0,
                    location = "ННГ, Цех 1",
                });

                Insert.IntoTable(Table).InSchema(Const.Schema).Row(new
                {
                    name = "Стенд 5",
                    description = "Новый стенд, тестирование",
                    state = 0,
                    location = "ЯМАЛ, Цех Б",
                });
            }
        }

        public override void Down()
        {
            if (Schema.Schema(Const.Schema).Table(Table).Exists())
            {
                Delete.FromTable(Table).InSchema(Const.Schema).Row(new { name = "Стенд 1" });
                Delete.FromTable(Table).InSchema(Const.Schema).Row(new { name = "Стенд 2" });
                Delete.FromTable(Table).InSchema(Const.Schema).Row(new { name = "Стенд 3" });
                Delete.FromTable(Table).InSchema(Const.Schema).Row(new { name = "Стенд 4" });
                Delete.FromTable(Table).InSchema(Const.Schema).Row(new { name = "Стенд 5" });
            }
        }
    }
}
