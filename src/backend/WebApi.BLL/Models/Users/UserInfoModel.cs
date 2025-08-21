namespace WebApi.BLL.Models.Users;

/// <summary>
/// Основная информация пользователя
/// </summary>
public class UserInfoModel
{
    public string Id { get; init; }
    
    public string Username { get; init; }
    
    public string[] Roles { get; init; }
}