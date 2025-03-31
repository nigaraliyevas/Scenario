using Scenario.Application.Dtos.ChapterDtos;

namespace Scenario.Application.Service.Interfaces
{
    public interface IChapterService
    {
        Task<int> Create(ChapterCreateDto categoryCreateDto);
        Task<int> Update(ChapterUpdateDto categoryUpdateDto);
        Task<int> Delete(int id);
        Task<List<ChapterDto>> GetAll();
        Task<ChapterDto> GetById(int id);
    }
}
