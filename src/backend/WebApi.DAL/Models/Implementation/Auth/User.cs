namespace WebApi.DAL.Models.Implementation.Auth;

/// <summary>
/// Модель данных пользователя
/// </summary>
public class User
{
    /// <summary>
    /// ФИО пользователя
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