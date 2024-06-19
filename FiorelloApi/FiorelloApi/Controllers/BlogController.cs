using AutoMapper;
using FiorelloApi.DTOs.BlogDto;
using FiorelloApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FiorelloApi.Controllers
{
    public class BlogController : BaseController
    {
        private readonly IBlogService _blogService;
        private readonly IMapper _mapper;
        public BlogController(IBlogService blogService, IMapper mapper)
        {
            _blogService = blogService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllForHome()
        {
            var data = await _blogService.GetAll();

            if (data == null) return null;

            var mappedData = _mapper.Map<List<BlogHomeDTO>>(data);

            return Ok(mappedData);
        }
    }
}
