using Entities.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.Contracts;
using Services;
using Services.Contracts;
using StoreWeb.Models;

namespace StoreWeb.Infrastructure.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureDbContext(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<RepositoryContext>(options =>
        {
            options.UseSqlServer(
                configuration.GetConnectionString("MssqlConnection")!,
                b => b.MigrationsAssembly("StoreWeb"));

            // options.EnableSensitiveDataLogging(true);
        });
    }

    public static void ConfigureIdentity(this IServiceCollection services)
    {
        services.AddIdentity<IdentityUser, IdentityRole>(
            options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.User.RequireUniqueEmail = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 1;
            }
        )
        .AddEntityFrameworkStores<RepositoryContext>()
        .AddDefaultTokenProviders();
    }

    public static void ConfigureSession(this IServiceCollection services)
    {
        services.AddDistributedMemoryCache();
        services.AddSession(options =>
        {
            options.Cookie.Name = "StoreWeb.Session";
            options.IdleTimeout = TimeSpan.FromMinutes(10);
        });

        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        services.AddScoped<Cart>(cart => SessionCart.GetCart(cart));
    }

    public static void ConfigureRepositoryRegistration(this IServiceCollection services)
    {
        services.AddScoped<IRepositoryManager, RepositoryManager>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
    }

    public static void ConfigureServiceRegistration(this IServiceCollection services)
    {
        services.AddScoped<IServiceManager, ServiceManager>();
        services.AddScoped<IProductService, ProductManager>();
        services.AddScoped<ICategoryService, CategoryManager>();
        services.AddScoped<IOrderService, OrderManager>();
        services.AddScoped<IAuthService, AuthManager>();
    }

    public static void ConfigureApplicationCookie(this IServiceCollection services)
    {
        services.ConfigureApplicationCookie(options =>
        {
            options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
            options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
            options.LoginPath = new PathString("/Account/Login");
            options.AccessDeniedPath = new PathString("/Account/AccessDenied");
        });
    }

    public static void ConfigureRouting(this IServiceCollection services)
    {
        services.AddRouting(options =>
        {
            options.LowercaseUrls = true;
            options.LowercaseQueryStrings = true;
            options.AppendTrailingSlash = false;
        });
    }
}
