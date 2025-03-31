using AutoMapper;
using Scenario.Application.Dtos.ChapterDtos;
using Scenario.Application.Exceptions;
using Scenario.Application.Service.Interfaces;
using Scenario.Core.Entities;
using Scenario.DataAccess.Implementations.UnitOfWork;

namespace Scenario.Application.Service.Implementations
{
    public class ChapterService : IChapterService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ChapterService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<int> Create(ChapterCreateDto chapterCreateDto)
        {
            if (string.IsNullOrWhiteSpace(chapterCreateDto.Content)) throw new CustomException(400, "Chapter content cannot be empty");

            const int CharsPerPage = 4500;  // 4500 characters per page for A4-like formatting
            int totalChars = chapterCreateDto.Content.Length;  // Total number of characters
            int totalPages = (int)Math.Ceiling((double)totalChars / CharsPerPage);  // Calculate total pages based on characters

            var chapter = _mapper.Map<Chapter>(chapterCreateDto);
            chapter.Page = totalPages;

            await _unitOfWork.ChapterRepository.Create(chapter);
            _unitOfWork.Commit();

            return chapter.Id;
        }

        public async Task<int> Delete(int id)
        {
            if (id <= 0) throw new CustomException(404, "Null Exception");
            var chapter = await _unitOfWork.ChapterRepository.GetEntity(x => x.Id == id);
            if (chapter == null) throw new CustomException(404, "Not Found");
            await _unitOfWork.ChapterRepository.Delete(chapter);
            _unitOfWork.Commit();
            return chapter.Id;
        }

        public async Task<List<ChapterDto>> GetAll()
        {
            var chapters = await _unitOfWork.ChapterRepository.GetAll();
            return _mapper.Map<List<ChapterDto>>(chapters);
        }

        public async Task<ChapterDto> GetById(int id)
        {
            if (id <= 0) throw new CustomException(404, "Null Exception");
            var chapter = await _unitOfWork.ChapterRepository.GetEntity(x => x.Id == id);
            if (chapter == null) throw new CustomException(404, "Chapter not found");
            var chapterDto = _mapper.Map<ChapterDto>(chapter);
            return chapterDto;
        }

        public async Task<int> Update(ChapterUpdateDto chapterUpdateDto)
        {
            if (chapterUpdateDto == null) throw new CustomException(400, "Null Exception");
            var chapter = await _unitOfWork.ChapterRepository.GetEntity(x => x.Id == chapterUpdateDto.Id);
            if (chapter == null) throw new CustomException(404, "Chapter not found");

            _mapper.Map(chapterUpdateDto, chapter);

            // Check if the content has changed, and if so, recalculate pages
            if (chapterUpdateDto.Content != chapter.Content)
            {
                const int CharsPerPage = 4500;  // 4500 characters per page for A4-like formatting
                int totalChars = chapterUpdateDto.Content.Length;  // Get the number of characters in updated content
                chapter.Page = (int)Math.Ceiling((double)totalChars / CharsPerPage);  // Recalculate total pages based on characters
            }

            await _unitOfWork.ChapterRepository.Update(chapter);
            _unitOfWork.Commit();
            return chapterUpdateDto.Id;
        }
    }
}
