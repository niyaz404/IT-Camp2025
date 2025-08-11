namespace AuthService.DAL.Models;

/// <summary>
/// Сущность информации пользователя
/// </summary>
public class UserInfoEntity
{
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Фио пользователя
    /// </summary>
    public string Username { get; set; }
    
    /// <summary>
    /// Роль пользователя
    /// </summary>
    public string Roles { get; set; } 
}