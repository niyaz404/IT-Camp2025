namespace WebApi.DAL.Models.Implementation.Users;

public class UserInfo
{
    public string Id { get; init; }
    
    public string Username { get; init; }
    
    public string[] Roles { get; init; }
}