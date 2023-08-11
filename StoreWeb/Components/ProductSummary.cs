using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace StoreWeb.Components;

public class ProductSummary : ViewComponent
{
    private readonly IServiceManager _manager;

    public ProductSummary(IServiceManager manager)
    {
        _manager = manager;
    }

    public string Invoke()
    {
        var productCount = _manager
            .ProductService
            .GetAllProducts(false)
            .Count().ToString();

        return productCount;
    }
}
