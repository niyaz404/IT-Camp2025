using System;
using FluentMigrator;
using DataManagerService.PostgresMigrator.Consts;

namespace DataManagerService.PostgresMigrator.Migrations
{
    /// <summary>
    /// Наполнение таблицы motors моковыми данными
    /// </summary>
    [Migration(15, "Наполнение таблицы motors моковыми данными")]
    public class M15 : Migration
    {
        private const string Table = "motors";

        public override void Up()
        {
            if (!Schema.Schema(Const.Schema).Table(Table).Exists())
                return;

            // СТЕНД 1 (10 ЭД)
            InsertMotor(1, 1, "Борец", "765433456-001", "Основной тестовый двигатель", 1);
            InsertMotor(1, 2, "Новомет", "765433456-002", "Рабочий экземпляр", 1);
            InsertMotor(1, 3, "АЛНАС", "765433456-003", "Используется для диагностики", 2);
            InsertMotor(1, 4, "Борец", "765433456-004", "Стендовый образец", 2);
            InsertMotor(1, 5, "Новомет", "765433456-005", "Опытный запуск", 1);
            InsertMotor(1, 1, "АЛНАС", "765433456-006", "Дубликат модели", 1);
            InsertMotor(1, 2, "Борец", "765433456-007", "Служебный двигатель", 2);
            InsertMotor(1, 4, "Новомет", "765433456-008", "Резерв", 1);
            InsertMotor(1, 6, "ТЭМЗ", "765433456-009", "Низковольтный двигатель", 1);
            InsertMotor(1, 7, "Борец", "765433456-010", "Высокомощный экземпляр", 2);

            // СТЕНД 2 (2 ЭД)
            InsertMotor(2, 5, "Новомет", "765433456-011", "Тестируется в режиме нагрузки", 1);
            InsertMotor(2, 1, "Борец", "765433456-012", "Запасной двигатель", 1);

            // СТЕНД 3 (3 ЭД)
            InsertMotor(3, 2, "АЛНАС", "765433456-013", "В эксплуатации", 1);
            InsertMotor(3, 6, "ТЭМЗ", "765433456-014", "Учебный стенд", 1);
            InsertMotor(3, 7, "Борец", "765433456-015", "Нагруженный тест", 2);

            // СТЕНД 4 (1 ЭД)
            InsertMotor(4, 3, "Новомет", "765433456-016", "Малый ресурс", 1);

            // СТЕНД 5 (2 ЭД)
            InsertMotor(5, 4, "Борец", "765433456-017", "Импортный", 1);
            InsertMotor(5, 1, "Schlumberger", "765433456-018", "Импортный двигатель", 2);
        }

        public override void Down()
        {
            if (!Schema.Schema(Const.Schema).Table(Table).Exists())
                return;

            string[] fns =
            {
                "765433456-001","765433456-002","765433456-003","765433456-004","765433456-005",
                "765433456-006","765433456-007","765433456-008","765433456-009","765433456-010",
                "765433456-011","765433456-012","765433456-013","765433456-014","765433456-015",
                "765433456-016","765433456-017","765433456-018"
            };

            foreach (var fn in fns)
                Delete.FromTable(Table).InSchema(Const.Schema).Row(new { factory_number = fn });
        }

        /// <summary>
        /// Вставка двигателя в таблицу
        /// </summary>
        private void InsertMotor(
            long standId,
            long modelId,
            string manufacturer,
            string factoryNo,
            string description,
            short state)
        {
            Insert.IntoTable(Table).InSchema(Const.Schema).Row(new
            {
                stand_id = standId,
                model_id = modelId,
                manufacturer = manufacturer,
                factory_number = factoryNo,
                description = description,
                type = 1, // пусть все будут асинхронные по умолчанию (или можно рандом)
                state = state,
                installed_at = DateTime.UtcNow.AddMonths(-new Random().Next(1, 36)) // случайная дата установки за последние 3 года
            });
        }
    }
}
