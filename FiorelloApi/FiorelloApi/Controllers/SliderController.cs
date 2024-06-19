using AutoMapper;
using FiorelloApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FiorelloApi.Controllers
{
    public class SliderController : BaseController
    {
        private readonly ISliderService _sliderService;
        public SliderController(ISliderService sliderService, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _sliderService = sliderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllForHome()
        {
            return Ok(await _sliderService.GetAllForHome());
        }


    }
}
