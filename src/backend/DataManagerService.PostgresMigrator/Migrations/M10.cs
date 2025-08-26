using DataManagerService.PostgresMigrator.Consts;
using FluentMigrator;

namespace DataManagerService.PostgresMigrator.Migrations
{
    /// <summary>
    /// Создание таблицы STANDS (стенды с электродвигателями)
    /// </summary>
    [Migration(10, "Создание таблицы STANDS")]
    public class M10 : Migration
    {
        private const string Table = "stands";

        public override void Up()
        {
            Console.WriteLine();
            Console.WriteLine(GetType());
            Console.WriteLine();
            if (!Schema.Schema(Const.Schema).Table(Table).Exists())
            {
                Create.Table(Table).InSchema(Const.Schema)
                    .WithColumn("id").AsInt64().PrimaryKey().Identity()
                        .WithColumnDescription("Идентификатор стенда (BIGSERIAL PK)")

                    .WithColumn("name").AsString(200).NotNullable()
                        .WithColumnDescription("Название стенда")

                    .WithColumn("description").AsString(int.MaxValue).Nullable()
                        .WithColumnDescription("Описание стенда")

                    .WithColumn("state").AsInt16().NotNullable().WithDefaultValue(1)
                        .WithColumnDescription("Состояние стенда")

                    .WithColumn("location").AsString(200).Nullable()
                        .WithColumnDescription("Местоположение стенда (цех, скважина, куст)")

                    .WithColumn("user_id").AsGuid().Nullable()
                    .WithColumnDescription("Идентификатор ответственного")

                    .WithColumn("created_at").AsCustom("timestamptz").NotNullable()
                        .WithDefault(SystemMethods.CurrentUTCDateTime)
                        .WithColumnDescription("Дата и время создания записи (UTC)")

                    .WithColumn("updated_at").AsCustom("timestamptz").NotNullable()
                        .WithDefault(SystemMethods.CurrentUTCDateTime)
                        .WithColumnDescription("Дата и время последнего обновления записи (UTC)");
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
