using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace StoreWeb.Infrastructure.TagHelpers;

[HtmlTargetElement("td", Attributes = "user-role")]
public class UserRoleTagHelper : TagHelper
{
    private readonly UserManager<IdentityUser> _userManager;

    private readonly RoleManager<IdentityRole> _roleManager;

    public string? UserId { get; set; }

    public UserRoleTagHelper(
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public override async Task ProcessAsync(
        TagHelperContext context,
        TagHelperOutput output)
    {
        var user = await _userManager.FindByIdAsync(UserId!);

        if (user!.UserName != "admin")
        {
            TagBuilder ul = new TagBuilder("ul");
            var roles = _roleManager.Roles.ToList().Select(r => r.Name);

            foreach (var role in roles)
            {
                TagBuilder div = new TagBuilder("div");
                TagBuilder input = new TagBuilder("input");
                TagBuilder li = new TagBuilder("li");
                string content = $"{role} : {await _userManager.IsInRoleAsync(user!, role!),5}";
                li.InnerHtml.Append(content);
                ul.InnerHtml.AppendHtml(li);
            }
            output.Content.AppendHtml(ul);
        }
    }
}
