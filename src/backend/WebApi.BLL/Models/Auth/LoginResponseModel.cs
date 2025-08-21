namespace WebApi.BLL.Models.Auth;

/// <summary>
/// Модель ответа авторизауии
/// </summary>
public class LoginResponseModel
{
    /// <summary>
    /// Токен авторизации
    /// </summary>
    public string AccessToken { get; set; }
    
    /// <summary>
    /// Токен авторизации
    /// </summary>
    public string RefreshToken { get; set; }
    
    /// <summary>
    /// Роли пользователя
    /// </summary>
    public string[] Roles { get; set; }
}