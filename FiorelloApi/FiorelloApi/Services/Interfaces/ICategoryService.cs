using FiorelloApi.DTOs.CategoryDto;
using FiorelloApi.Models;

namespace FiorelloApi.Services.Interfaces
{
    public interface ICategoryService
    {
        Task Create(Category category);
        Task Delete(Category category);
        Task Edit(Category category, CategoryEditDTO request);
        Task<Category> GetById(int id);
        Task<List<Category>> GetAll();
        Task<bool> ExistCategory(string name);
        Task<Category> GetCategoryByName(string name);
    }
}
