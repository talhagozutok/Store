using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace StoreWeb.Areas.Admin.Controllers;

[Area(areaName: "Admin")]
[Authorize(Roles = "Admin")]
public class OrderController : Controller
{
    private readonly IServiceManager _manager;

    public OrderController(IServiceManager manager)
    {
        _manager = manager;
    }

    [Route("[area]/[controller]", Order = 0)]
    [Route("[area]/Orders", Order = 1)]
    public IActionResult Index()
    {
        var orders = _manager.OrderService.Orders;
        return View(orders);
    }

    [HttpPost]
    public IActionResult Complete([FromForm] int id)
    {
        _manager.OrderService.Complete(id);
        return RedirectToAction(nameof(Index));
    }
}
