using Entities.RequestParameters;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using StoreWeb.Models;

namespace StoreWeb.Controllers;

public class ProductController : Controller
{
    private readonly IServiceManager _manager;

    public ProductController(IServiceManager manager)
    {
        _manager = manager;
    }

    public IActionResult Index([FromQuery] ProductRequestParameters p)
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

    public IActionResult Get([FromRoute(Name = "id")] int id)
    {
        var model = _manager
            .ProductService
            .GetOneProduct(id, trackChanges: false);

        return View(model);
    }
}
