using AutoMapper;
using DataManagerService.BLL.Models.Stands;
using DataManagerService.BLL.Services.Interfaces.Stands;
using DataManagerService.DAL.Repositories.Interface;
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
    
    public StandService(ILogger logger, IMapper mapper, IStandsRepository standsRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _standsRepository = standsRepository ?? throw new ArgumentNullException(nameof(standsRepository));
    }
    
    /// <summary>
    /// Получить список стендов
    /// </summary>
    public async Task<IEnumerable<StandModel>> GetAllAsync()
    {
        try
        {
            var stands = await _standsRepository.SelectAllAsync();
            var result = _mapper.Map<List<StandModel>>(stands);

            return result;
        }
        catch (Exception e)
        {
            _logger.Log(e);
            throw;
        }
    }
}