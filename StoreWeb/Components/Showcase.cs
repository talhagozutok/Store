using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace StoreWeb.Components;

public class Showcase : ViewComponent
{
    private readonly IServiceManager _manager;

    public Showcase(IServiceManager manager)
    {
        _manager = manager;
    }

    public IViewComponentResult Invoke(string viewName = "default")
    {
        var products = _manager
            .ProductService
            .GetShowcaseProducts(trackChanges: false);

        return viewName switch
        {
            "List" => View("List", products),
            "MiniCart" => View("MiniCart", products),
            _ => View(products)
        };
    }
}
