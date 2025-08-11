using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Share.Services.Interface;

namespace WebApi.Controllers
{
    /// <summary>
    /// Контроллер для проверки работоспособности сервиса
    /// </summary>
    [ApiController]
    [Route("[controller]/[action]")]
    [Route("api/[controller]/[action]")]
    public class HealthController : ControllerBase
    {
        private readonly ILogger _logger;
        
        public HealthController(ILogger logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// Проверка работоспособности сервиса
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<string>> Check()
        {
            _logger.Log("Checking health status");
            return Ok("Ok");
        }
    }
}