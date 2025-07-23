using AuthService.PostgresMigrator.Consts;
using FluentMigrator;

namespace AuthService.PostgresMigrator.Migrations
{
    /// <summary>
    /// Добавление тестовый данных в таблицу USER
    /// </summary>
    [Migration(10, "Добавление тестовый данных в таблицу USER")]
    public class M10 : Migration
    {
        private static readonly string _tableName = "users";
        private static readonly string _roleBindTableName = "user_role";
        
        public override void Up()
        {
            // Проверка на существование таблицы перед добавлением записей
            if (Schema.Schema(Const.Schema).Table(_tableName).Exists())
            {
                Insert.IntoTable(_tableName).InSchema(Const.Schema)
                    .Row(new
                    {
                        username = "ADMIN", login = "admin", //pw = admin
                        hash = "MzvZ8ZC47ff0bgnAoa3wck31Bam1XvoBcTlAHteUln8=", salt = "rA2R2AlSlvh4CXcCZ/VPFw=="
                    });
            }
        }

        public override void Down()
        {
            // Проверка на существование таблицы перед удалением записей
            if (Schema.Schema(Const.Schema).Table(_tableName).Exists())
            {
                Delete.FromTable(_tableName)
                    .Row(new
                    {
                        username = "ADMIN", login = "admin",
                        hash = "MzvZ8ZC47ff0bgnAoa3wck31Bam1XvoBcTlAHteUln8=", salt = "rA2R2AlSlvh4CXcCZ/VPFw=="
                    });
            }
        }
    }
}
