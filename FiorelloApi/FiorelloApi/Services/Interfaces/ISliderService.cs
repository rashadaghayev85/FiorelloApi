using FiorelloApi.DTOs.SliderDto;
using FiorelloApi.Models;

namespace FiorelloApi.Services.Interfaces
{
    public interface ISliderService
    {
        Task<List<SliderHomeDTO>> GetAllForHome();
        Task Create(Slider slider);
        Task<Slider> GetById(int id);
        Task Delete(Slider slider);
        Task Edit(Slider slider, SliderEditDTO request);
    }
}
