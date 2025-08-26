using AutoMapper;
using DataManagerService.BLL.Services.Interfaces.Defects;
using DataManagerService.DAL.Repositories.Interface.Defects;
using Share.Services.Interface;

namespace DataManagerService.BLL.Services.Implementation.Defects;

public class MotorDefectService : IMotorDefectService
{
    private readonly ILogger _logger;
    private readonly IMapper _mapper;
    private readonly IMotorDefectsRepository _motorDefectsRepository;
    
    public MotorDefectService(ILogger logger, IMapper mapper, IMotorDefectsRepository motorDefectsRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _motorDefectsRepository = motorDefectsRepository ?? throw new ArgumentNullException(nameof(motorDefectsRepository));
    }
}