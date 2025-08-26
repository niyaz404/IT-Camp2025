using FluentMigrator;
using DataManagerService.PostgresMigrator.Consts;

namespace DataManagerService.PostgresMigrator.Migrations
{
    /// <summary>
    /// Наполнение motor_defects моковыми данными для двигателей первого стенда
    /// </summary>
    [Migration(17, "Наполнение motor_defects моковыми данными (стенд 1)")]
    public class M17 : Migration
    {
        private const string Table = "motor_defects";

        // Список заводских номеров двигателей стенда 1 (из M15)
        private static readonly string[] Stand1FactoryNumbers = new[]
        {
            "765433456-001","765433456-002","765433456-003","765433456-004","765433456-005",
            "765433456-006","765433456-007","765433456-008","765433456-009","765433456-010"
        };

        public override void Up()
        {
            Console.WriteLine();
            Console.WriteLine(GetType());
            Console.WriteLine();
            if (!Schema.Schema(Const.Schema).Table(Table).Exists())
                return;

            // ЭДС-110-117М №1
            InsertDefect("765433456-001", 2, 85, "2025-08-20T09:10:00Z", "seed-m17", "detector-1.0.0");
            InsertDefect("765433456-001", 3, 35, "2025-08-20T09:12:00Z", "seed-m17", "detector-1.0.0");

            // ЭД-125-117М №1
            InsertDefect("765433456-002", 1, 45, "2025-08-21T08:00:00Z", "seed-m17", "detector-1.0.0");

            // ЭДС-125-117М №1
            InsertDefect("765433456-003", 3, 88, "2025-08-19T14:22:00Z", "seed-m17", "detector-1.0.0");
            InsertDefect("765433456-003", 4, 26, "2025-08-19T14:25:00Z", "seed-m17", "detector-1.0.0");

            // ЭДС-140-117М №1
            InsertDefect("765433456-004", 4, 52, "2025-08-18T11:05:00Z", "seed-m17", "detector-1.0.0");

            // ВДМ14-400-3.0 №1
            InsertDefect("765433456-005", 2, 31, "2025-08-17T07:40:00Z", "seed-m17", "detector-1.0.0");

            // ЭДС-110-117М №2
            InsertDefect("765433456-006", 1, 22, "2025-08-16T10:10:00Z", "seed-m17", "detector-1.0.0");

            // ЭД-125-117М №2
            InsertDefect("765433456-007", 3, 64, "2025-08-15T13:33:00Z", "seed-m17", "detector-1.0.0");
            InsertDefect("765433456-007", 2, 48, "2025-08-15T13:35:00Z", "seed-m17", "detector-1.0.0");

            // ЭДС-140-117М №2

            // АИР-90L-4 №1
            InsertDefect("765433456-009", 1, 18, "2025-08-14T06:00:00Z", "seed-m17", "detector-1.0.0");

            // ВД-315-6 №1
            InsertDefect("765433456-010", 4, 57, "2025-08-13T12:20:00Z", "seed-m17", "detector-1.0.0");
            InsertDefect("765433456-010", 2, 29, "2025-08-13T12:25:00Z", "seed-m17", "detector-1.0.0");
        }

        public override void Down()
        {
            if (!Schema.Schema(Const.Schema).Table(Table).Exists())
                return;

            Execute.Sql($@"
                DELETE FROM {Const.Schema}.{Table} d
                USING {Const.Schema}.motors m
                WHERE d.motor_id = m.id
                  AND m.factory_number = ANY(ARRAY[{ToSqlArray(Stand1FactoryNumbers)}])
                  AND d.note = 'seed-m17';
            ");
        }

        private void InsertDefect(string factoryNumber, short defectType, short severity, string detectedAtIsoUtc, string note, string detectorVersion)
        {
            Execute.Sql($@"
                INSERT INTO {Const.Schema}.motor_defects (motor_id, defect_type, severity, detection_date, note, detector_version, created_at, updated_at)
                SELECT m.id, {defectType}, {severity}, TIMESTAMPTZ '{detectedAtIsoUtc}', '{Escape(note)}', '{Escape(detectorVersion)}', NOW(), NOW()
                FROM {Const.Schema}.motors m
                WHERE m.factory_number = '{Escape(factoryNumber)}';
            ");
        }

        private static string Escape(string s) => s.Replace("'", "''");

        private static string ToSqlArray(string[] items)
        {
            return string.Join(",", Enumerable.Select(items, x => $"'{x.Replace("'", "''")}'"));
        }
    }
}
