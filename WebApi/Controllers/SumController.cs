using Microsoft.AspNetCore.Mvc;
using Shared.Logging;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SumController : ControllerBase
    {
        private readonly AbstractLogger _logger;
        public SumController(AbstractLogger logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public int Sum(int x, int y)
        {
            _logger.Info("Sum get isteği geldi.");

            return x + y;
        }

    }
}
