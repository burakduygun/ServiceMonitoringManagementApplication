using Microsoft.AspNetCore.Mvc;
using Shared.Logging;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SumController : ControllerBase
    {
        private readonly ILogger<SumController> _logger;

        public SumController(ILogger<SumController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public int Sum(int x, int y)
        {
            _logger.LogInformation("Sum get isteği geldi.");
            return x + y;
        }
    }
}
