using AutoMapper;
using FiorelloApi.DTOs.ProductDto;
using FiorelloApi.Models;
using FiorelloApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FiorelloApi.Controllers.Admin
{
    public class ProductController : BaseAdminController
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ICategoryService _categoryService;
        public ProductController(IProductService productService, IMapper mapper, IWebHostEnvironment webHostEnvironment, ICategoryService categoryService)
        {
            _productService = productService;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _categoryService = categoryService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ProductCreateDTO request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (!await _categoryService.ExistCategory(request.Category))
            {
                ModelState.AddModelError("Category", "Category does not exists");
                return BadRequest(ModelState);
            }

            foreach (var item in request.ImageFiles)
            {
                if (!(item.Length / 1024 < 500))
                {
                    ModelState.AddModelError("Image", "Image max size is 500KB");
                    return BadRequest(ModelState);
                }
            }

            bool existingProduct = await _productService.ExistProduct(request.Name);
            if (existingProduct)
            {
                ModelState.AddModelError("Name", "Product with this title already exists");
                return BadRequest(ModelState);
            }

            var category = await _categoryService.GetCategoryByName(request.Category);
            if (category == null)
            {
                ModelState.AddModelError("Category", "Category does not exist");
                return BadRequest(ModelState);
            }

            List<string> images = new();

            foreach (var item in request.ImageFiles)
            {
                string fileName = Guid.NewGuid().ToString() + item.FileName;

                string path = Path.Combine(_webHostEnvironment.WebRootPath, "img", fileName);

                using (FileStream stream = new(path, FileMode.Create))
                {
                    await item.CopyToAsync(stream);
                }

                images.Add(fileName);

            }
            request.Images = images;

            Product product = _mapper.Map<Product>(request);

            product.Category = category;

            product.ProductImages.FirstOrDefault().isMain = true;

            await _productService.Create(product);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int? id)
        {
            if (id is null) return BadRequest(ModelState);

            var product = await _productService.GetById((int)id);

            if (product is null) return NotFound(ModelState);

            foreach (var item in product.ProductImages)
            {
                string path = Path.Combine(_webHostEnvironment.WebRootPath, "img", item.Name);

                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }

            await _productService.Delete(product);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Edit([FromQuery] int? id, [FromForm] ProductEditDTO request)
        {
            if (id is null) return BadRequest(ModelState);

            var product = await _productService.GetById((int)id);

            if (product is null) return NotFound(ModelState);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var category = await _categoryService.GetCategoryByName(request.Category);
            if (category == null)
            {
                ModelState.AddModelError("Category", "Category does not exist");
                return BadRequest(ModelState);
            }

            if (await _productService.ExistProduct(request.Name))
            {
                ModelState.AddModelError("Name", "Product with this title already exists");
                return BadRequest(ModelState);
            }

            if (request.ImageFiles is not null)
            {
                List<string> images = new();

                foreach (var item in request.ImageFiles)
                {
                    string fileName = Guid.NewGuid().ToString() + item.FileName;

                    string path = Path.Combine(_webHostEnvironment.WebRootPath, "img", fileName);

                    using (FileStream stream = new(path, FileMode.Create))
                    {
                        await item.CopyToAsync(stream);
                    }

                    images.Add(fileName);

                }
                request.Images = images;
            }
            else
            {
                request.Images = product.ProductImages.Select(m => m.Name).ToList();
            }

            product.Category = category;

            await _productService.Edit(product, request);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Detail([FromQuery] int? id)
        {
            if (id is null) return BadRequest(ModelState);

            var product = await _productService.GetById((int)id);

            if (product is null) return NotFound(ModelState);

            return Ok(_mapper.Map<ProductDetailDTO>(product));
        }
    }
}
