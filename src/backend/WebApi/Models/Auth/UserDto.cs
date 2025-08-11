namespace WebApi.Models.Auth;

/// <summary>
/// Dto данных пользователя
/// </summary>
public class UserDto
{
    /// <summary>
    /// ФИО
    /// </summary>
    public string Username { get; set; }
    
    /// <summary>
    /// Логин пользователя
    /// </summary>
    public string Login { get; set; }
    
    /// <summary>
    /// Пароль пользователя
    /// </summary>
    public string Password { get; set; }
}