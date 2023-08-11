using Entities.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Services.Contracts;

namespace StoreWeb.Infrastructure.TagHelpers;

[HtmlTargetElement("div", Attributes = "products")]
public class NewProductTagHelper : TagHelper
{
    private readonly IServiceManager _manager;

    public int Number { get; set; } = 5;

    public NewProductTagHelper(IServiceManager manager)
    {
        _manager = manager;
    }

    public override void Process(
        TagHelperContext context,
        TagHelperOutput output)
    {
        TagBuilder div = new TagBuilder("div");
        div.Attributes.Add("class", "my-3");

        TagBuilder h6 = new TagBuilder("h6");
        h6.Attributes.Add("class", "lead");

        TagBuilder icon = new TagBuilder("i");
        icon.Attributes.Add("class", "fa fa-box text-secondary");

        h6.InnerHtml.AppendHtml(icon);
        h6.InnerHtml.Append(" New Products");

        TagBuilder ul = new TagBuilder("ul");
        ul.Attributes.Add("class", "text-start");
        var products = _manager.ProductService.GetNewProducts(Number, false);

        foreach (Product product in products)
        {
            TagBuilder li = new TagBuilder("li");
            TagBuilder a = new TagBuilder("a");
            a.Attributes.Add("href", $"/product/get/{product.ProductId}");
            a.InnerHtml.Append(product.ProductName!);
            li.InnerHtml.AppendHtml(a);
            ul.InnerHtml.AppendHtml(li);
        }

        div.InnerHtml.AppendHtml(h6);
        div.InnerHtml.AppendHtml(ul);
        output.Content.AppendHtml(div);
    }
}
