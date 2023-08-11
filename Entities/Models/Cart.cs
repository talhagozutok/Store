namespace Entities.Models;

public class Cart
{
    public List<CartItem> Items { get; set; }
    public Cart()
    {
        Items = new List<CartItem>();
    }

    public virtual void AddItem(Product product, int quantity)
    {
        CartItem? item =
            Items.Where(i => i.Product.ProductId.Equals(product.ProductId))
                 .FirstOrDefault();

        if (item is null)
        {
            Items.Add(new CartItem()
            {
                Product = product,
                Quantity = quantity
            });
        }
        else
        {
            item.Quantity += quantity;
        }
    }

    public virtual void RemoveLine(Product product)
        => Items.RemoveAll(i => i.Product.ProductId.Equals(product.ProductId));

    public decimal ComputeTotalValue()
        => Items.Sum(e => e.Product.Price * e.Quantity);

    public virtual void Clear() => Items.Clear();
}
