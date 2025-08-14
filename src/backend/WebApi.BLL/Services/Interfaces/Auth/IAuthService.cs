using WebApi.BLL.Models.Implementation.Auth;
using WebApi.DAL.Models.Implementation.Auth;

namespace WebApi.BLL.Services.Interfaces.Auth;

/// <summary>
/// Интерфейс сервиса для работы с авторизацией
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// Метод входа в систему (получения токена)
    /// </summary>
    public Task<LoginResponseModel> Login(UserCredentialsModel userCredentials);
    
    /// <summary>
    /// Метод обновления токена
    /// </summary>
    public Task<TokenPair> RefreshToken(string refreshToken);
    
    /// <summary>
    /// Регистрация пользователя
    /// </summary>
    public Task<LoginResponseModel> Register(UserModel user);
    
    /// <summary>
    /// МСброс пароля
    /// </summary>
    public Task ResetPassword(UserCredentialsModel userCredentials);
}