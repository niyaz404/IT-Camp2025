namespace WebApi.BLL.Models.Implementation.Auth;

/// <summary>
/// Модель ответа авторизауии
/// </summary>
public class LoginResponseModel
{
    /// <summary>
    /// Токен авторизации
    /// </summary>
    public string Token { get; set; }
    
    /// <summary>
    /// Роли пользователя
    /// </summary>
    public string[] Roles { get; set; }
}