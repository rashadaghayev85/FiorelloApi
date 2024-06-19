using AutoMapper;
using FiorelloApi.DTOs.CategoryDto;
using FiorelloApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FiorelloApi.Controllers
{
    public class CategoryController : BaseController
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CategoryController(ICategoryService categoryService, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _categoryService = categoryService;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllForHome()
        {
            var data = await _categoryService.GetAll();

            if (data is null) return null;

            var mappedData = _mapper.Map<List<CategoryHomeDTO>>(data);

            return Ok(mappedData);
        }
    }
}
