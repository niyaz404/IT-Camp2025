using System.Data;
using AuthService.DAL.Models;
using AuthService.DAL.Repositories.Abstract;
using AuthService.DAL.Repositories.Interface;
using Dapper;
using Npgsql;

namespace AuthService.DAL.Repositories.Implementation;

/// <summary>
/// Репозиторий для Связи Пользователя и Роли
/// </summary>
public class UserToToRoleRepository(string connectionString) : Repository(connectionString, "user_role"), IUserToRoleRepository
{
    public async Task InsertInTransaction(UserToRoleEntity userToRole, IDbConnection connection, IDbTransaction transaction)
    {
        var sql = $"insert into {_mainTableName} (user_id, role_id) values(:userid, :roleid)";
        await connection.ExecuteAsync(sql, new { userid = userToRole.UserId, roleid = userToRole.RoleId }, transaction);
    }

    public async Task<IEnumerable<int>> SelectByUserId(string userId)
    {
        var sql = $"select role_id from {_mainTableName} where user_id = :userid";
        
        await using var connection = new NpgsqlConnection(_connectionString);
        return await connection.QueryAsync<int>(sql, new { userid = userId });
    }
}