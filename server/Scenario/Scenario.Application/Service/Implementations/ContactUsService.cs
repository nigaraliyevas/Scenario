using AutoMapper;
using Scenario.Application.Dtos.ContactUsDtos;
using Scenario.Application.Exceptions;
using Scenario.Application.Service.Interfaces;
using Scenario.Core.Entities;
using Scenario.DataAccess.Implementations.UnitOfWork;

namespace Scenario.Application.Service.Implementations
{
    public class ContactUsService : IContactUsService
    {
        private readonly IEmailSenderService _emailService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ContactUsService(IMapper mapper, IUnitOfWork unitOfWork, IEmailSenderService emailService)
        {
            _emailService = emailService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Delete(int id)
        {
            if (id <= 0) throw new CustomException(400, "Id is not right");
            var contact = await _unitOfWork.ContactUsRepository.GetEntity(x => x.Id == id);
            if (contact == null) throw new CustomException(404, "Couldn't find");

            await _unitOfWork.ContactUsRepository.Delete(contact);
            _unitOfWork.Commit();
            return true;
        }

        public async Task<List<ContactUsDto>> GetAll()
        {
            var contacts = await _unitOfWork.ContactUsRepository.GetAll();
            return _mapper.Map<List<ContactUsDto>>(contacts);
        }

        public async Task<ContactUsDto> GetById(int id)
        {
            if (id <= 0) throw new CustomException(400, "Id is not right");
            var contact = await _unitOfWork.ContactUsRepository.GetEntity(x => x.Id == id);
            return _mapper.Map<ContactUsDto>(contact);
        }

        public async Task<bool> SubmitRequest(ContactUsCreateDto contactUsCreateDto)
        {
            if (contactUsCreateDto == null) throw new CustomException(400, "Can't be null");
            var contactRequest = _mapper.Map<ContactUs>(contactUsCreateDto);
            await _unitOfWork.ContactUsRepository.Create(contactRequest);
            _unitOfWork.Commit();
            string emailBody =
                   $"<h3>New Contact Request</h3><p><b>Name:</b> {contactUsCreateDto.FullName}</p>" +
                   $"<p><b>Subject:</b> {contactUsCreateDto.Subject}</p>" +
                   $"<p><b>Phone:</b> {contactUsCreateDto.PhoneNumber}</p>" +
                   $"<p><b>Message:</b> {contactUsCreateDto.Message}</p>";

            try
            {
                await _emailService.SendEmailAsync("nvailesi@gmail.com", "Yeni Əlaqə", emailBody);
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Update(int id, ContactUsUpdateDto contactUsUpdateDto)
        {
            if (contactUsUpdateDto == null || id <= 0) throw new CustomException(400, "Can't be null");
            var contact = await _unitOfWork.ContactUsRepository.GetEntity(x => x.Id == id);
            if (contact == null) throw new CustomException(404, "Couldn't find");

            var updated = _mapper.Map(contactUsUpdateDto, contact);
            await _unitOfWork.ContactUsRepository.Update(updated);
            _unitOfWork.Commit();
            return true;
        }
    }
}
