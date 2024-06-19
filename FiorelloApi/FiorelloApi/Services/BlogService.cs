using AutoMapper;
using FiorelloApi.DTOs.BlogDto;
using FiorelloApi.Models;
using FiorelloApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using static FiorelloApi.Data.AppDBContext;

namespace FiorelloApi.Services
{
    public class BlogService : IBlogService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public BlogService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task Create(Blog blog)
        {
          //  await _context.Blogs.AddAsync(blog);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Blog blog)
        {
           // _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();
        }

        public async Task Edit(Blog blog, BlogEditDTO request)
        {
            _mapper.Map(request, blog);
           // _context.Blogs.Update(blog);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistBlog(string title)
        {
            return await _context.Blogs.AnyAsync(blog => blog.Title == title);
        }

        public async Task<List<Blog>> GetAll()
        {
            return await _context.Blogs.ToListAsync();
        }

        public async Task<Blog> GetById(int id)
        {
            return await _context.Blogs.FirstOrDefaultAsync(blog => blog.Id == id);
        }
    }
}
