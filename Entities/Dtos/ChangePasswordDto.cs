using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Dtos;

public record ChangePasswordDto
{
    public string? UserId { get; init; }

    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Password is required.")]
    public string? Password { get; init; }

    [NotMapped]
    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Required(ErrorMessage = "Confirm password is required.")]
    [Compare(nameof(Password), ErrorMessage = "'Password' and 'Confirm password' must match.")]
    public string? ConfirmPassword { get; init; }
}
