using Entities.Models;
using Repositories.Contracts;

namespace Repositories;

public class CategoryRepository :
    RepositoryBase<Category>,
    ICategoryRepository
{
    public CategoryRepository(RepositoryContext context) : base(context)
    {

    }

    public void DeleteOneCategory(Category category)
        => Remove(category);

    public IQueryable<Category> GetAllProducts(bool trackChanges)
        => FindAll(trackChanges);

    public Category? GetOneCategory(int id, bool trackChanges)
        => FindByCondition(c => c.CategoryId.Equals(id), trackChanges);

    public Category? GetOneProduct(int id, bool trackChanges)
        => FindByCondition(p => p.CategoryId.Equals(id), trackChanges);

    public void UpdateOneCategory(Category category)
        => Update(category);
}

