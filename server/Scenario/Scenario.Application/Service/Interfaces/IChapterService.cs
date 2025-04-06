using Scenario.Application.Dtos.ChapterDtos;

namespace Scenario.Application.Service.Interfaces
{
    public interface IChapterService
    {
        Task<ChapterDto> Create(ChapterCreateDto categoryCreateDto);
        Task<ChapterDto> Update(ChapterUpdateDto categoryUpdateDto);
        Task<int> Delete(int id);
        Task<List<ChapterDto>> GetAll();
        Task<ChapterDto> GetById(int id);
    }
}
