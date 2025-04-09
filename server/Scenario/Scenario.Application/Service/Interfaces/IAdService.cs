using Scenario.Application.Dtos.AdDtos;

namespace Scenario.Application.Service.Interfaces
{
    public interface IAdService
    {
        Task<int> Create(AdCreateDto adCreateDto);
        Task<int> Update(AdUpdateDto adUpdateDto);
        Task<int> Delete(int id);
        Task<List<AdDto>> GetAll();
        Task<List<AdDto>> GetAll(int takeCount);
        Task<AdDto> GetById(int id);
    }
}
