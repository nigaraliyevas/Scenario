using Scenario.Application.Dtos.AboutTestimonialDtos;

namespace Scenario.Application.Service.Interfaces
{
    public interface IAboutTestimonialService
    {
        Task<int> Create(AboutTestimonialCreateDto aboutTestimonialCreateDto);
        Task<int> Update(AboutTestimonialUpdateDto aboutTestimonialUpdateDto, int id);
        Task<AboutTestimonialDto> GetById(int id);
    }
}
