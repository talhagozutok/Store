using System.Text.Json.Serialization;
using Entities.Models;
using StoreWeb.Infrastructure.Extensions;

namespace StoreWeb.Models;

public class SessionCart : Cart
{
    [JsonIgnore]
    public ISession? Session { get; set; }

    public static Cart GetCart(IServiceProvider services)
    {
        ISession? session = services.GetRequiredService<IHttpContextAccessor>()?
            .HttpContext?.Session;

        SessionCart cart = session?.Get<SessionCart>("Cart") ?? new();
        cart.Session = session;

        return cart;
    }

    public override void AddItem(Product product, int quantity)
    {
        base.AddItem(product, quantity);
        Session?.Set<SessionCart>("Cart", this);
    }

    public override void RemoveLine(Product product)
    {
        base.RemoveLine(product);
        Session?.Set("Cart", this);
    }

    public override void Clear()
    {
        base.Clear();
        Session?.Remove("Cart");
    }
}
