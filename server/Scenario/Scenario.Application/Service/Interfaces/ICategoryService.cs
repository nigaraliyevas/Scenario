using Scenario.Application.Dtos.CategoryDtos;

namespace Scenario.Application.Service.Interfaces
{
    public interface ICategoryService
    {
        Task<int> Create(CategoryCreateDto categoryCreateDto);
        Task<int> Update(CategoryUpdateDto categoryUpdateDto);
        Task<int> Delete(int id);
        Task<List<CategoryDto>> GetAll();
        Task<CategoryDto> GetById(int id);
    }
}
