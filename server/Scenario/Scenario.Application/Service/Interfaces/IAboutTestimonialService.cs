using Scenario.Application.Dtos.AboutTestimonialDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scenario.Application.Service.Interfaces
{
    public interface IAboutTestimonialService
    {
        Task<int> Create(AboutTestimonialCreateDto aboutTestimonialCreateDto);
        Task<int> Update(AboutTestimonialUpdateDto aboutTestimonialUpdateDto, int id);        
        Task<AboutTestimonialDto> GetById(int id);
    }   
}
