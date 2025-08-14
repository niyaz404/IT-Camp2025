using WebApi.DAL.Models.Implementation.Auth;

namespace WebApi.DAL.Providers.Interface;

/// <summary>
/// Интерфейс провайдера для работы с Auth-сервисом
/// </summary>
public interface IAuthProvider
{
    /// <summary>
    /// Метод авторизации через сервис авторизации
    /// </summary>
    /// <returns>Токен</returns>
    Task<TokenPair> Login(UserCredentials userCredentials);
    
    /// <summary>
    /// Метод регистрации
    /// </summary>
    /// <returns>Токен</returns>
    Task<TokenPair> Register(User user);

    /// <summary>
    /// Обновление токена
    /// </summary>
    /// <returns></returns>
    Task<TokenPair> RefreshToken(string userId, string refreshToken);
    
    /// <summary>
    /// Метод сброса пароля
    /// </summary>
    Task ResetPassword(UserCredentials userCredentials);
}