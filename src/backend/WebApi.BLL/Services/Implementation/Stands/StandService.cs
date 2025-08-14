using AutoMapper;
using Share.Services.Interface;
using WebApi.BLL.Models.Implementation.Stands;
using WebApi.BLL.Services.Interfaces.Stands;
using WebApi.DAL.Providers.Interface;

namespace WebApi.BLL.Services.Implementation.Stands;

/// <summary>
/// Сервис для работы со стендами
/// </summary>
public class StandService : IStandService
{
    private readonly ILogger _logger;
    private readonly IMapper _mapper;
    private readonly IStandProvider _standProvider;
    
    public StandService(ILogger logger, IMapper mapper, IStandProvider standProvider)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _standProvider = standProvider ?? throw new ArgumentNullException(nameof(standProvider));
    }
    
    /// <summary>
    /// Получить список стендов
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<StandModel>> GetAll()
    {
        try
        {
            var stands = await _standProvider.GetAllAsync();
            var result = _mapper.Map<IEnumerable<StandModel>>(stands);

            return result;
        }
        catch (Exception e)
        {
            _logger.Log(e);
            throw;
        }
    }
}