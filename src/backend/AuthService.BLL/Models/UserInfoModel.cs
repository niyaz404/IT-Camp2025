namespace AuthService.BLL.Models;

/// <summary>
/// Основная информация пользователя
/// </summary>
public class UserInfoModel
{
    public string Id { get; init; }
    
    public string Username { get; init; }
    
    public string[] Roles { get; init; }
}