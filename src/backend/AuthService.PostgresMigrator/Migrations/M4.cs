using AuthService.PostgresMigrator.Consts;
using FluentMigrator;

namespace AuthService.PostgresMigrator.Migrations
{
    /// <summary>
    /// Миграция для добавления записей в таблицу ROLE
    /// </summary>
    [Migration(4, "Добавление записей в таблицу ROLES")]
    public class M4 : Migration
    {
        private static readonly string _tableName = "roles";
        
        public override void Up()
        {
            // Проверка на существование таблицы перед добавлением записей
            if (Schema.Schema(Const.Schema).Table(_tableName).Exists())
            {
                Insert.IntoTable(_tableName).InSchema(Const.Schema)
                    .Row(new { id = 1, name = "ADMIN" })
                    .Row(new { id = 2, name = "EXPERT" });
            }
        }

        public override void Down()
        {
            // Проверка на существование таблицы перед удалением записей
            if (Schema.Schema(Const.Schema).Table(_tableName).Exists())
            {
                Delete.FromTable(_tableName)
                    .Row(new { id = 1, name = "ADMIN" })
                    .Row(new { id = 2, name = "EXPERT" });
            }
        }
    }
}
