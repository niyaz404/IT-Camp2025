namespace WebApi.BLL.Services.Interfaces.Cache;

/// <summary>
/// Сервис для работы с хранилищем редис
/// </summary>
public interface IRefreshStoreService
{
    /// <summary>
    /// Сохранение токена
    /// </summary>
    Task SaveAsync(string userId, string refreshToken);
    
    /// <summary>
    /// Получение токена по идентификатору пользователя
    /// </summary>
    Task<string?> GetAsync(string userId);
}