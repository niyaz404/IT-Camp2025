using DataManagerService.PostgresMigrator.Consts;
using FluentMigrator;

namespace DataManagerService.PostgresMigrator.Migrations
{
    /// <summary>
    /// Создание таблицы motor_types (виды ЭД)
    /// </summary>
    [Migration(12, "Создание таблицы motor_models")]
    public class M12 : Migration
    {
        private const string Table = "motor_models";

        public override void Up()
        {
            Console.WriteLine();
            Console.WriteLine(GetType());
            Console.WriteLine();
            if (!Schema.Schema(Const.Schema).Table(Table).Exists())
            {
                Create.Table(Table).InSchema(Const.Schema)
                    .WithColumn("id").AsInt64().PrimaryKey().Identity()
                    .WithColumnDescription("Идентификатор вида ЭД")
                    .WithColumn("name").AsString(200).NotNullable()
                    .WithColumnDescription("Название модели/вида ЭД, например ЭДС-110-117М")
                    .WithColumn("description").AsString(int.MaxValue).Nullable()
                    .WithColumnDescription("Описание/примечания")
                    .WithColumn("phases_count").AsInt16().NotNullable().WithDefaultValue(3)
                    .WithColumnDescription("Число фаз (обычно 3)")
                    .WithColumn("rated_frequency").AsFloat().NotNullable().WithDefaultValue(50.0)
                    .WithColumnDescription("Номинальная частота сети, Гц")
                    .WithColumn("rated_power").AsDecimal(10, 2).NotNullable()
                    .WithColumnDescription("Номинальная мощность, кВт")
                    .WithColumn("type").AsInt16().NotNullable()
                    .WithColumnDescription("Тип")
                    .WithColumn("voltage").AsInt32().Nullable()
                    .WithColumnDescription("Номинальное напряжение, В")
                    .WithColumn("rated_current").AsDecimal(10, 2).Nullable()
                    .WithColumnDescription("Номинальный ток, А")
                    .WithColumn("created_at").AsCustom("timestamptz").NotNullable()
                    .WithDefault(SystemMethods.CurrentUTCDateTime)
                    .WithColumnDescription("Дата/время создания (UTC)")
                    .WithColumn("updated_at").AsCustom("timestamptz").NotNullable()
                    .WithDefault(SystemMethods.CurrentUTCDateTime)
                    .WithColumnDescription("Дата/время обновления (UTC)");
            }
        }

        public override void Down()
        {
            if (Schema.Schema(Const.Schema).Table(Table).Exists())
            {
                Delete.Table(Table).InSchema(Const.Schema);
            }
        }
    }
}
