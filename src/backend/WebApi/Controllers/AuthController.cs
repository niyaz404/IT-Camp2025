using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Share.Enums;
using Share.Exceptions;
using WebApi.BLL.Models.Implementation.Auth;
using Share.Services.Interface;
using WebApi.BLL.Services.Interfaces.Auth;
using WebApi.Models.Auth;
using WebApi.Models.Common;

namespace WebApi.Controllers
{
    /// <summary>
    /// Контроллер авторизации
    /// </summary>
    [ApiController]
    [Route("[controller]/[action]")]
    [Route("api/[controller]/[action]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public AuthController(ILogger logger, IMapper mapper, IAuthService authService)
        {
            _logger = logger;
            _mapper = mapper;
            _authService = authService;
        }
        
        /// <summary>
        /// Метод авторизации
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<LoginResponseDto>> Login([FromBody] UserCredentialsDto userCredentials)
        {
            try
            {
                var result = await _authService.Login(_mapper.Map<UserCredentialsModel>(userCredentials));
                
                Response.Cookies.Append("refreshToken", result.RefreshToken, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = false,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTime.UtcNow.AddDays(7)
                });
                return new LoginResponseDto(result.AccessToken);
            }
            catch (UserNotFoundException e)
            {
                _logger.Log(e);
                return Unauthorized(new BffApiError(
                    ErrorCode.InvalidPassword.ToString(),
                    "Неверный логин или пароль"
                ));
            }
            catch (InvalidPasswordException e)
            {
                _logger.Log(e);
                return Unauthorized(new BffApiError(
                    ErrorCode.InvalidPassword.ToString(),
                    "Неверный логин или пароль"
                ));
            }
            catch (Exception e)
            {
                _logger.Log(e);
                return BadRequest(new BffApiError(
                    ErrorCode.UnknownError.ToString(),
                    "Произошла непредвиденная ошибка"
                ));
            }
        }
        
        /// <summary>
        /// Метод авторизации
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<LoginResponseDto>> Register([FromBody] UserDto user)
        {
            try
            {
                var result = await _authService.Register(_mapper.Map<UserModel>(user));
                
                Response.Cookies.Append("refreshToken", result.RefreshToken, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = false,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTime.UtcNow.AddDays(7)
                });
                
                return new LoginResponseDto(result.AccessToken);
            }
            catch (UserAlreadyExistsException e)
            {
                _logger.Log(e);
                return BadRequest(new BffApiError(
                    ErrorCode.UserAlreadyExists.ToString(),
                    "Пользователь с таким логином уже существует"
                ));
            }
            catch (Exception e)
            {
                _logger.Log(e);
                return BadRequest(new BffApiError(
                    ErrorCode.UnknownError.ToString(),
                    "Произошла непредвиденная ошибка"
                ));
            }
        }
        
        /// <summary>
        /// Сброс пароля
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> ResetPassword([FromBody] UserCredentialsDto userCredentials)
        {
            try
            {
                await _authService.ResetPassword(_mapper.Map<UserCredentialsModel>(userCredentials));
                return Ok();
            }
            catch (UserNotFoundException e)
            {
                _logger.Log(e);
                return Unauthorized(new BffApiError(
                    ErrorCode.UserNotFound.ToString(),
                    "Пользователь не найден"
                ));
            }
            catch (Exception e)
            {
                _logger.Log(e);
                return BadRequest(e.Message);
            }
        }
        
        [HttpPost]
        public async Task<ActionResult<LoginResponseDto>> RefreshToken()
        {
            try
            {
                if (!Request.Cookies.TryGetValue("refreshToken", out var refreshToken))
                {
                    return Unauthorized(new BffApiError(ErrorCode.InvalidRefreshToken.ToString(), "Нет refresh токена"));
                }

                var tokens = await _authService.RefreshToken(refreshToken);

                Response.Cookies.Append("refreshToken", tokens.RefreshToken, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTime.UtcNow.AddDays(7)
                });

                return Ok(new { accessToken = tokens.AccessToken });
            }
            catch
            {
                return Unauthorized(new BffApiError(ErrorCode.InvalidRefreshToken.ToString(), "Refresh токен недействителен"));
            }
        }
    }
}