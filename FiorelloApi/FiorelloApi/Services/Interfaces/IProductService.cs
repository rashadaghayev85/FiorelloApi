using FiorelloApi.DTOs.ProductDto;
using FiorelloApi.Models;

namespace FiorelloApi.Services.Interfaces
{
    public interface IProductService
    {
        Task Create(Product product);
        Task Delete(Product product);
        Task Edit(Product product, ProductEditDTO request);
        Task<Product> GetById(int id);
        Task<List<Product>> GetAll();
        Task<bool> ExistProduct(string name);
    }
}
