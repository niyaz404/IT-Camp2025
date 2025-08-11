using System.Security.Claims;
using AuthService.BLL.Services.Interface;
using AuthService.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ILogger = Share.Services.Interface.ILogger;

namespace AuthService.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class UsersController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly IUserService _userService;
    
    public UsersController(ILogger logger, IMapper mapper, IUserService userService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
    }
    
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<UserInfoDto>> Me()
    {
        try
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId is null)
            {
                return Unauthorized();
            }

            var user = await _userService.GetInfoByIdAsync(Guid.Parse(userId));
            if (user is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<UserInfoDto>(user));
        }
        catch (Exception e)
        {
            _logger.Log(e.Message);
            return BadRequest();
        }
    }
}