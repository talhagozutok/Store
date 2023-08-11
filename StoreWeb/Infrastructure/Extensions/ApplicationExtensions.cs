using System.Globalization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace StoreWeb.Infrastructure.Extensions;

public static class ApplicationExtensions
{
    public static void ConfigureAndCheckMigration(this IApplicationBuilder app)
    {
        RepositoryContext context = app
            .ApplicationServices
            .CreateScope()
            .ServiceProvider
            .GetRequiredService<RepositoryContext>();

        if (context.Database.GetPendingMigrations().Any())
        {
            context.Database.Migrate();
        }
    }

    public static void ConfigureLocalization(this WebApplication app)
    {
        /*
        var supportedCultures = new[] { "en-US", "tr-TR" };
        app.UseRequestLocalization(options =>
        {
            options.AddSupportedCultures(supportedCultures)
                .AddSupportedUICultures(supportedCultures)
                .SetDefaultCulture(supportedCultures[0]);
        });
        */

        var supportedCultures = new CultureInfo[] {
            CultureExtensions.GetTurkishCultureInfo(),
            CultureExtensions.GetUSCultureInfo()
        };

        CultureInfo.DefaultThreadCurrentCulture = supportedCultures[0];
        CultureInfo.DefaultThreadCurrentUICulture = supportedCultures[0];
    }

    public static async void ConfigureDefaultAdminUser(this IApplicationBuilder app)
    {
        const string adminUser = "admin";
        const string adminPass = "1";

        // UserManager
        UserManager<IdentityUser> userManager = app
            .ApplicationServices
            .CreateScope()
            .ServiceProvider
            .GetRequiredService<UserManager<IdentityUser>>();

        // RoleManager
        RoleManager<IdentityRole> roleManager = app
            .ApplicationServices
            .CreateAsyncScope()
            .ServiceProvider
            .GetRequiredService<RoleManager<IdentityRole>>();

        IdentityUser? user = await userManager.FindByNameAsync(adminUser);

        if (user is null)
        {
            user = new IdentityUser()
            {
                Email = "limon@cikcikakademi.gov.tr",
                PhoneNumber = "5061234567",
                UserName = adminUser,
            };

            var result = await userManager.CreateAsync(user, adminPass);
            
            if (!result.Succeeded)
            {
                throw new Exception("Admin user could not created.");
            }

            var roleResult = await userManager.AddToRolesAsync(user,
                roleManager
                    .Roles
                    .Select(r => r.Name)
                    .ToList()!
            );
            if (!roleResult.Succeeded)
            {
                throw new Exception("System have problems with role definition for admin.");
            }
        }
    }

    public static void ConfigureErrorViewPages(this IApplicationBuilder app)
    {
        app.Use(async (ctx, next) =>
        {
            await next();
            if (ctx.Response.StatusCode == StatusCodes.Status404NotFound && !ctx.Response.HasStarted)
            {
                // Re-execute the request so the user gets the error page.
                string originalPath = ctx.Request.Path.Value!;
                ctx.Items["originalPath"] = originalPath;
                ctx.Request.Path = "/error/404";
                await next();
            }
        });
    }
}
