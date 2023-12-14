using Microsoft.AspNetCore.Mvc;

namespace DatabaseLogTest.Controllers
{
    [Route("api/databaselogtest")]
    [ApiController]
    public class TestDatabaseController : ControllerBase
    {
        private readonly ILogger<TestDatabaseController> _logger;

        public TestDatabaseController(ILogger<TestDatabaseController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("test")]
        public IActionResult TestEndPoint()
        {
            _logger.LogInformation("I should always see this information message.");
            _logger.LogDebug("I should only see this debug message if the switch is set to debug.");

            return Ok("Thanks");
        }
    }
}
