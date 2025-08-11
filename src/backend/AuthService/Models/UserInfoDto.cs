namespace AuthService.Models;

/// <summary>
/// Информация о пользователе
/// </summary>
public class UserInfoDto
{
    public string Id { get; init; }
    
    public string Username { get; init; }
    
    public string[] Roles { get; init; }
}