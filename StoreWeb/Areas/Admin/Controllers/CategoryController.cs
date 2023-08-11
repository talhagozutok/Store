using Entities.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace StoreWeb.Areas.Admin.Controllers;

[Area(areaName: "Admin")]
[Authorize(Roles = "Admin")]
public class CategoryController : Controller
{
    private readonly IServiceManager _manager;

    public CategoryController(IServiceManager manager)
    {
        _manager = manager;
    }

    [Route("/[area]/[controller]", Order = 0)]
    [Route("/[area]/Categories", Order = 1)]
    public IActionResult Index()
    {
        var categories = _manager
            .CategoryService
            .GetAllCategories(trackChanges: false);

        return View(categories);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([FromForm] CategoryDtoForInsertion categoryDto)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        _manager.CategoryService.CreateCategory(categoryDto);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Update([FromRoute(Name = "id")] int id)
    {
        var model = _manager
            .CategoryService
            .GetOneCategoryForUpdate(id, trackChanges: false);

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Update([FromForm] CategoryDtoForUpdate categoryDto)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        _manager.CategoryService.UpdateOneCategory(categoryDto);
        return RedirectToAction(
            nameof(Update), 
            new {id = categoryDto.CategoryId});
    }

    [HttpGet]
    public IActionResult Delete([FromRoute(Name = "id")] int id)
    {
        _manager
            .CategoryService
            .DeleteOneCategory(id);

        return RedirectToAction(nameof(Index));
    }
}
