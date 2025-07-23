using System.Data;
using AuthService.DAL.Models;

namespace AuthService.DAL.Repositories.Interface;

/// <summary>
/// Интерфейс репозитория Связи пользователя и роли
/// </summary>
public interface IUserToRoleRepository
{
    /// <summary>
    /// Добавление связи пользователя и роли
    /// </summary>
    /// <param name="userToRole">Сущность связи</param>
    /// <param name="connection">Соединение</param>
    /// <param name="transaction">Транзакция</param>
    public Task InsertInTransaction(UserToRoleEntity userToRole, IDbConnection connection, IDbTransaction transaction);

    /// <summary>
    /// Получение идентификаторов ролей по идентификатору пользователя
    /// </summary>
    /// <param name="userId">Идентификатор пользовател</param>
    /// <returns>Список идентификаторов ролей</returns>
    public Task<IEnumerable<int>> SelectByUserId(string userId);
}