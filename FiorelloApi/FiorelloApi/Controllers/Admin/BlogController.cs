using AutoMapper;
using FiorelloApi.DTOs.BlogDto;
using FiorelloApi.Models;
using FiorelloApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;

namespace FiorelloApi.Controllers.Admin
{
    public class BlogController : BaseAdminController
    {
        private readonly IBlogService _blogService;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public BlogController(IBlogService blogService, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _blogService = blogService;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] BlogCreateDTO request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (!(request.ImageFile.Length / 1024 < 500))
            {
                ModelState.AddModelError("Image", "Image max size is 500KB");
                return BadRequest(ModelState);
            }

            string fileName = Guid.NewGuid().ToString() + request.ImageFile.FileName;

            string path = Path.Combine(_webHostEnvironment.WebRootPath, "img", fileName);

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await request.ImageFile.CopyToAsync(stream);
            }
            request.Image = fileName;

            var blog = _mapper.Map<Blog>(request);

            await _blogService.Create(blog);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int? id)
        {
            if (id is null) return BadRequest(ModelState);

            var blog = await _blogService.GetById((int)id);

            if (blog is null) return NotFound(ModelState);

            string path = Path.Combine(_webHostEnvironment.WebRootPath, "img", blog.Image);

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

            await _blogService.Delete(blog);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Edit([FromQuery] int? id, [FromForm] BlogEditDTO request)
        {
            if (id is null) return BadRequest(ModelState);

            var blog = await _blogService.GetById((int)id);

            if (blog is null) return NotFound(ModelState);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (await _blogService.ExistBlog(request.Title))
            {
                ModelState.AddModelError("Title", "Blog with this title already exists");
                return BadRequest(ModelState);
            }

            if (request.ImageFile is not null)
            {
                if (!(request.ImageFile.Length / 1024 < 500))
                {
                    ModelState.AddModelError("Image", "Image max size is 500KB");
                    return BadRequest(ModelState);
                }
                string fileName = Guid.NewGuid().ToString() + request.ImageFile.FileName;

                string path = Path.Combine(_webHostEnvironment.WebRootPath, "img", fileName);

                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    await request.ImageFile.CopyToAsync(stream);
                }
                request.Image = fileName;
            }
            else
            {
                request.Image = blog.Image;
            }


            await _blogService.Edit(blog, request);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Detail([FromQuery] int? id)
        {
            if (id is null) return BadRequest(ModelState);

            var blog = await _blogService.GetById((int)id);

            if (blog is null) return NotFound(ModelState);

            return Ok(_mapper.Map<BlogDetailDTO>(blog));
        }
    }
}
