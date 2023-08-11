using Entities.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace StoreWeb.Areas.Admin.Controllers;

[Area(areaName: "Admin")]
[Authorize(Roles = "Admin")]
public class UserController : Controller
{
    private readonly IServiceManager _manager;

    public UserController(IServiceManager manager)
    {
        _manager = manager;
    }

    [Route("[area]/[controller]", Order = 0)]
    [Route("[area]/Users", Order = 1)]
    public IActionResult Index()
    {
        var users = _manager.AuthService.GetAllUsers();
        return View(users);
    }

    public IActionResult Create()
    {

        return View(new UserDtoForCreation()
        {
            Roles = new HashSet<string>(
                _manager
                .AuthService
                .Roles
                .Select(r => r.Name)
                .ToList()!
            )
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([FromForm] UserDtoForCreation userDto)
    {
        var result = await _manager
            .AuthService
            .CreateUser(userDto);

        return result.Succeeded
            ? RedirectToAction(nameof(Index))
            : View();
    }

    public async Task<IActionResult> Update([FromRoute(Name = "id")] string id)
    {
        var user = await _manager
            .AuthService
            .GetOneUserForUpdate(id);

        return View(user);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update([FromForm] UserDtoForUpdate userDto)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        await _manager
            .AuthService
            .Update(userDto);

        return RedirectToAction(nameof(Index));
    }

    public IActionResult ChangePassword([FromRoute(Name = "id")] string userId)
    {
        return View(new ChangePasswordDto()
        {
            UserId = userId
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ChangePassword([FromForm] ChangePasswordDto model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var result = await _manager.AuthService.ChangePassword(model);

        return result.Succeeded
            ? RedirectToAction(nameof(Index))
            : View();
    }

    [HttpGet]
    public async Task<IActionResult> Delete([FromRoute(Name = "id")] string userId)
    {
        await _manager.AuthService.DeleteOneUser(userId);
        return RedirectToAction(nameof(Index));
    }
}
