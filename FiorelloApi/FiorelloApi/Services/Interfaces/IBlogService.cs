using FiorelloApi.DTOs.BlogDto;
using FiorelloApi.Models;

namespace FiorelloApi.Services.Interfaces
{
    public interface IBlogService
    {
        Task Create(Blog blog);
        Task Delete(Blog blog);
        Task Edit(Blog blog, BlogEditDTO request);
        Task<Blog> GetById(int id);
        Task<List<Blog>> GetAll();
        Task<bool> ExistBlog(string title);
    }
}
