using AutoMapper;
using FiorelloApi.DTOs.SliderDto;
using FiorelloApi.Models;
using FiorelloApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FiorelloApi.Controllers.Admin
{
    public class SliderController : BaseAdminController
    {
        private readonly ISliderService _sliderService;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public SliderController(ISliderService sliderService, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _sliderService = sliderService;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] SliderCreateDTO request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (!(request.ImageFile.Length / 1024 < 200))
            {
                return BadRequest(ModelState);
            }

            string fileName = Guid.NewGuid().ToString() + request.ImageFile.FileName;

            string path = Path.Combine(_webHostEnvironment.WebRootPath, "img", fileName);

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await request.ImageFile.CopyToAsync(stream);
            }

            request.Image = fileName;

            var slider = _mapper.Map<Slider>(request);

            await _sliderService.Create(slider);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int? id)
        {
            if (id is null) return BadRequest(ModelState);
            var slider = await _sliderService.GetById((int)id);
            if (slider is null) return NotFound(ModelState);

            string path = Path.Combine(_webHostEnvironment.WebRootPath, "img", slider.Image);

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

            await _sliderService.Delete(slider);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Edit([FromQuery] int? id, [FromForm] SliderEditDTO request)
        {
            if (id is null) return BadRequest(ModelState);

            var slider = await _sliderService.GetById((int)id);

            if (slider is null) return NotFound(ModelState);

            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (!(request.NewImage.Length / 1024 < 200))
            {
                return BadRequest(ModelState);
            }

            string fileName = Guid.NewGuid().ToString() + request.NewImage.FileName;

            string path = Path.Combine(_webHostEnvironment.WebRootPath, "img", fileName);

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await request.NewImage.CopyToAsync(stream);
            }

            request.Image = fileName;

            await _sliderService.Edit(slider, request);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Detail([FromQuery] int? id)
        {
            if (id is null) return BadRequest(ModelState);

            var slider = await _sliderService.GetById((int)id);

            if (slider is null) return NotFound(ModelState);

            return Ok(_mapper.Map<SliderDetailDTO>(slider));
        }
    }
}
