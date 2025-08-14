using WebApi.BLL.Models.Implementation.Users;

namespace WebApi.BLL.Services.Interfaces.Users;

/// <summary>
/// Сервис работы с опльзователями
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Получение основной информации о пользователе
    /// </summary>
    Task<UserInfoModel> GetCurrentUserInfoAsync(string bearerToken);
}