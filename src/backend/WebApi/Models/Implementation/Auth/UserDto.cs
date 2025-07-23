namespace WebApi.Models.Implementation.Auth;

/// <summary>
/// Dto данных пользователя
/// </summary>
public class UserDto
{
    /// <summary>
    /// ФИО
    /// </summary>
    public string UserName { get; set; }
    
    /// <summary>
    /// Логин пользователя
    /// </summary>
    public string Login { get; set; }
    
    /// <summary>
    /// Пароль пользователя
    /// </summary>
    public string Password { get; set; }
}