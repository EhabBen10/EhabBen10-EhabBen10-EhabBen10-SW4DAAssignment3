using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SW4DAAssignment3.Data;
using SW4DAAssignment3.DTO;
using SW4DAAssignment3.Models;

namespace SW4DAAssignment3.Controllers;

[Route("[controller]/[action]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly BakeryDBcontext _context;
    private readonly IConfiguration _configuration;
    private readonly UserManager<BakeryUser> _userManager;
    private readonly SignInManager<BakeryUser> _signInManager;

    private readonly ILogger<AccountController> _logger;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AccountController(
        BakeryDBcontext context,
        IConfiguration configuration,
        UserManager<BakeryUser> userManager,
        SignInManager<BakeryUser> signInManager,
        RoleManager<IdentityRole> roleManager,
        ILogger<AccountController> logger)
    {
        _context = context;
        _configuration = configuration;
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _logger = logger;
    }

    [HttpPost("CreateAdmin")]

    public async Task<IActionResult> CreateAdmin([FromBody] RegisterDTO model)
    {
        var user = new BakeryUser
        {
            UserName = model.FullName,
            Email = model.Email,
        };

        var result = _userManager.CreateAsync(user, model.Password).Result;

        if (result.Succeeded)
        {
            // Check if the "Admin" role exists
            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                // If not, create the "Admin" role
                var roleResult = await _roleManager.CreateAsync(new IdentityRole("Admin"));
                if (!roleResult.Succeeded)
                {
                    return BadRequest("Failed to create admin role.");
                }
            }
            // Add the "Admin" role to the user
            await _userManager.AddToRoleAsync(user, "Admin");

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

        return BadRequest("Failed to create admin user.");
    }

    [HttpPost("CreateManager")]
    public async Task<IActionResult> CreateManager([FromBody] RegisterDTO model)
    {
        var user = new BakeryUser
        {
            UserName = model.FullName,
            Email = model.Email,
        };

        var result = _userManager.CreateAsync(user, model.Password).Result;

        if (result.Succeeded)
        {
            if (!await _roleManager.RoleExistsAsync("Manager"))
            {
                var roleResult = await _roleManager.CreateAsync(new IdentityRole("Manager"));
                if (!roleResult.Succeeded)
                {
                    return BadRequest("Failed to create Manager role.");
                }
            }
            await _userManager.AddToRoleAsync(user, "Manager");

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

        return BadRequest("Failed to create Manager user.");
    }

    [HttpPost("CreateBaker")]
    public async Task<IActionResult> CreateBaker([FromBody] RegisterDTO model)
    {
        var user = new BakeryUser
        {
            UserName = model.FullName,
            Email = model.Email,
        };

        var result = _userManager.CreateAsync(user, model.Password).Result;

        if (result.Succeeded)
        {
            if (!await _roleManager.RoleExistsAsync("Baker"))
            {
                var roleResult = await _roleManager.CreateAsync(new IdentityRole("Baker"));
                if (!roleResult.Succeeded)
                {
                    return BadRequest("Failed to create Baker role.");
                }
            }
            await _userManager.AddToRoleAsync(user, "Baker");

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

        return BadRequest("Failed to create Baker user.");
    }

    [HttpPost("CreateDriver")]
    public async Task<IActionResult> CreateDriver([FromBody] RegisterDTO model)
    {
        var user = new BakeryUser
        {
            UserName = model.FullName,
            Email = model.Email,
        };

        var result = _userManager.CreateAsync(user, model.Password).Result;

        if (result.Succeeded)
        {
            if (!await _roleManager.RoleExistsAsync("Driver"))
            {
                var roleResult = await _roleManager.CreateAsync(new IdentityRole("Driver"));
                if (!roleResult.Succeeded)
                {
                    return BadRequest("Failed to create Driver role.");
                }
            }
            await _userManager.AddToRoleAsync(user, "Driver");

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

        return BadRequest("Failed to create Driver user.");
    }

    [HttpPost("SeedUsers")]
    public async Task<IActionResult> SeedUsers()
    {

        var admin = new RegisterDTO
        {
            FullName = "admin",
            Email = "admin@example.com",
            Password = "Adminpassword12?"
        };
        var manager = new RegisterDTO
        {
            FullName = "manager",
            Email = "manager@example.com",
            Password = "Managerpassword12?"
        };
        var baker = new RegisterDTO
        {
            FullName = "baker",
            Email = "baker@example.com",
            Password = "Bakerpassword12?"
        };
        var driver = new RegisterDTO
        {
            FullName = "driver",
            Email = "driver@example.com",
            Password = "Driverpassword12?"
        };
        if (await CreateAdmin(admin) != BadRequest("Failed to create admin user.") &&
        await CreateManager(manager) != BadRequest("Failed to create Manager user.") &&
        await CreateBaker(baker) != BadRequest("Failed to create Baker user.") &&
        await CreateDriver(driver) != BadRequest("Failed to create Driver user."))
        {
            return Ok("Users created successfully.");
        }


        return BadRequest("Failed to create user.");

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
