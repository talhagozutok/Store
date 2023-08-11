using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace StoreWeb.Components;

public class OrdersInProgress : ViewComponent
{
    private readonly IServiceManager _manager;

    public OrdersInProgress(IServiceManager manager)
    {
        _manager = manager;
    }

    public string Invoke()
    {
        var ordersInProgress = _manager.
            OrderService
            .NumberOfInProcess.ToString();

        return ordersInProgress;
    }
}
