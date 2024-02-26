using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SumController : ControllerBase
    {
        private readonly ILogger _logger;
        public SumController(ILogger logger)
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
