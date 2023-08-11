using Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace StoreWeb.Components;

public class CartSummary : ViewComponent
{
    private readonly Cart _cart;

    public CartSummary(Cart cartService)
    {
        _cart = cartService;
    }

    /* public string Invoke()
    {
        return _cart.Items.Count.ToString();
    } */

    public IViewComponentResult Invoke()
    {
        var cartItemCount = _cart.Items.Count;
        return View(cartItemCount);
    }
}
