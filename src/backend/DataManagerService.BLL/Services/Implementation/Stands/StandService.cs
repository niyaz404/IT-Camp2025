using AutoMapper;
using DataManagerService.BLL.Models.Stands;
using DataManagerService.BLL.Models.Users;
using DataManagerService.BLL.Services.Interfaces.Stands;
using DataManagerService.DAL.Repositories.Interface;
using DataManagerService.DAL.Repositories.Interface.Keycloak;
using DataManagerService.DAL.Repositories.Interface.Stands;
using Share.Services.Interface;

namespace DataManagerService.BLL.Services.Implementation.Stands;

/// <summary>
/// Сервис для работы со стендами
/// </summary>
public class StandService : IStandService
{
    private readonly ILogger _logger;
    private readonly IMapper _mapper;
    private readonly IStandsRepository _standsRepository;
    private readonly IKeycloakProvider _keycloakProvider;
    
    public StandService(ILogger logger, IMapper mapper, IStandsRepository standsRepository, IKeycloakProvider keycloakProvider)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _standsRepository = standsRepository ?? throw new ArgumentNullException(nameof(standsRepository));
        _keycloakProvider = keycloakProvider ?? throw new ArgumentNullException(nameof(keycloakProvider));
    }
    
    /// <summary>
    /// Получить список стендов
    /// </summary>
    public async Task<IEnumerable<StandModel>> GetAllAsync()
    {
        try
        {
            var standEntities = await _standsRepository.SelectAllAsync();
            
            var users = await _keycloakProvider.GetUsersAsync();
            var result = _mapper.Map<List<StandModel>>(standEntities);

            foreach (var stand in result)
            {
                var entity = standEntities.First(x => x.Id == stand.Id);
                var responsiblePerson = _mapper.Map<KeycloakUserModel>(entity.ResponsiblePersonId.HasValue
                    ? await _keycloakProvider.GetUserAsync(entity.ResponsiblePersonId.Value)
                    : users?.LastOrDefault());
                stand.ResponsiblePerson = responsiblePerson;
            }

            return result;
        }
        catch (Exception e)
        {
            _logger.Log(e);
            throw;
        }
    }
}