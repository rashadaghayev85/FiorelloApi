using AutoMapper;
using FiorelloApi.DTOs.SliderDto;
using FiorelloApi.Models;
using FiorelloApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using static FiorelloApi.Data.AppDBContext;

namespace FiorelloApi.Services
{
    public class SliderService : ISliderService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public SliderService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task Create(Slider slider)
        {
            //await _context.Sliders.AllAsync(slider);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Slider slider)
        {
           // _context.Sliders.Remove(slider);
            await _context.SaveChangesAsync();
        }

        public async Task Edit(Slider slider, SliderEditDTO request)
        {
            _mapper.Map(request, slider);

           // _context.Sliders.Update(slider);
            await _context.SaveChangesAsync();
        }

        public async Task<List<SliderHomeDTO>> GetAllForHome()
        {
            var data = await _context.Sliders.ToListAsync();

            if (data == null) return null;
            return _mapper.Map<List<SliderHomeDTO>>(data);
        }

        public async Task<Slider> GetById(int id)
        {
            return await _context.Sliders.FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}
