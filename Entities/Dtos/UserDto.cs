using System.ComponentModel.DataAnnotations;

namespace Entities.Dtos;

public record UserDto
{
    [DataType(DataType.Text)]
    [Display(Name = "Username")]
    [Required(ErrorMessage = "Username is required.")]
    public string? UserName { get; init; }

    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "Email is required.")]
    public string? Email { get; init; }

    [DataType(DataType.PhoneNumber)]
    [Display(Name = "Phone number")]
    public string? PhoneNumber { get; init; }

    // Roles can be settable.
    public HashSet<string> Roles { get; set; } = new HashSet<string>();
}
