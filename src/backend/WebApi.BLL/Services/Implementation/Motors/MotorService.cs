using AutoMapper;
using Share.Services.Interface;
using WebApi.BLL.Models.Motors;
using WebApi.BLL.Services.Interfaces.Motors;
using WebApi.DAL.Providers.Interface;

namespace WebApi.BLL.Services.Implementation.Motors;

public class MotorService : IMotorService
{
    private readonly ILogger _logger;
    private readonly IMapper _mapper;
    private readonly IMotorProvider _motorProvider;
    
    public MotorService(ILogger logger, IMapper mapper, IMotorProvider motorProvider)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _motorProvider = motorProvider ?? throw new ArgumentNullException(nameof(motorProvider));
    }
    
    public async Task<MotorWithDefectsModel> GetMotorByIdAsync(long id)
    {
        try
        {
            var motor = await _motorProvider.GetByIdAsync(id);
            var result = _mapper.Map<MotorWithDefectsModel>(motor);

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
            var motors = await _motorProvider.GetByStandIdAsync(standId);
            var result = _mapper.Map<List<MotorModel>>(motors);

            return result;
        }
        catch (Exception e)
        {
            _logger.Log(e);
            throw;
        }
    }
}