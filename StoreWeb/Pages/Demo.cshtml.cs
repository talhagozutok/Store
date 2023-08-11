using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace StoreWeb.Pages;

public class DemoModel : PageModel
{
    public string? FullName => HttpContext?.Session?.GetString("name");

    public DemoModel()
    {

    }

    public void OnGet()
    {

    }

    public void OnPost([FromForm] string name)
    {
        // FullName = name;
        HttpContext.Session.SetString("name", name);
    }
}
