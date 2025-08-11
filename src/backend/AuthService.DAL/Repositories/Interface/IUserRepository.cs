using System.Data;
using AuthService.DAL.Models;
using AuthService.DAL.Repositories.Abstract;

namespace AuthService.DAL.Repositories.Interface;

/// <summary>
/// Интерфейс репозитория сущности Пользователя
/// </summary>
public interface IUserRepository : IRepository
{
    /// <summary>
    /// Возвращает пользователя по логину
    /// </summary>
    /// <param name="login">Логин</param>
    public Task<UserEntity> SelectByLogin(string login);
    
    /// <summary>
    /// Возвращает пользователя по логину
    /// </summary>
    /// <param name="login">Логин</param>
    public Task<UserInfoEntity> SelectById(Guid id);

    /// <summary>
    /// Добавляет нового пользователя в систему
    /// </summary>
    /// <param name="user">Сущность ползователя</param>
    /// <param name="connection">Соединение</param>
    /// <param name="transaction">Транзакция</param>
    public Task<Guid> InsertInTransaction(UserEntity user, IDbConnection connection, IDbTransaction transaction);
    
    /// <summary>
    /// Получение идентификатора пользователя по логину
    /// </summary>
    /// <param name="login">Логин ползователя</param>
    public Task<Guid> SelectIdByLogin(string login);
    
    /// <summary>
    /// Обновление пароля
    /// </summary>
    /// <param name="login">Логин ползователя</param>
    /// <param name="password">Новый пароль</param>
    public Task UpdatePassword(UserEntity user);
}