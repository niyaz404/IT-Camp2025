using DataManagerService.PostgresMigrator.Consts;
using FluentMigrator;

namespace DataManagerService.PostgresMigrator.Migrations
{
    /// <summary>
    /// Создание таблицы motors (реальные экземпляры ЭД)
    /// </summary>
    [Migration(13, "Создание таблицы motors")]
    public class M13 : Migration
    {
        private const string Table = "motors";

        public override void Up()
        {
            Console.WriteLine();
            Console.WriteLine(GetType());
            Console.WriteLine();
            if (!Schema.Schema(Const.Schema).Table(Table).Exists())
            {
                Create.Table(Table).InSchema(Const.Schema)
                    .WithColumn("id").AsInt64().PrimaryKey().Identity()
                        .WithColumnDescription("Идентификатор экземпляра ЭД")
                    .WithColumn("stand_id").AsInt64().NotNullable()
                        .WithColumnDescription("FK на стенд (stands.id)")
                    .WithColumn("model_id").AsInt64().NotNullable()
                        .WithColumnDescription("FK на вид ЭД (motor_types.id)")
                    .WithColumn("manufacturer").AsString(200).Nullable()
                        .WithColumnDescription("Производитель")
                    .WithColumn("user_id").AsGuid().Nullable()
                        .WithColumnDescription("Идентификатор ответственного")
                    .WithColumn("factory_number").AsString(100).NotNullable()
                        .WithColumnDescription("Заводской номер")
                    .WithColumn("description").AsString(int.MaxValue).Nullable()
                        .WithColumnDescription("Описание/примечания")
                    .WithColumn("phases_count").AsInt16().Nullable()
                        .WithColumnDescription("Число фаз").WithDefaultValue(3)
                    .WithColumn("type").AsInt16().WithDefaultValue(1)
                        .WithColumnDescription("Тип")
                    .WithColumn("state").AsInt16().NotNullable().WithDefaultValue(1)
                        .WithColumnDescription("Состояние")
                    .WithColumn("installed_at").AsCustom("timestamptz").Nullable()
                        .WithColumnDescription("Дата установки/ввода в эксплуатацию")
                    .WithColumn("created_at").AsCustom("timestamptz").NotNullable()
                        .WithDefault(SystemMethods.CurrentUTCDateTime)
                        .WithColumnDescription("Дата/время создания (UTC)")
                    .WithColumn("updated_at").AsCustom("timestamptz").NotNullable()
                        .WithDefault(SystemMethods.CurrentUTCDateTime)
                        .WithColumnDescription("Дата/время обновления (UTC)");

                Create.ForeignKey($"{Const.Schema}_{Table}_stand_fk")
                    .FromTable(Table).InSchema(Const.Schema).ForeignColumn("stand_id")
                    .ToTable("stands").InSchema(Const.Schema).PrimaryColumn("id");

                Create.ForeignKey($"{Const.Schema}_{Table}_type_fk")
                    .FromTable(Table).InSchema(Const.Schema).ForeignColumn("model_id")
                    .ToTable("motor_models").InSchema(Const.Schema).PrimaryColumn("id");
            }
        }

        public override void Down()
        {
            if (Schema.Schema(Const.Schema).Table(Table).Exists())
            {
                Delete.ForeignKey($"{Const.Schema}_{Table}_stand_fk").OnTable(Table).InSchema(Const.Schema);
                Delete.ForeignKey($"{Const.Schema}_{Table}_type_fk").OnTable(Table).InSchema(Const.Schema);
                Delete.Table(Table).InSchema(Const.Schema);
            }
        }
    }
}
