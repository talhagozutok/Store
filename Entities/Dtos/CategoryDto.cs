using System.ComponentModel.DataAnnotations;

namespace Entities.Dtos;

public record CategoryDto
{
    public int CategoryId { get; init; }

    [Display(Name = "Category name")]
    [Required(ErrorMessage = "CategoryName is required.")]
    public string CategoryName { get; init; } = default!;
}
