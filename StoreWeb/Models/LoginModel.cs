using System.ComponentModel.DataAnnotations;

namespace StoreWeb.Models;

public class LoginModel
{
    private string? _returnUrl;

    [DataType(DataType.Text)]
    [Display(Name = "Username")]
    [Required(ErrorMessage = "Username is required.")]
    public string? UserName { get; set; }

    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Password is required.")]
    public string? Password { get; set; }

    public string ReturnUrl
    {
        get
        {
            return _returnUrl is null ? "/" : _returnUrl;
        }
        set
        {
            _returnUrl = value;
        }
    }
}
