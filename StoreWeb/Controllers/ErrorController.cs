using Microsoft.AspNetCore.Mvc;

namespace StoreWeb.Controllers;

[Route("error")]
public class ErrorController : Controller
{
    [Route("404")]
    public IActionResult PageNotFound()
    {
        string originalPath = "unknown";

        if (HttpContext.Items.ContainsKey("originalPath"))
        {
            originalPath = (string)HttpContext.Items["originalPath"]!;
        }

        return View();
    }
}
