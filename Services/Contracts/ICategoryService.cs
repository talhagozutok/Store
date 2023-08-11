using Entities.Dtos;
using Entities.Models;

namespace Services.Contracts;

public interface ICategoryService
{
    IEnumerable<Category> GetAllCategories(bool trackChanges);
    CategoryDtoForUpdate? GetOneCategoryForUpdate(int id, bool trackChanges);
    void CreateCategory(CategoryDtoForInsertion categoryDto);
    void UpdateOneCategory(CategoryDtoForUpdate categoryDto);
    void DeleteOneCategory(int id);
}
