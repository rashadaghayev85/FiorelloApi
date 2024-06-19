using AutoMapper;
using FiorelloApi.DTOs.ProductDto;
using FiorelloApi.Models;
using FiorelloApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using static FiorelloApi.Data.AppDBContext;

namespace FiorelloApi.Services
{
    public class ProductService : IProductService
    {
        private readonly Data.AppDBContext.AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductService(AppDbContext context, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _context = context;
        }

        public async Task Create(Product product)
        {
           // await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Product product)
        {
          //  _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task Edit(Product product, ProductEditDTO request)
        {
            _mapper.Map(request, product);
           // _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistProduct(string name)
        {
            return await _context.Products.AnyAsync(m => m.Name == name);
        }

        public async Task<List<Product>> GetAll()
        {
            return await _context.Products.Include(m => m.ProductImages).Include(m => m.Category).ToListAsync();
        }

        public async Task<Product> GetById(int id)
        {
            return await _context.Products.Include(m => m.ProductImages).Include(m => m.Category).FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}
