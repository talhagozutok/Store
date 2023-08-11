using Microsoft.AspNetCore.Mvc;
using Repositories.Contracts;

namespace StoreWeb.Controllers;

public class CategoryController : Controller
{
    private IRepositoryManager _manager;

    public CategoryController(IRepositoryManager manager)
    {
        _manager = manager;
    }

    public IActionResult Index()
    {
        var model = _manager
            .Category
            .FindAll(trackChanges: false)
            .ToList();

        return View(model);
    }
}
