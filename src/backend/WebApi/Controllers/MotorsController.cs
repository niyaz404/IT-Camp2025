using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.BLL.Services.Interfaces.Motors;
using WebApi.Models.Motors;
using ILogger = Share.Services.Interface.ILogger;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
[Route("api/[controller]")]
public class MotorsController : ControllerBase
{
    private readonly ILogger _logger;
    private readonly IMapper _mapper;
    private readonly IMotorService _motorService;
    
    public MotorsController(ILogger logger, IMapper mapper, IMotorService motorService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _motorService = motorService ?? throw new ArgumentNullException(nameof(motorService));
    }
    
    [HttpGet]
    [Authorize]
    [Route("[action]")]
    public async Task<ActionResult<IEnumerable<MotorDto>>> GetAll(long standId)
    {
        try
        {
            var motors = await _motorService.GetMotorsByStandIdAsync(standId);
            var result = _mapper.Map<IEnumerable<MotorDto>>(motors);
            
            return Ok(result);
        }
        catch (Exception e)
        {
            _logger.Log(e.Message);
            return BadRequest();
        }
    }
    
    [HttpGet]
    [Authorize]
    [Route("{id:long}")]
    public async Task<ActionResult<MotorWithDefectsDto>> Get(long id)
    {
        try
        {
            var motor = await _motorService.GetMotorByIdAsync(id);
            var result = _mapper.Map<MotorWithDefectsDto>(motor);
            
            return Ok(result);
        }
        catch (Exception e)
        {
            _logger.Log(e.Message);
            return BadRequest();
        }
    }
}