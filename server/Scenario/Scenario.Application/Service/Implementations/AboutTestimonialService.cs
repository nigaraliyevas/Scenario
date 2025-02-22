using AutoMapper;
using Scenario.Application.Dtos.AboutTestimonialDtos;
using Scenario.Application.Exceptions;
using Scenario.Application.Service.Interfaces;
using Scenario.Core.Entities;
using Scenario.DataAccess.Implementations.UnitOfWork;

namespace Scenario.Application.Service.Implementations
{
    public class AboutTestimonialService : IAboutTestimonialService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public AboutTestimonialService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<int> Create(AboutTestimonialCreateDto aboutTestimonialCreateDto)
        {
            if (aboutTestimonialCreateDto == null) throw new CustomException(404, "Null Exception");
            var newTestimonial = new AboutTestimonial
            {
                Header = aboutTestimonialCreateDto.Header,
                Title = aboutTestimonialCreateDto.Title,
            };
            if (aboutTestimonialCreateDto.Image != null){newTestimonial.Image = aboutTestimonialCreateDto.Image;}
            await _unitOfWork.AboutTestimonialRepository.Create(newTestimonial);
            _unitOfWork.Commit();
            return newTestimonial.Id;
        }


        public async Task<AboutTestimonialDto> GetById(int id)
        {
            var testimonial = await _unitOfWork.AboutTestimonialRepository.GetEntity(x => x.Id == id);
            if (testimonial == null) throw new CustomException(404, "Not Found");      
            return new AboutTestimonialDto
            {
                Title = testimonial.Title,
                Header = testimonial.Header,
                Image = testimonial.Image,
            };
        }

        public async Task<int> Update(AboutTestimonialUpdateDto aboutTestimonialUpdateDto, int id)
        {
            if (aboutTestimonialUpdateDto == null) throw new CustomException(404, "Null Exception");
            var existTestimonial = await _unitOfWork.AboutTestimonialRepository.GetEntity(x => x.Id == id);
            if (existTestimonial == null) throw new CustomException(404, "Not Found");
            existTestimonial.UpdatedDate = DateTime.Now;
            existTestimonial.Header = aboutTestimonialUpdateDto.Header;
            existTestimonial.Title = aboutTestimonialUpdateDto.Title;
            if (aboutTestimonialUpdateDto.Image != null)
            {
               existTestimonial.Image = aboutTestimonialUpdateDto.Image;
            }
            await _unitOfWork.AboutTestimonialRepository.Update(existTestimonial);
            _unitOfWork.Commit();
            return id;
        }
    }
}
