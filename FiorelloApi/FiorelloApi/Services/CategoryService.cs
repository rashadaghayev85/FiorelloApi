using AutoMapper;
using FiorelloApi.DTOs.CategoryDto;
using FiorelloApi.Models;
using FiorelloApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using static FiorelloApi.Data.AppDBContext;

namespace FiorelloApi.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        public CategoryService(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }
        public async Task Create(Category category)
        {
           // await _appDbContext.Categories.AddAsync(category);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task Delete(Category category)
        {
          //  _appDbContext.Categories.Remove(category);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task Edit(Category category, CategoryEditDTO request)
        {
            _mapper.Map(request, category);
            //_appDbContext.Categories.Update(category);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task<Category> GetById(int id)
        {
            return await _appDbContext.Categories.FirstOrDefaultAsync(m => m.Id == id);
        }
        public async Task<List<Category>> GetAll()
        {
            return await _appDbContext.Categories.ToListAsync();
        }
        public async Task<bool> ExistCategory(string name)
        {
            return await _appDbContext.Categories.AnyAsync(m => m.Name == name);
        }

        public async Task<Category> GetCategoryByName(string name)
        {
            return await _appDbContext.Categories.FirstOrDefaultAsync(m => m.Name == name);
        }
    }
}
