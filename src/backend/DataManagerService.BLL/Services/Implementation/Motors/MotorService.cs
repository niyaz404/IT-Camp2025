using AutoMapper;
using DataManagerService.BLL.Models.Defects;
using DataManagerService.BLL.Models.Motors;
using DataManagerService.BLL.Models.Users;
using DataManagerService.BLL.Services.Interfaces.Motors;
using DataManagerService.DAL.Repositories.Interface.Defects;
using DataManagerService.DAL.Repositories.Interface.Keycloak;
using DataManagerService.DAL.Repositories.Interface.Motors;
using Share.Services.Interface;

namespace DataManagerService.BLL.Services.Implementation.Motors;

public class MotorService : IMotorService
{
    private readonly ILogger _logger;
    private readonly IMapper _mapper;
    private readonly IMotorsRepository _motorsRepository;
    private readonly IMotorDefectsRepository _motorDefectsRepository;
    private readonly IKeycloakProvider _keycloakProvider;
    
    public MotorService(ILogger logger, IMapper mapper, IMotorsRepository motorsRepository, IMotorDefectsRepository motorDefectsRepository, IKeycloakProvider keycloakProvider)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _motorsRepository = motorsRepository ?? throw new ArgumentNullException(nameof(motorsRepository));
        _motorDefectsRepository = motorDefectsRepository ?? throw new ArgumentNullException(nameof(motorDefectsRepository));
        _keycloakProvider = keycloakProvider ?? throw new ArgumentNullException(nameof(keycloakProvider));
    }
    
    public async Task<MotorWithDefectsModel> GetMotorByIdAsync(long id)
    {
        try
        {
            var motorEntity = await _motorsRepository.SelectByIdAsync(id);

            var users = await _keycloakProvider.GetUsersAsync();
            var result = _mapper.Map<MotorWithDefectsModel>(motorEntity);

            result.Defects = _mapper.Map<IEnumerable<MotorDefectModel>>(await _motorDefectsRepository.SelectByMotorIdAsync(result.Id));

            var responsiblePerson = _mapper.Map<KeycloakUserModel>(motorEntity.ResponsiblePersonId.HasValue
                ? await _keycloakProvider.GetUserAsync(motorEntity.ResponsiblePersonId.Value)
                : users?.LastOrDefault());
            result.ResponsiblePerson = responsiblePerson;

            return result;
        }
        catch (Exception e)
        {
            _logger.Log(e);
            throw;
        }
    }

    public async Task<IEnumerable<MotorModel>> GetMotorsByStandIdAsync(long standId)
    {
        try
        {
            var motorEntities = await _motorsRepository.SelectByStandIdAsync(standId);
            
            var users = await _keycloakProvider.GetUsersAsync();
            var result = _mapper.Map<List<MotorModel>>(motorEntities);

            foreach (var motor in result)
            {
                var entity = motorEntities.First(x => x.Id == motor.Id);
                var responsiblePerson = _mapper.Map<KeycloakUserModel>(entity.ResponsiblePersonId.HasValue
                    ? await _keycloakProvider.GetUserAsync(entity.ResponsiblePersonId.Value)
                    : users?.LastOrDefault());
                motor.ResponsiblePerson = responsiblePerson;
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