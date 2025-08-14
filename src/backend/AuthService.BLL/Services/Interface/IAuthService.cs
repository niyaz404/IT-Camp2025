using AuthService.BLL.Models;

namespace AuthService.BLL.Services.Interface;

/// <summary>
/// Сервис аутентификации на уровне бизнес-логики
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// Генерирует токен доступа
    /// </summary>
    Task<TokenPair> GenerateToken(UserCredentialsModel userCredentials);

    /// <summary>
    /// Регистрирует пользователя в системе
    /// </summary>
    Task Register(UserModel user);

    /// <summary>
    /// Сброс пароля
    /// </summary>
    Task ResetPassword(UserCredentialsModel userCredentials);

    /// <summary>
    /// Обновление токена
    /// </summary>
    Task<TokenPair> RefreshToken(Guid userId, string refreshToken);
}