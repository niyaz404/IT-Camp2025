using DataManagerService.DAL.Models.Users;

namespace DataManagerService.DAL.Repositories.Interface.Keycloak;

/// <summary>
/// Провайдер для работы с кейклоком
/// </summary>
public interface IKeycloakProvider
{
    Task<string?> GetServiceTokenAsync();
    
    Task<KeycloakUser> GetUserAsync(Guid userId);
    
    Task<IEnumerable<KeycloakUser>> GetUsersAsync();
}
    