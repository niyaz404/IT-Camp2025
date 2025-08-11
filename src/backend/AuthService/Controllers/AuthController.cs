using System.Security.Authentication;
using AuthService.BLL.Models;
using AuthService.BLL.Services.Interface;
using AuthService.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Share;
using Share.Enums;
using Share.Exceptions;
using Share.Models;
using ILogger = Share.Services.Interface.ILogger;

namespace AuthService.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class AuthController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly IAuthService _authService;
    
    public AuthController(ILogger logger, IMapper mapper, IAuthService authService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _authService = authService ?? throw new ArgumentNullException(nameof(authService));
    }
    
    /// <summary>
    /// Регистрация пользователя
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Register([FromBody]UserModel user)
    {
        try
        {
            await _authService.Register(_mapper.Map<UserModel>(user));
            var token = await _authService.GenerateToken(
                new UserCredentialsModel() { Login = user.Login, Password = user.Password });
            return Ok(new { Token = token }); 
            
        }
        catch (UserAlreadyExistsException e)
        {
            _logger.Log(e.Message);
            return Unauthorized(new ResponseError(ErrorCode.UserAlreadyExists, e.Message));
        }
        catch (Exception e)
        {
            _logger.Log(e.Message);
            return BadRequest();
        }
    }

    /// <summary>
    /// Генерация токена доступа
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> GenerateToken([FromBody] UserCredentialsDto userCredentialsDto)
    {
        try
        {
            var token = await _authService.GenerateToken(_mapper.Map<UserCredentialsModel>(userCredentialsDto));

            return Ok(new { Token = token });
        }
        catch (InvalidPasswordException e)
        {
            _logger.Log(e.Message);
            return Unauthorized(new ResponseError(ErrorCode.InvalidPassword, e.Message));
        }
        catch (UserNotFoundException e)
        {
            _logger.Log(e.Message);
            return Unauthorized(new ResponseError(ErrorCode.UserNotFound, e.Message));
        }
        catch (Exception e)
        {
            _logger.Log(e.Message);
            return BadRequest();
        }
    }

    /// <summary>
    /// Генерация токена доступа
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> ResetPassword([FromBody] UserCredentialsDto userCredentialsDto)
    {
        try
        {
            await _authService.ResetPassword(_mapper.Map<UserCredentialsModel>(userCredentialsDto));

            return Ok();
        }
        catch (Exception e)
        {
            _logger.Log(e.Message);
            return BadRequest();
        }
    }
}