using System.ComponentModel.DataAnnotations;

namespace Entities.Dtos;

public record RegisterDto
{
    [DataType(DataType.Text)]
    [Display(Name = "User name")]
    [Required(ErrorMessage = "Username is required.")]
    public string? UserName { get; init; }

    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Password is required.")]
    public string? Password { get; init; }

    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "Email is required.")]
    public string? Email { get; init; }
}
