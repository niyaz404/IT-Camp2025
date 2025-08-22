namespace DataManagerService.Models.Users;

/// <summary>
/// Основная информация пользователя
/// </summary>
public class UserInfoDto
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public string Id { get; init; }
    
    /// <summary>
    /// ФИО пользователя
    /// </summary>
    public string Username { get; init; }
    
    /// <summary>
    /// Роли пользователя
    /// </summary>
    public string[] Roles { get; init; }
}