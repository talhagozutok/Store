using Entities.Dtos;
using Entities.RequestParameters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services.Contracts;
using StoreWeb.Models;

namespace StoreWeb.Areas.Admin.Controllers;

[Area(areaName: "Admin")]
[Authorize(Roles = "Admin")]
public class ProductController : Controller
{
    private readonly IServiceManager _manager;

    public ProductController(IServiceManager manager)
    {
        _manager = manager;
    }

    private SelectList GetCategoriesSelectList()
    {
        var categories = new SelectList(
            items: _manager
                .CategoryService
                .GetAllCategories(trackChanges: false),
            dataValueField: "CategoryId",
            dataTextField: "CategoryName",
            selectedValue: "1"
        );

        return categories;
    }

    [Route("[area]/[controller]", Order = 0)]
    [Route("[area]/Products", Order = 1)]
    public IActionResult Index(ProductRequestParameters p)
    {
        var products = _manager
            .ProductService
            .GetAllProductsWithDetails(p);

        var pagination = new Pagination()
        {
            CurrentPage = p.PageNumber,
            ItemsPerPage = p.PageSize,
            TotalItems = _manager.ProductService.GetAllProducts(false).Count()
        };

        return View(new ProductListViewModel()
        {
            Products = products,
            Pagination = pagination
        });
    }

    public IActionResult Create()
    {
        ViewBag.Categories = GetCategoriesSelectList();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([FromForm] ProductDtoForInsertion productDto, IFormFile file)
    {
        ViewBag.Categories = GetCategoriesSelectList();

        if (ModelState.IsValid)
        {
            // File operations
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", file.FileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            productDto.ImageUrl = string.Concat("/images/", file.FileName);

            _manager.ProductService.CreateProduct(productDto);

            TempData["success"] = $"{productDto.ProductName} has been created.";

            return RedirectToAction(nameof(Index));
        }

        return View();
    }

    public IActionResult Update([FromRoute(Name = "id")] int id)
    {
        var model = _manager
            .ProductService
            .GetOneProductForUpdate(id, trackChanges: false);
        ViewBag.Categories = GetCategoriesSelectList();

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update([FromForm] ProductDtoForUpdate productDto, IFormFile file)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Categories = GetCategoriesSelectList();
            return View(productDto);
        }

        // File operations
        string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", file.FileName);

        using (var stream = new FileStream(path, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        productDto.ImageUrl = string.Concat("/images/", file.FileName);

        _manager.ProductService.UpdateOneProduct(productDto);
        TempData["success"] = $"{productDto.ProductName} has been updated.";

        return RedirectToAction(
            nameof(Index),
            new ProductRequestParameters());
    }

    [HttpGet]
    public IActionResult Delete([FromRoute(Name = "id")] int id)
    {
        _manager.ProductService.DeleteOneProduct(id);
        TempData["danger"] = $"The product has been removed.";

        return RedirectToAction(nameof(Index));
    }
}
