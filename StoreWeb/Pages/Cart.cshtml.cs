using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Contracts;

namespace StoreWeb.Pages;

public class CartModel : PageModel
{
    private readonly IServiceManager _manager;
    public Cart Cart { get; set; } // IoC
    public string ReturnUrl { get; set; } = "/";

    public CartModel(IServiceManager manager, Cart cartService)
    {
        _manager = manager;
        Cart = cartService;
    }

    public void OnGet(string returnUrl)
    {
        ReturnUrl = returnUrl ?? "/";
    }

    public IActionResult OnPost(int productId, string returnUrl)
    {
        Product? product = _manager
            .ProductService
            .GetOneProduct(productId, trackChanges: false);

        if (product is not null)
        {
            Cart.AddItem(product, 1);
        }

        return RedirectToPage(new { returnUrl = returnUrl }); // returnUrl
    }

    public IActionResult OnPostRemove(int lineId, string returnUrl)
    {
        Cart.RemoveLine(Cart.Items.First(i => i.Product.ProductId.Equals(lineId)).Product);
        return Page(); // returnUrl
    }
}
