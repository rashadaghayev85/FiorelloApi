using AutoMapper;
using FiorelloApi.DTOs.ProductDto;
using FiorelloApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FiorelloApi.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        public ProductController(IProductService productService, IMapper mapper)
        {
            _mapper = mapper;
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllForHome()
        {
            var data = await _productService.GetAll();
            if (data is null) return NotFound(ModelState);
            var mappedData = _mapper.Map<List<ProductHomeDTO>>(data);
            return Ok(mappedData);
        }
    }
}
