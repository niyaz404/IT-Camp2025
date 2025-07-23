using System.Data;
using System.Data.Common;
using AuthService.DAL.Models;
using AuthService.DAL.Repositories.Abstract;
using AuthService.DAL.Repositories.Interface;
using Dapper;
using Npgsql;

namespace AuthService.DAL.Repositories.Implementation;

/// <summary>
/// Репозиторий для сущностей Пользователя
/// </summary>
public class UserRepository(string connectionString) : Repository(connectionString, "users"), IUserRepository
{
    public async Task<UserEntity> SelectByLogin(string login)
    {
        var sql = @$"
            select m.*, (
                select string_agg(r.name, ',')
                from roles r
                join user_role ur ON ur.role_id = r.id
                where ur.user_id = m.id
              ) AS roles
            from {_mainTableName} m
            where login=:login";

        await using var connection = new NpgsqlConnection(_connectionString);
        return await connection.QueryFirstOrDefaultAsync<UserEntity>(sql, new { login });
    }
    
    public async Task<Guid> SelectIdByLogin(string login)
    {
        var sql = $"select id from {_mainTableName} where login=:login";

        await using var connection = new NpgsqlConnection(_connectionString);
        return await connection.QueryFirstOrDefaultAsync<Guid>(sql, new { login });
    }

    public async Task UpdatePassword(UserEntity user)
    {
        var sql = $"update {_mainTableName} set hash = :hash, salt = :salt where login=:login";

        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.ExecuteAsync(sql, new { hash = user.Hash, salt = user.Salt, login = user.Login });
    }

    public async Task<Guid> InsertInTransaction(UserEntity user, IDbConnection connection, IDbTransaction transaction)
    {
        var sql = $"insert into {_mainTableName} (username, login, hash, salt) values(:username, :login, :hash, :salt) returning id";
        
        return await connection.QueryFirstOrDefaultAsync<Guid>(sql, new
        {
            username = user.Username,
            login = user.Login,
            hash = user.Hash,
            salt = user.Salt
        }, transaction);
    }
}