namespace Entities.Models;

public class Category
{
    public int CategoryId { get; set; }
    public string? CategoryName { get; set; } = string.Empty;
    public ICollection<Product>? Products { get; } // Collection navigation property
}
