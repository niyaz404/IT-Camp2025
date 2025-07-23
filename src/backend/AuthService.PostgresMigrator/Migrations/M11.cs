using AuthService.PostgresMigrator.Consts;
using FluentMigrator;

namespace AuthService.PostgresMigrator.Migrations
{
    /// <summary>
    /// Добавление тестовый данных в таблицу USER
    /// </summary>
    [Migration(11, "Добавление тестовый данных в таблицу user_role")]
    public class M11 : Migration
    {
        private static readonly string _tableName = "users";
        private static readonly string _roleBindTableName = "user_role";
        
        public override void Up()
        {
            Execute.Sql($"insert into {Const.Schema}.{_roleBindTableName}(user_id, role_id) values ((select id from {Const.Schema}.{_tableName} where login = 'admin'), 1)");
        }

        public override void Down()
        {
            Execute.Sql($"delete from {Const.Schema}.{_roleBindTableName} where user_id = (select id from {Const.Schema}.{_tableName} where login = 'admin') and role_id = 1)");
        }
    }
}
