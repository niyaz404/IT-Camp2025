using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.BLL.Services.Interface.Users;
using WebApi.Models.Users;
using ILogger = Share.Services.Interface.ILogger;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class UsersController : ControllerBase
{
    private readonly ILogger _logger;
    private readonly IMapper _mapper;
    private readonly IUserService _userService;
    
    public UsersController(ILogger logger, IMapper mapper, IUserService userService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
    }
    
    [HttpGet]
    public async Task<ActionResult<UserInfoDto>> Me()
    {
        try
        {
            var bearerToken = Request.Headers["Authorization"].FirstOrDefault();
            if (bearerToken is null)
            {
                return Unauthorized();
            }

            var user = await _userService.GetCurrentUserInfoAsync(bearerToken);
            if (user is null)
            {
                return NotFound();
            }

            var userInfo = _mapper.Map<UserInfoDto>(user);

            return Ok(userInfo);
        }
        catch (Exception e)
        {
            _logger.Log(e.Message);
            return BadRequest();
        }
    }
}