using DataManagerService.PostgresMigrator.Consts;
using FluentMigrator;

namespace DataManagerService.PostgresMigrator.Migrations
{
    /// <summary>
    /// Наполнение таблицы motor_models
    /// </summary>
    [Migration(14, "Наполнение motor_models")]
    public class M14 : Migration
    {
        private const string Table = "motor_models";

        public override void Up()
        {
            Console.WriteLine();
            Console.WriteLine(GetType());
            Console.WriteLine();
            if (!Schema.Schema(Const.Schema).Table(Table).Exists())
                return;

            Insert.IntoTable(Table).InSchema(Const.Schema).Row(new
            {
                name = "ЭДС-110-117М",
                description = "Асинхронный электродвигатель, серия ЭДС",
                phases_count = (short)3,
                rated_frequency = 50.0,
                rated_power = 110.00m,
                type = (short)1,
                voltage = 6000,
                rated_current = 13.5m
            });

            Insert.IntoTable(Table).InSchema(Const.Schema).Row(new
            {
                name = "ЭД-125-117М",
                description = "Асинхронный электродвигатель, серия ЭД",
                phases_count = (short)3,
                rated_frequency = 50.0,
                rated_power = 125.00m,
                type = (short)1,
                voltage = 6000,
                rated_current = 15.0m
            });

            Insert.IntoTable(Table).InSchema(Const.Schema).Row(new
            {
                name = "ЭДС-125-117М",
                description = "Асинхронный электродвигатель, серия ЭДС",
                phases_count = (short)3,
                rated_frequency = 50.0,
                rated_power = 125.00m,
                type = (short)1,
                voltage = 6000,
                rated_current = 15.0m
            });

            Insert.IntoTable(Table).InSchema(Const.Schema).Row(new
            {
                name = "ЭДС-140-117М",
                description = "Асинхронный электродвигатель, серия ЭДС",
                phases_count = (short)3,
                rated_frequency = 50.0,
                rated_power = 140.00m,
                type = (short)1,
                voltage = 6000,
                rated_current = 16.8m
            });

            Insert.IntoTable(Table).InSchema(Const.Schema).Row(new
            {
                name = "ВДМ14-400-3.0-117/1В5",
                description = "Вентильный электродвигатель",
                phases_count = (short)3,
                rated_frequency = 50.0,
                rated_power = 140.00m,
                type = (short)2,
                voltage = 6000,
                rated_current = 17.0m
            });

            Insert.IntoTable(Table).InSchema(Const.Schema).Row(new
            {
                name = "АИР-90L-4",
                description = "Асинхронный двигатель общего назначения",
                phases_count = (short)3,
                rated_frequency = 50.0,
                rated_power = 2.20m,
                type = (short)1,
                voltage = 380,
                rated_current = 4.8m
            });

            Insert.IntoTable(Table).InSchema(Const.Schema).Row(new
            {
                name = "ВД-315-6",
                description = "Вентильный электродвигатель",
                phases_count = (short)3,
                rated_frequency = 50.0,
                rated_power = 200.00m,
                type = (short)2,
                voltage = 6000,
                rated_current = 25.0m
            });
        }

        public override void Down()
        {
            if (!Schema.Schema(Const.Schema).Table(Table).Exists())
                return;

            Delete.FromTable(Table).InSchema(Const.Schema).Row(new { name = "ЭДС-110-117М" });
            Delete.FromTable(Table).InSchema(Const.Schema).Row(new { name = "ЭД-125-117М" });
            Delete.FromTable(Table).InSchema(Const.Schema).Row(new { name = "ЭДС-125-117М" });
            Delete.FromTable(Table).InSchema(Const.Schema).Row(new { name = "ЭДС-140-117М" });
            Delete.FromTable(Table).InSchema(Const.Schema).Row(new { name = "ВДМ14-400-3.0-117/1В5" });
            Delete.FromTable(Table).InSchema(Const.Schema).Row(new { name = "АИР-90L-4" });
            Delete.FromTable(Table).InSchema(Const.Schema).Row(new { name = "ВД-315-6" });
        }
    }
}
