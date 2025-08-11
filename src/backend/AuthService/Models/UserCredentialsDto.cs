namespace AuthService.Models;

/// <summary>
/// Модель кредов пользователя
/// </summary>
public class UserCredentialsDto
{
    /// <summary>
    /// Логин пользователя
    /// </summary>
    public string Login { get; set; }
    
    /// <summary>
    /// Пароль пользователя
    /// </summary>
    public string Password { get; set; }
}