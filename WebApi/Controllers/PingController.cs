using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PingController : ControllerBase
    {
        private readonly ILogger _logger;
        public PingController(ILogger logger)
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
