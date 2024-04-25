using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SW4DAAssignment3.Data;
using SW4DAAssignment3.DTO;
using SW4DAAssignment3.Models;
using SW4DAAssignment3.Services;

namespace SW4DAAssignment3.Controllers;

[Route("[controller]/[action]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<AccountController> _logger;
    private readonly AccountSeedingService _accountSeedingService;
    private readonly UserManager<BakeryUser> _userManager;

    public AccountController(
        IConfiguration configuration,
        ILogger<AccountController> logger,
        AccountSeedingService accountSeedingService,
        UserManager<BakeryUser> userManager)
    {
        _configuration = configuration;
        _logger = logger;
        _accountSeedingService = accountSeedingService;
        _userManager = userManager;
    }

    [HttpPost("CreateAdmin")]
    public async Task<IActionResult> CreateAdmin([FromBody] RegisterDTO model)
    {
        if (await _accountSeedingService.CreateAdmin(model))
        {
            var timestamp = new DateTimeOffset(DateTime.UtcNow);
            var loginfo = new Loginfo
            {
                specificUser = User.Identity?.Name,
                Operation = "post CreateAdmin",
                Timestamp = timestamp.DateTime
            };
            _logger.LogInformation("Get called {@LogInfo} ", loginfo);
            return Ok("Admin user created successfully.");
        }
        else
        {
            return BadRequest("Failed to create admin user.");
        }
    }

    [HttpPost("CreateManager")]
    public async Task<IActionResult> CreateManager([FromBody] RegisterDTO model)
    {
        if (await _accountSeedingService.CreateManager(model))
        {
            var timestamp = new DateTimeOffset(DateTime.UtcNow);
            var loginfo = new Loginfo
            {
                specificUser = User.Identity?.Name,
                Operation = "post CreateManager",
                Timestamp = timestamp.DateTime
            };
            _logger.LogInformation("Get called {@LogInfo} ", loginfo);
            return Ok("Manager user created successfully.");
        }
        else
        {
            return BadRequest("Failed to create Manager user.");
        }

    }

    [HttpPost("CreateBaker")]
    public async Task<IActionResult> CreateBaker([FromBody] RegisterDTO model)
    {
        if (await _accountSeedingService.CreateBaker(model))
        {
            var timestamp = new DateTimeOffset(DateTime.UtcNow);
            var loginfo = new Loginfo
            {
                specificUser = User.Identity?.Name,
                Operation = "post CreateBaker",
                Timestamp = timestamp.DateTime
            };
            _logger.LogInformation("Get called {@LogInfo} ", loginfo);
            return Ok("Baker user created successfully.");
        }
        else
        {
            return BadRequest("Failed to create Baker user.");
        }
    }

    [HttpPost("CreateDriver")]
    public async Task<IActionResult> CreateDriver([FromBody] RegisterDTO model)
    {
        if (await _accountSeedingService.CreateDriver(model))
        {
            var timestamp = new DateTimeOffset(DateTime.UtcNow);
            var loginfo = new Loginfo
            {
                specificUser = User.Identity?.Name,
                Operation = "post CreateDriver",
                Timestamp = timestamp.DateTime
            };
            _logger.LogInformation("Get called {@LogInfo} ", loginfo);
            return Ok("Driver user created successfully.");
        }
        else
        {
            return BadRequest("Failed to create Driver user.");
        }
    }

    [HttpPost("SeedUsers")]
    public async Task<IActionResult> SeedUsers()
    {
        if (await _accountSeedingService.SeedUsers())
        {
            return Ok("Users seeded successfully.");
        }
        else
        {
            return BadRequest("Failed to seed users.");
        }
    }

    [HttpPost]
    public async Task<ActionResult> Login(LoginDTO input)
    {

        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByNameAsync(input.UserName);
            if (user != null && await _userManager.CheckPasswordAsync(user, input.Password))
            {
                var signingCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(
                        System.Text.Encoding.UTF8.GetBytes(_configuration["JWT:SigningKey"])),
                    SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName)
            };

                // Get user's roles and add them to the claims
                var userRoles = await _userManager.GetRolesAsync(user);
                claims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

                var jwtObject = new JwtSecurityToken(
                    issuer: _configuration["JWT:Issuer"],
                    audience: _configuration["JWT:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddSeconds(3000),
                    signingCredentials: signingCredentials);

                var jwtString = new JwtSecurityTokenHandler().WriteToken(jwtObject);

                var timestamp = new DateTimeOffset(DateTime.UtcNow);
                var loginfo = new Loginfo
                {
                    specificUser = input.UserName,
                    Operation = "post Login",
                    Timestamp = timestamp.DateTime
                };
                _logger.LogInformation("Get called {@LogInfo} ", loginfo);

                return StatusCode(StatusCodes.Status200OK, jwtString);
            }
        }

        var details = new ValidationProblemDetails(ModelState)
        {
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            Status = StatusCodes.Status400BadRequest
        };

        return new BadRequestObjectResult(details);
    }

}
