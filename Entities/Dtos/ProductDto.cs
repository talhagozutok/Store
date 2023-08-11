using System.ComponentModel.DataAnnotations;

namespace Entities.Dtos;

public record ProductDto
{
    public int ProductId { get; init; }

    [Display(Name = "Product name")]
    [Required(ErrorMessage = "ProductName is required.")]
    public string? ProductName { get; init; } = string.Empty;

    [Required(ErrorMessage = "Price is required.")]
    public decimal Price { get; init; }
    public string? Summary { get; init; } = string.Empty;
    public string? ImageUrl { get; set; }
    public int? CategoryId { get; init; }
}
