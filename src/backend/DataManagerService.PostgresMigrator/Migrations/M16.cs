using DataManagerService.PostgresMigrator.Consts;
using FluentMigrator;

namespace DataManagerService.PostgresMigrator.Migrations
{
    /// <summary>
    /// Создание таблицы motor_defects 
    /// </summary>
    [Migration(16, "Создание таблицы motor_defects")]
    public class M16 : Migration
    {
        private const string Table = "motor_defects";

        public override void Up()
        {
            Console.WriteLine();
            Console.WriteLine(GetType());
            Console.WriteLine();
            if (!Schema.Schema(Const.Schema).Table(Table).Exists())
            {
                Create.Table(Table).InSchema(Const.Schema)
                    .WithColumn("id").AsInt64().PrimaryKey().Identity()
                        .WithColumnDescription("Идентификатор записи о дефекте")
                    .WithColumn("motor_id").AsInt64().NotNullable()
                        .WithColumnDescription("FK на двигатель ")
                    .WithColumn("defect_type").AsInt16().NotNullable()
                        .WithColumnDescription("Тип дефекта: 1=stator, 2=rotor, 3=bearing, 4=misalignment")
                    .WithColumn("severity").AsInt16().NotNullable()
                        .WithColumnDescription("Степень выраженности, 0..100")
                    .WithColumn("detection_date").AsCustom("timestamptz").NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime)
                        .WithColumnDescription("Дата/время обнаружения дефекта (UTC)")
                    .WithColumn("detector_version").AsString(50).Nullable()
                        .WithColumnDescription("Версия детектора, обнаружившего дефект")
                    .WithColumn("note").AsString(int.MaxValue).Nullable()
                        .WithColumnDescription("Комментарий/примечание")
                    .WithColumn("created_at").AsCustom("timestamptz").NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime)
                        .WithColumnDescription("Дата/время создания записи (UTC)")
                    .WithColumn("updated_at").AsCustom("timestamptz").NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime)
                        .WithColumnDescription("Дата/время обновления записи (UTC)");

                Execute.Sql($@"
                    ALTER TABLE {Const.Schema}.{Table}
                    ADD CONSTRAINT {Const.Schema}_{Table}_ck_severity
                    CHECK (severity BETWEEN 0 AND 100);
                ");

                Execute.Sql($@"
                    ALTER TABLE {Const.Schema}.{Table}
                    ADD CONSTRAINT {Const.Schema}_{Table}_ck_defect_type
                    CHECK (defect_type IN (1,2,3,4));
                ");

                Create.ForeignKey($"{Const.Schema}_{Table}_motor_fk")
                    .FromTable(Table).InSchema(Const.Schema).ForeignColumn("motor_id")
                    .ToTable("motors").InSchema(Const.Schema).PrimaryColumn("id");
            }
        }

        public override void Down()
        {
            if (Schema.Schema(Const.Schema).Table(Table).Exists())
            {
                Delete.ForeignKey($"{Const.Schema}_{Table}_motor_fk").OnTable(Table).InSchema(Const.Schema);
                Execute.Sql($@"ALTER TABLE {Const.Schema}.{Table} DROP CONSTRAINT IF EXISTS {Const.Schema}_{Table}_ck_severity;");
                Execute.Sql($@"ALTER TABLE {Const.Schema}.{Table} DROP CONSTRAINT IF EXISTS {Const.Schema}_{Table}_ck_defect_type;");
                Delete.Table(Table).InSchema(Const.Schema);
            }
        }
    }
}
