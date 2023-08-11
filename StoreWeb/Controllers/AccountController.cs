using Entities.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StoreWeb.Models;

namespace StoreWeb.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public IActionResult Login([FromQuery(Name = "ReturnUrl")] string returnUrl = "/")
    {
        return View(new LoginModel()
        {
            ReturnUrl = returnUrl
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login([FromForm] LoginModel model)
    {
        if (ModelState.IsValid)
        {
            IdentityUser? user = await _userManager.FindByNameAsync(model.UserName!);
            if (user is not null)
            {
                await _signInManager.SignOutAsync();
                if ((await _signInManager.PasswordSignInAsync(user, model.Password!, false, false)).Succeeded)
                {
                    return LocalRedirect(model?.ReturnUrl ?? "/");
                }
                ModelState.AddModelError("Error", "Invalid username or password.");
            }
        }

        return View();
    }

    public async Task<IActionResult> Logout([FromQuery(Name = "ReturnUrl")] string returnUrl = "/")
    {
        await _signInManager.SignOutAsync();
        return LocalRedirect(returnUrl);
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register([FromForm] RegisterDto model)
    {
        var user = new IdentityUser
        {
            UserName = model.UserName,
            Email = model.Email
        };

        var result = await _userManager
            .CreateAsync(user, model.Password!);

        if (result.Succeeded)
        {
            var roleResult = await _userManager
                .AddToRoleAsync(user, "User");

            if (roleResult.Succeeded)
            {
                return RedirectToAction(
                    nameof(Login), 
                    new { ReturnUrl = "/" });
            }
        }
        else
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }

        return View();
    }

    public IActionResult AccessDenied([FromQuery(Name = "ReturnUrl")] string returnUrl)
    {
        return View();
    }
}
