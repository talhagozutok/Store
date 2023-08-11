using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;

namespace StoreWeb.Infrastructure.Extensions;

public static class TagHelperExtensions
{
    public static string GetString(IHtmlContent content)
    {
        using (var writer = new System.IO.StringWriter())
        {
            content.WriteTo(writer, HtmlEncoder.Default);
            return writer.ToString();
        }
    }
}
