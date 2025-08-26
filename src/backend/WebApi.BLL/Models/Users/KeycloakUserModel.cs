namespace WebApi.BLL.Models.Users;

/// <summary>
/// Модель пользователя в кейклок
/// </summary>
public class KeycloakUserModel
{
    public string Id { get; set; }
    
    public string Username { get; set; }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
}