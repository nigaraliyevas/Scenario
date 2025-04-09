using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Scenario.Application.Dtos.PlotDtos;
using Scenario.Application.Exceptions;
using Scenario.Application.Extensions.Extension;
using Scenario.Application.Service.Interfaces;
using Scenario.Core.Entities;
using Scenario.Core.Entities.Common;
using Scenario.Core.Repositories;
using Scenario.DataAccess.Implementations.UnitOfWork;

namespace Scenario.Application.Service.Implementations
{
    public class PlotService : IPlotService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PlotService(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<int> Create(PlotCreateDto createPlotDto)
        {
            if (createPlotDto == null) throw new CustomException(400, "Invalid plot data");
            string userImage = null;
            if (!string.IsNullOrEmpty(createPlotDto.Image))
            {
                if (!createPlotDto.Image.CheckContentType("image"))
                    throw new CustomException(400, "The file has to be img");

                if (createPlotDto.Image.CheckSize(1024))
                    throw new CustomException(400, "The file is too large");

                userImage = await createPlotDto.Image.SaveFile("userImages", _httpContextAccessor);
            }
            var newPlot = _mapper.Map<Plot>(createPlotDto);

            createPlotDto.Image = userImage;


            await _unitOfWork.PlotRepository.Create(newPlot);

            await SavePivotTables(newPlot.PlotCategories, _unitOfWork.PlotCategoryRepository);


            _unitOfWork.Commit();

            return newPlot.Id;
        }


        public async Task<int> Delete(int id)
        {
            if (id <= 0 || id == null) throw new CustomException(404, "Null Exception");
            var plot = await _unitOfWork.PlotRepository.GetEntity(x => x.Id == id);
            if (plot == null) throw new CustomException(404, "Not Found");
            await _unitOfWork.PlotRepository.Delete(plot);
            _unitOfWork.Commit();
            return plot.Id;
        }

        public async Task<int> Update(PlotUpdateDto plotUpdateDto)
        {
            if (plotUpdateDto == null) throw new CustomException(404, "Null Exception");
            var existPlot = await _unitOfWork.PlotRepository.GetEntity(x => x.Id == plotUpdateDto.Id);
            if (existPlot == null) throw new CustomException(404, "Not Found");
            existPlot.UpdatedDate = DateTime.Now;
            await _unitOfWork.PlotRepository.Update(existPlot);
            _unitOfWork.Commit();
            return existPlot.Id;
        }
        public async Task<List<PlotDto>> GetAll()
        {
            var plots = await _unitOfWork.PlotRepository.GetAll(null, "PlotCategories.Category");
            if (plots == null || !plots.Any()) throw new CustomException(404, "Not Found");

            var plotIds = plots.Select(p => p.Id).ToList();

            var chapters = await _unitOfWork.ChapterRepository
                .GetAll(x => plotIds.Contains(x.PlotId));

            var commentCounts = chapters
                .GroupBy(c => c.PlotId)
                .ToDictionary(g => g.Key, g => g.Sum(c => c.Comments?.Count() ?? 0));

            var plotRatings = await _unitOfWork.PlotRatingRepository
                .GetAll(x => plotIds.Contains(x.PlotId));

            var plotRatingsByPlot = plotRatings
                .GroupBy(r => r.PlotId)
                .ToDictionary(g => g.Key, g => g.Average(r => r.Rating));

            foreach (var plot in plots)
            {
                plot.CommentedCount = commentCounts.ContainsKey(plot.Id) ? commentCounts[plot.Id] : 0;

                plot.AverageRating = plotRatingsByPlot.ContainsKey(plot.Id)
                    ? Math.Round(plotRatingsByPlot[plot.Id], 2)
                    : 0;
            }
            var plotDtos = plots.Select(plot => new PlotDto
            {
                Id = plot.Id,
                Header = plot.Header,
                Image = plot.Image,
                Description = plot.Description,
                ReadCount = plot.ReadCount,
                Status = plot.Status,
                AverageRating = plot.AverageRating,
                CommentedCount = plot.CommentedCount,
                ScriptwriterId = plot.ScriptwriterId,
                ScriptwriterName = plot.Scriptwriter?.Name ?? "",
                //Scriptwriter = plot.Scriptwriter,
                //Chapters = plot.Chapters,
                PlotCategories = plot?.PlotCategories?.Select(x => x.Category.CategoryName)?.ToList(),
                CategoryIds = plot?.PlotCategories?.Select(pc => pc.CategoryId)?.ToList(),
                CreatedDate = plot.CreatedDate,
            }).ToList();

            return plotDtos;
        }
        public async Task<PlotListDto> GetAllByCategoryName(string categoryName, int itemPerPage, int page)
        {
            if (string.IsNullOrEmpty(categoryName)) throw new CustomException(400, "Category name cannot be null or empty");

            IQueryable<Plot> query = _unitOfWork.PlotRepository.GetAllAsQeuryable(null, "Scriptwriter", "Chapters", "PlotRatings");

            query = query.Where(x => x.PlotCategories.Any(pc => pc.Category.CategoryName.ToLower() == categoryName.ToLower()));

            int totalCount = await query.CountAsync();

            List<Plot> paginatedPlots = await query
                .Skip((page - 1) * itemPerPage)
                .Take(itemPerPage)
                .ToListAsync();

            if (!paginatedPlots.Any()) throw new CustomException(404, "No plots found for the given category");

            var plotIds = paginatedPlots.Select(p => p.Id).ToList();
            var chapters = await _unitOfWork.ChapterRepository
                .GetAll(x => plotIds.Contains(x.PlotId), "Comments");

            var commentCounts = chapters
                .GroupBy(c => c.PlotId)
                .ToDictionary(g => g.Key, g => g.Sum(c => c.Comments?.Count() ?? 0));

            var plotRatings = await _unitOfWork.PlotRatingRepository
                .GetAll(x => plotIds.Contains(x.PlotId));

            var plotRatingsByPlot = plotRatings
                .GroupBy(r => r.PlotId)
                .ToDictionary(g => g.Key, g => g.Average(r => r.Rating));

            foreach (var plot in paginatedPlots)
            {
                plot.CommentedCount = commentCounts.ContainsKey(plot.Id) ? commentCounts[plot.Id] : 0;
                plot.AverageRating = plotRatingsByPlot.ContainsKey(plot.Id)
                    ? Math.Round(plotRatingsByPlot[plot.Id], 2)
                    : 0;
            }

            List<PlotDto> plotDtos = _mapper.Map<List<PlotDto>>(paginatedPlots);

            return new PlotListDto
            {
                Page = page,
                TotalCount = totalCount,
                Items = plotDtos
            };
        }


        public async Task<PlotDto> GetById(int id)
        {
            if (id <= 0) throw new CustomException(400, "Invalid ID");

            var plot = await _unitOfWork.PlotRepository.GetEntity(x => x.Id == id, "PlotRatings");
            if (plot == null) throw new CustomException(404, "Not Found");

            plot.ReadCount++;

            var chapters = await _unitOfWork.ChapterRepository
                .GetAll(x => x.PlotId == id, "Comments");

            plot.CommentedCount = chapters.Sum(c => c.Comments?.Count() ?? 0);

            var plotRatings = await _unitOfWork.PlotRatingRepository
                .GetAll(x => x.PlotId == id);
            plot.AverageRating = plotRatings.Any()
                ? Math.Round(plotRatings.Average(r => r.Rating), 2)
                : 0;

            await _unitOfWork.PlotRepository.Update(plot);
            var plotDto = _mapper.Map<PlotDto>(plot);
            return plotDto;
        }

        public async Task<PlotDto> GetByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new CustomException(400, "Name cannot be null or empty");

            var plotName = await _unitOfWork.PlotRepository.GetEntity(x => x.Header == name, "PlotRatings");
            if (plotName == null) throw new CustomException(404, "Not Found");

            var plotRatings = await _unitOfWork.PlotRatingRepository
                .GetAll(x => x.PlotId == plotName.Id);
            plotName.AverageRating = plotRatings.Any()
                ? Math.Round(plotRatings.Average(r => r.Rating), 2)
                : 0;
            var plotDto = _mapper.Map<PlotDto>(plotName);

            return plotDto;
        }

        public async Task<PlotListDto> GetAllByNameOrScriptwriter(string search, int itemPerPage, int page)
        {
            if (string.IsNullOrEmpty(search))
                throw new CustomException(400, "Search parameter cannot be null or empty.");

            IQueryable<Plot> query = _unitOfWork.PlotRepository.GetAllAsQeuryable(
                null,
                "Scriptwriter",
                "Chapters",
                "PlotRatings"
            );

            query = query.Where(x => x.Header.ToLower().Contains(search.ToLower())
                                   || x.Scriptwriter.Name.ToLower().Contains(search.ToLower())
                                   || x.Scriptwriter.Surname.ToLower().Contains(search.ToLower()));

            int totalCount = await query.CountAsync();

            List<Plot> paginatedPlots = await query
                .Skip((page - 1) * itemPerPage)
                .Take(itemPerPage)
                .ToListAsync();

            if (!paginatedPlots.Any())
                throw new CustomException(404, "Not Found");

            var plotIds = paginatedPlots.Select(p => p.Id).ToList();
            var chapters = await _unitOfWork.ChapterRepository
                .GetAll(x => plotIds.Contains(x.PlotId), "Comments");

            var commentCounts = chapters
                .GroupBy(c => c.PlotId)
                .ToDictionary(g => g.Key, g => g.Sum(c => c.Comments?.Count() ?? 0));

            var plotRatings = await _unitOfWork.PlotRatingRepository
                .GetAll(x => plotIds.Contains(x.PlotId));

            var plotRatingsByPlot = plotRatings
                .GroupBy(r => r.PlotId)
                .ToDictionary(g => g.Key, g => g.Average(r => r.Rating));

            foreach (var plot in paginatedPlots)
            {
                plot.CommentedCount = commentCounts.ContainsKey(plot.Id) ? commentCounts[plot.Id] : 0;
                plot.AverageRating = plotRatingsByPlot.ContainsKey(plot.Id)
                    ? Math.Round(plotRatingsByPlot[plot.Id], 2)
                    : 0;
            }

            List<PlotDto> plotDtos = _mapper.Map<List<PlotDto>>(paginatedPlots);

            return new PlotListDto
            {
                Page = page,
                TotalCount = totalCount,
                Items = plotDtos
            };
        }

        private async Task SavePivotTables<T>(List<T> entities, IRepository<T> repository) where T : BaseEntity
        {
            if (entities != null)
            {
                foreach (var entity in entities)
                {
                    await repository.Create(entity);
                }
            }
        }
    }
}
