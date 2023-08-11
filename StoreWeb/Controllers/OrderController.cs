using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace StoreWeb.Controllers;

public class OrderController : Controller
{
    private readonly IServiceManager _manager;

    private readonly Cart _cart;

    public OrderController(IServiceManager manager, Cart cart)
    {
        _manager = manager;
        _cart = cart;
    }

    [Authorize]
    public ViewResult Checkout() => View(new Order());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Checkout([FromForm] Order order)
    {
        if (_cart.Items.Count == 0)
        {
            ModelState.AddModelError("", "Sorry your cart is empty.");
        }

        if (ModelState.IsValid)
        {
            order.Items = _cart.Items.ToArray();
            _manager.OrderService.SaveOrder(order);
            _cart.Clear();

            return RedirectToPage("/Complete", new { OrderId = order.OrderId });
        }

        return View();
    }
}
