using Microsoft.AspNetCore.Mvc;

namespace StoreWeb.Components;

public class ProductFilterMenu : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}
