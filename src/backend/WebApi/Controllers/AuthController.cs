using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.BLL.Models.Implementation.Auth;
using WebApi.BLL.Services.Interface.Auth;
using WebApi.Models.Implementation.Auth;

namespace WebApi.Controllers
{
    /// <summary>
    /// Контроллер авторизации
    /// </summary>
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public AuthController( IAuthService authService, IMapper mapper)
        {
            _authService = authService;
            _mapper = mapper;
        }
        
        /// <summary>
        /// Метод авторизации
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<string>> Login([FromBody] UserCredentialsDto userCredentials)
        {
            try
            {
                var result = await _authService.Login(_mapper.Map<UserCredentialsModel>(userCredentials));
                return result.Token;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        /// <summary>
        /// Метод авторизации
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<string>> Register([FromBody] UserDto user)
        {
            try
            {
                var result = await _authService.Register(_mapper.Map<UserModel>(user));
                return result.Token;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
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
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}