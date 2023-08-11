using Entities.Models;

namespace Repositories.Extensions;

public static class ProductRepositoryExtensions
{
    public static IQueryable<Product> FilteredByCategoryId(
        this IQueryable<Product> products,
        int? categoryId)
    {
        if (categoryId is null)
        {
            return products;
        }

        return products.Where(prd => prd.CategoryId.Equals(categoryId));
    }

    public static IQueryable<Product> FilteredBySearchTerm(
        this IQueryable<Product> products,
        string? searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            return products;
        }

        return products.Where(prd => prd.ProductName!.ToLower().Contains(searchTerm.ToLower()));
    }

    public static IQueryable<Product> FilteredByPrice(
        this IQueryable<Product> products,
        int minPrice,
        int maxPrice,
        bool isValidPrice)
    {
        if (!isValidPrice)
        {
            return products;
        }

        return products.Where(prd => minPrice <= prd.Price && prd.Price <= maxPrice);
    }

    public static IQueryable<Product> ToPaginate(
        this IQueryable<Product> products,
        int pageNumber,
        int pageSize)
    {
        return products
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);
    }
}
