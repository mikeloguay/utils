using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace HangfireSample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HangfireController : ControllerBase
    {
        private readonly ILogger<HangfireController> _logger;

        public HangfireController(ILogger<HangfireController> logger)
        {
            _logger = logger;
        }

        [HttpPost(Name = "EnqueueBackgroundJob")]
        public void EnqueueBackgroundJob([FromBody]string message)
        {
            BackgroundJob.Enqueue(() => Console.WriteLine(message));
        }
    }
}