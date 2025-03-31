using Scenario.Application.Dtos.ContactUsDtos;

namespace Scenario.Application.Service.Interfaces
{
    public interface IContactUsService
    {
        Task<bool> SubmitRequest(ContactUsCreateDto contactUsCreateDto);
        Task<bool> Update(int id, ContactUsUpdateDto contactUsUpdateDto);
        Task<bool> Delete(int id);
        Task<List<ContactUsDto>> GetAll();
        Task<ContactUsDto> GetById(int id);
    }
}
