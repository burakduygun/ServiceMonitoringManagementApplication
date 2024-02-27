using Microsoft.AspNetCore.Mvc;
using Shared.Logging;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogLevelController : Controller
    {
        private readonly AbstractLogger _logger;
        public LogLevelController(AbstractLogger logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string Get()
        {
            return _logger.GetLogLevel().ToString();
        }

        [HttpPost]
        public IActionResult Post(string logLevel)
        {
            try
            {
                var level = Enum.Parse<Shared.Logging.LogLevel>(logLevel);
                _logger.SetLogLevel(level);
            }
            catch (Exception)
            {
                return BadRequest("Yanlış giriş. Geçerli bir LogLevel girmediniz.");
            }

            return Ok();
        }
    }
}
