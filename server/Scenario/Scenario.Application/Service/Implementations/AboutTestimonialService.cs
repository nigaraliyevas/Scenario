using AutoMapper;
using Scenario.Application.Dtos.AboutTestimonialDtos;
using Scenario.Application.Dtos.ActorDtos;
using Scenario.Application.Exceptions;
using Scenario.Application.Service.Interfaces;
using Scenario.Core.Entities;
using Scenario.DataAccess.Implementations.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            if (aboutTestimonialCreateDto.Image.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await aboutTestimonialCreateDto.Image.CopyToAsync(memoryStream);
                    newTestimonial.Image = memoryStream.ToArray();
                }
            }
            await _unitOfWork.AboutTestimonialRepository.Create(newTestimonial);
            _unitOfWork.Commit();
            return newTestimonial.Id;
        }


        public async Task<AboutTestimonialDto> GetById(int id)
        {
            var testimonial = await _unitOfWork.AboutTestimonialRepository.GetEntity(x => x.Id == id);
            if (testimonial == null) throw new CustomException(404, "Not Found");
            // Convert byte[] to Base64 string (only if image exists)
            string imageDataUrl = testimonial.Image != null
                ? $"data:image/png;base64,{Convert.ToBase64String(testimonial.Image)}"
                : null;
            return new AboutTestimonialDto
            {
                Title = testimonial.Title,
                Header = testimonial.Header,
                ImageUrl = imageDataUrl,
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
                using (var memoryStream = new MemoryStream())
                {
                    await aboutTestimonialUpdateDto.Image.CopyToAsync(memoryStream);
                    existTestimonial.Image = memoryStream.ToArray();
                }
            }
            await _unitOfWork.AboutTestimonialRepository.Update(existTestimonial);
            _unitOfWork.Commit();
            return id;
        }
    }
}
