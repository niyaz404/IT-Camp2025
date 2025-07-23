using AuthService.PostgresMigrator.Consts;
using FluentMigrator;

namespace AuthService.PostgresMigrator.Migrations
{
    /// <summary>
    /// Миграция для создания таблицы USER_ROLE
    /// </summary>
    [Migration(6, "Создание таблицы USER_ROLE")]
    public class M6 : Migration
    {
        private static readonly string _tableName = "user_role";
        private static readonly string _userTableName = "users";
        private static readonly string _roleTableName = "roles";
        
        public override void Up()
        {
            // Проверка на существование таблицы перед созданием
            if (!Schema.Schema(Const.Schema).Table(_tableName).Exists())
            {
                Create.Table(_tableName).InSchema(Const.Schema)
                    .WithColumn("id").AsGuid().PrimaryKey().WithDefault(SystemMethods.NewGuid)
                    .WithColumnDescription("Идентификатор связи")

                    .WithColumn("user_id").AsGuid().NotNullable()
                    .ForeignKey("user_id", Const.Schema, _userTableName, "id")
                    .WithColumnDescription("Идентификатор пользователя")

                    .WithColumn("role_id").AsInt32().NotNullable()
                    .ForeignKey("role_id", Const.Schema, _roleTableName, "id")
                    .WithColumnDescription("Идентификатор роли");
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
