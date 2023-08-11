using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;

namespace Repositories;

public class OrderRepository :
    RepositoryBase<Order>,
    IOrderRepository
{
    public OrderRepository(RepositoryContext context) : base(context)
    {
    }

    public IQueryable<Order> Orders
        => _context
            .Orders!
            .Include(o => o.Items)
            .ThenInclude(cartItem => cartItem.Product)
            .OrderBy(o => o.Shipped)
            .ThenByDescending(o => o.OrderId);

    public int NumberOfInProcess
        => _context
            .Orders!
            .Count(o => o.Shipped.Equals(false));

    public void Complete(int id)
    {
        var order = FindByCondition(
            o => o.OrderId.Equals(id),
            trackChanges: true
        );

        ArgumentNullException.ThrowIfNull(order, "Order could not found!");

        order.Shipped = true;
    }

    public Order? GetOneOrder(int id)
    {
        return FindByCondition(o => o.OrderId.Equals(id), trackChanges: false);
    }

    public void SaveOrder(Order order)
    {
        _context.AttachRange(order.Items.Select(item => item.Product));

        if (order.OrderId == 0)
        {
            _context.Orders?.Add(order);
        }

        _context.SaveChanges();
    }
}
