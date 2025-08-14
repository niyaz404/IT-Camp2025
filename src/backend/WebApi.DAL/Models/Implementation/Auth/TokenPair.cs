namespace WebApi.DAL.Models.Implementation.Auth;

/// <summary>
/// Пара токенов
/// </summary>
public class TokenPair
{
    /// <summary>
    /// Токен авторизации
    /// </summary>
    public string AccessToken { get; set; }
    
    /// <summary>
    /// Токен авторизации
    /// </summary>
    public string RefreshToken { get; set; }
}