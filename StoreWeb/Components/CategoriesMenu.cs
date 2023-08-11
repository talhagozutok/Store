using Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace StoreWeb.Components;

public class CategoriesMenu : ViewComponent
{
    private readonly IServiceManager _manager;

    public CategoriesMenu(IServiceManager manager)
    {
        _manager = manager;
    }

    public IViewComponentResult Invoke()
    {
        var categories = _manager
            .CategoryService
            .GetAllCategories(trackChanges: false);

        return View(categories);
    }
}
