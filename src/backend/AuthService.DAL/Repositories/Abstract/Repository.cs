using System.Data;
using System.Data.Common;
using AuthService.Consts;
using Npgsql;

namespace AuthService.DAL.Repositories.Abstract;

/// <summary>
/// Абстрактная сущность репозитория
/// </summary>
public abstract class Repository(string connectionString, string mainTableName) : IRepository
{
    /// <summary>
    /// Строка подключения к БД
    /// </summary>
    protected readonly string _connectionString = connectionString;

    /// <summary>
    /// Название основной таблицы репозитория
    /// </summary>
    protected readonly string _mainTableName = $"{PgTables.Schema}.{mainTableName}";

    /// <summary>
    /// Открытие транзакции
    /// </summary>
    /// <returns></returns>
    public IDbConnection CreateConnection()
    {
        return new NpgsqlConnection(_connectionString);
    }
}