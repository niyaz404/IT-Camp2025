using WebApi.DAL.Models.Implementation.Users;

namespace WebApi.DAL.Providers.Interface;

/// <summary>
/// Провайдер для работы с пользователями
/// </summary>
public interface IUserProvider
{
    /// <summary>
    /// Получение информации о текущем пользователе
    /// </summary>
    Task<UserInfo> GetCurrentUserInfoAsync(string bearerToken);
}