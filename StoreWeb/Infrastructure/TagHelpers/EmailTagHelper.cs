using Microsoft.AspNetCore.Razor.TagHelpers;

namespace StoreWeb.Infrastructure.TagHelpers;

// Match mail or email tag.
[HtmlTargetElement("mail")]
[HtmlTargetElement("email")]
public class EmailTagHelper : TagHelper
{
    private const string EmailDomain = "cikcikakademi.gov.tr";
    public string? MailTo { get; set; }

    // <email>help</email>
    // <a href="mailto:help@cikcikakademi.gov.tr">help@cikcikakademi.gov.tr</a>
    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "a";
        var content = await output.GetChildContentAsync();
        var target = content.GetContent() + "@" + EmailDomain;
        output.Attributes.SetAttribute("href", "mailto:" + target);
        output.Content.SetContent(target);
    }
}
