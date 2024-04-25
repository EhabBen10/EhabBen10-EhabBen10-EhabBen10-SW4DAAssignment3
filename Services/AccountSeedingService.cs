using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using SW4DAAssignment3.Data;
using SW4DAAssignment3.DTO;
using SW4DAAssignment3.Models;

namespace SW4DAAssignment3.Services;

public class AccountSeedingService
{

    private readonly BakeryDBcontext _context;
    private readonly UserManager<BakeryUser> _userManager;
    private readonly SignInManager<BakeryUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AccountSeedingService(
        BakeryDBcontext context,
        IConfiguration configuration,
        UserManager<BakeryUser> userManager,
        SignInManager<BakeryUser> signInManager,
        RoleManager<IdentityRole> roleManager
        )
    {
        _context = context;
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
    }


    public async Task<bool> CreateAdmin(RegisterDTO model)
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
                    return false;
                }
            }
            // Add the "Admin" role to the user
            await _userManager.AddToRoleAsync(user, "Admin");
            return true;
        }

        return false;
    }

    public async Task<bool> CreateManager(RegisterDTO model)
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
                    return false;
                }
            }
            await _userManager.AddToRoleAsync(user, "Manager");


            return true;
        }

        return false;
    }

    public async Task<bool> CreateBaker(RegisterDTO model)
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
                    return false;
                }
            }
            await _userManager.AddToRoleAsync(user, "Baker");
            return true;
        }

        return false;
    }

    public async Task<bool> CreateDriver(RegisterDTO model)
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
                    return false;
                }
            }
            await _userManager.AddToRoleAsync(user, "Driver");
            return true;
        }

        return false;
    }

    public async Task<bool> SeedUsers()
    {
        if (await _userManager.FindByNameAsync("admin") != null)
        {
            return false;
        }
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
        if (await CreateAdmin(admin) != false &&
        await CreateManager(manager) != false &&
        await CreateBaker(baker) != false &&
        await CreateDriver(driver) != false)
        {
            return true;
        }
        return false;
    }
}