using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Share.Services.Interface;
using WebApi.BLL.Services.Interface.Stands;
using WebApi.Models.Stands;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class StandsController : ControllerBase
{
    private readonly ILogger _logger;
    private readonly IMapper _mapper;
    private readonly IStandService _standService;
    
    public StandsController(ILogger logger, IMapper mapper, IStandService standService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _standService = standService ?? throw new ArgumentNullException(nameof(standService));
    }
    
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IEnumerable<StandDto>>> GetAll()
    {
        try
        {
            var stands = await _standService.GetAll();
            var result = _mapper.Map<IEnumerable<StandDto>>(stands);
            
            return Ok(result);
        }
        catch (Exception e)
        {
            _logger.Log(e.Message);
            return BadRequest();
        }
    }
}