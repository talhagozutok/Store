using Entities.Models;

namespace Repositories.Contracts;

public interface ICategoryRepository : IRepositoryBase<Category>
{
    void DeleteOneCategory(Category category);
    Category? GetOneCategory(int id, bool trackChanges);
    void UpdateOneCategory(Category entity);
}
