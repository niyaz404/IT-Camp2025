using AutoMapper;
using Share.Services.Interface;
using WebApi.BLL.Models.Motors;
using WebApi.BLL.Models.Stands;
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
    private readonly IMotorProvider _motorProvider;
    
    public StandService(ILogger logger, IMapper mapper, IStandProvider standProvider, IMotorProvider motorProvider)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _standProvider = standProvider ?? throw new ArgumentNullException(nameof(standProvider));
        _motorProvider = motorProvider ?? throw new ArgumentNullException(nameof(motorProvider));
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

    /// <summary>
    /// Получить стенд
    /// </summary>
    /// <returns></returns>
    public async Task<StandWithMotorsModel> GetById(long id)
    {
        try
        {
            var stand = (await _standProvider.GetAllAsync()).FirstOrDefault(s => s.Id == id);

            if (stand == null)
            {
                return null;
            }
            
            var result = _mapper.Map<StandWithMotorsModel>(stand);
            result.Motors = _mapper.Map<List<MotorModel>>(await _motorProvider.GetByStandIdAsync(id));

            return result;
        }
        catch (Exception e)
        {
            _logger.Log(e);
            throw;
        }
    }
}