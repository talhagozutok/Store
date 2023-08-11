using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace StoreWeb.Areas.Admin.Controllers;

[Area(areaName: "Admin")]
[Authorize(Roles = "Admin")]
public class RoleController : Controller
{
    private readonly IServiceManager _manager;

    public RoleController(IServiceManager manager)
    {
        _manager = manager;
    }

    [Route("[area]/[controller]", Order = 0)]
    [Route("[area]/Roles", Order = 1)]
    public IActionResult Index()
    {
        return View(_manager.AuthService.Roles);
    }
}
