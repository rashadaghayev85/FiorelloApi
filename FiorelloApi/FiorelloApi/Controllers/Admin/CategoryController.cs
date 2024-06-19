using AutoMapper;
using FiorelloApi.DTOs.CategoryDto;
using FiorelloApi.Models;
using FiorelloApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FiorelloApi.Controllers.Admin
{
    public class CategoryController : BaseAdminController
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

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryCreateDTO request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var category = _mapper.Map<Category>(request);

            await _categoryService.Create(category);
            return Ok(category);
        }

        [HttpPut]
        public async Task<IActionResult> Edit([FromQuery] int? id, [FromBody] CategoryEditDTO request)
        {
            if (id is null) return BadRequest(ModelState);

            var category = await _categoryService.GetById((int)id);

            if (category is null) return NotFound(ModelState);

            if (await _categoryService.ExistCategory(request.Name))
            {
                ModelState.AddModelError("Name", "Category with this name already exists");
                return BadRequest(ModelState);
            }

            await _categoryService.Edit(category, request);
            return Ok(category);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int? id)
        {
            if (id is null) return BadRequest(ModelState);

            var category = await _categoryService.GetById((int)id);

            if (category is null) return NotFound(ModelState);

            await _categoryService.Delete(category);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Detail([FromQuery] int? id)
        {
            if (id is null) return BadRequest(ModelState);

            var category = await _categoryService.GetById((int)id);

            if (category is null) return NotFound(ModelState);

            return Ok(_mapper.Map<CategoryDetailDTO>(category));
        }
    }
}
