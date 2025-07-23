using AuthService.PostgresMigrator.Consts;
using FluentMigrator;

namespace AuthService.PostgresMigrator.Migrations
{
    /// <summary>
    /// Миграция для создания таблицы ROLE
    /// </summary>
    [Migration(3, "Создание таблицы ROLES")]
    public class M3 : Migration
    {
        private static readonly string _tableName = "roles";
        
        public override void Up()
        {
            // Проверка на существование таблицы перед созданием
            if (!Schema.Schema(Const.Schema).Table(_tableName).Exists())
            {
                Create.Table(_tableName).InSchema(Const.Schema)
                    .WithColumn("id").AsInt32().PrimaryKey()
                    .WithColumnDescription("Идентификатор роли")

                    .WithColumn("name").AsString(255).NotNullable()
                    .WithColumnDescription("Название роли");
            }
        }

        public override void Down()
        {
            // Проверка на существование таблицы перед удалением
            if (Schema.Schema(Const.Schema).Table(_tableName).Exists())
            {
                Delete.Table(_tableName).InSchema(Const.Schema);
            }
        }
    }
}
