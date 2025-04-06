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
        public async Task<ChapterDto> Create(ChapterCreateDto chapterCreateDto)
        {
            if (chapterCreateDto == null) throw new CustomException(400, "Cannot be null");
            if (string.IsNullOrWhiteSpace(chapterCreateDto.Content))
                throw new CustomException(400, "Chapter content cannot be empty");

            const int CharsPerPage = 4500;
            int totalChars = chapterCreateDto.Content.Length;
            int totalPages = (int)Math.Ceiling((double)totalChars / CharsPerPage);

            var chapter = _mapper.Map<Chapter>(chapterCreateDto);
            chapter.Page = totalPages;

            await _unitOfWork.ChapterRepository.Create(chapter);
            _unitOfWork.Commit();

            var paginatedContent = new Dictionary<string, string>();
            for (int i = 0; i < totalPages; i++)
            {
                int start = i * CharsPerPage;
                int length = Math.Min(CharsPerPage, totalChars - start);
                string pageContent = chapterCreateDto.Content.Substring(start, length);
                paginatedContent.Add($"page {i + 1}", pageContent);
            }

            var chapterDto = _mapper.Map<ChapterDto>(chapter);
            chapterDto.Content = paginatedContent;
            return chapterDto;
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
            const int CharsPerPage = 4500;

            var chapters = await _unitOfWork.ChapterRepository.GetAll();

            var chapterDtos = new List<ChapterDto>();

            foreach (var chapter in chapters)
            {
                var content = chapter.Content ?? "";
                int totalChars = content.Length;
                int totalPages = (int)Math.Ceiling((double)totalChars / CharsPerPage);
                var paginatedContent = new Dictionary<string, string>();
                for (int i = 0; i < totalPages; i++)
                {
                    int start = i * CharsPerPage;
                    int length = Math.Min(CharsPerPage, totalChars - start);
                    string pageContent = content.Substring(start, length);
                    paginatedContent.Add($"page {i + 1}", pageContent);
                }

                var dto = _mapper.Map<ChapterDto>(chapter);
                dto.Page = totalPages;
                dto.Content = paginatedContent;

                chapterDtos.Add(dto);
            }

            return chapterDtos;
        }

        public async Task<ChapterDto> GetById(int id)
        {
            if (id <= 0) throw new CustomException(404, "Null Exception");
            const int CharsPerPage = 4500;
            var chapter = await _unitOfWork.ChapterRepository.GetEntity(x => x.Id == id);
            if (chapter == null)
                throw new CustomException(404, "Chapter not found");

            var content = chapter.Content ?? "";
            int totalChars = content.Length;
            int totalPages = (int)Math.Ceiling((double)totalChars / CharsPerPage);

            var paginatedContent = new Dictionary<string, string>();
            for (int i = 0; i < totalPages; i++)
            {
                int start = i * CharsPerPage;
                int length = Math.Min(CharsPerPage, totalChars - start);
                string pageContent = content.Substring(start, length);
                paginatedContent.Add($"page {i + 1}", pageContent);
            }
            var chapterDto = _mapper.Map<ChapterDto>(chapter);
            chapterDto.Page = totalPages;
            chapterDto.Content = paginatedContent;

            return chapterDto;


        }

        public async Task<ChapterDto> Update(ChapterUpdateDto chapterUpdateDto)
        {
            if (chapterUpdateDto.Id <= 0 || chapterUpdateDto == null) throw new CustomException(400, "Cannot be null");
            if (string.IsNullOrWhiteSpace(chapterUpdateDto.Content))
                throw new CustomException(400, "Chapter content cannot be empty");

            var existingChapter = await _unitOfWork.ChapterRepository.GetEntity(x => x.Id == chapterUpdateDto.Id);
            if (existingChapter == null)
                throw new CustomException(404, "Chapter not found");

            const int CharsPerPage = 4500;
            int totalChars = chapterUpdateDto.Content.Length;
            int totalPages = (int)Math.Ceiling((double)totalChars / CharsPerPage);

            existingChapter.Name = chapterUpdateDto.Name;
            existingChapter.Content = chapterUpdateDto.Content;
            existingChapter.Page = totalPages;

            await _unitOfWork.ChapterRepository.Update(existingChapter);
            _unitOfWork.Commit();

            var paginatedContent = new Dictionary<string, string>();
            for (int i = 0; i < totalPages; i++)
            {
                int start = i * CharsPerPage;
                int length = Math.Min(CharsPerPage, totalChars - start);
                string pageContent = chapterUpdateDto.Content.Substring(start, length);
                paginatedContent.Add($"page {i + 1}", pageContent);
            }

            var chapterDto = _mapper.Map<ChapterDto>(existingChapter);
            chapterDto.Content = paginatedContent;
            return chapterDto;
        }
    }
}
