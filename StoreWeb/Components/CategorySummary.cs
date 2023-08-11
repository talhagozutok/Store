using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace StoreWeb.Components;

public class CategorySummary : ViewComponent
{
    private readonly IServiceManager _manager;

    public CategorySummary(IServiceManager manager)
    {
        _manager = manager;
    }

    public string Invoke()
    {
        var categoryCount = _manager
            .CategoryService
            .GetAllCategories(trackChanges: false)
            .Count().ToString();

        return categoryCount;
    }
}
