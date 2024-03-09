using Microsoft.AspNetCore.Mvc;
using Shared.Logging;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PingController : ControllerBase
    {
        private readonly ILogger<PingController> _logger;

        public PingController(ILogger<PingController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string Ping()
        {
            _logger.LogInformation("Ping get isteği geldi.");
            return "Ping başarılı";
        }
    }
}
