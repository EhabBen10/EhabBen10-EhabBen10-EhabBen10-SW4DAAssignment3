using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SW4DAAssignment3.Data;
using SW4DAAssignment3.Models;
using SW4DAAssignment3.Services;

namespace SW4DAAssignment3.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SeedController : ControllerBase
    {
        private readonly SeedService _seedService;
        private readonly ILogger<SeedController> _logger;
        public SeedController(SeedService seedService, ILogger<SeedController> logger)
        {
            _seedService = seedService;
            _logger = logger;
        }

        [HttpPut(Name = "Seed")]
        [ResponseCache(NoStore = true)]
        public async Task<IActionResult> Put()
        {
            if (await _seedService.SeedDb() == true)
            {
                var timestamp = new DateTimeOffset(DateTime.UtcNow);
                var loginfo = new Loginfo
                {
                    specificUser = User.Identity?.Name,
                    Operation = "Seed Ingredient",
                    Timestamp = timestamp.DateTime
                };
                _logger.LogInformation("Get called {@LogInfo} ", loginfo);
                return Ok("Database seeded successfully");
            }
            else
                return BadRequest("Can not seed the database.Could be because there is already data in the database.");


        }
    }
}