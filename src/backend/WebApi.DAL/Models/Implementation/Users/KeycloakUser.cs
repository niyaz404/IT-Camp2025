namespace WebApi.DAL.Models.Implementation.Users;

/// <summary>
/// Модель пользователя в кейклок
/// </summary>
public class KeycloakUser
{
    public string Id { get; set; }
    
    public string Username { get; set; }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
}