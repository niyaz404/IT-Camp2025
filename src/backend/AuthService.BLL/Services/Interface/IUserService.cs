using AuthService.BLL.Models;

namespace AuthService.BLL.Services.Interface;

/// <summary>
/// Сервис работы с опльзователями
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Получение основной информации о пользователе по идентификатору
    /// </summary>
    Task<UserInfoModel> GetInfoByIdAsync(Guid userId);
}