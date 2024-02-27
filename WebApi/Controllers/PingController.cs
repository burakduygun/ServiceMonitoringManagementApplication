using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Logging;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PingController : ControllerBase
    {
        private readonly AbstractLogger _logger;
        public PingController(AbstractLogger logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string Ping()
        {
            _logger.Info("Ping get isteği geldi.");

            return "Ping başarılı";
        }
    }
}
