using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class CartItem
{
    public int CartItemId { get; set; }
    public Product Product { get; set; } = new();
    public int Quantity { get; set; }

    [NotMapped]
    public decimal SubTotal => Product.Price * Quantity;
}
