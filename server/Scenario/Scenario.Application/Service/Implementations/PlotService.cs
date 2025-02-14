using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Scenario.Application.Dtos.PlotDtos;
using Scenario.Application.Exceptions;
using Scenario.Application.Service.Interfaces;
using Scenario.Core.Entities;
using Scenario.DataAccess.Implementations.UnitOfWork;

namespace Scenario.Application.Service.Implementations
{
    public class PlotService : IPlotService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public PlotService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<int> Create(PlotCreateDto createPlotDto)
        {
            if (createPlotDto == null) throw new CustomException(400, "Invalid plot data");
            var newPlot = _mapper.Map<Plot>(createPlotDto);
            await _unitOfWork.PlotRepository.Create(newPlot);
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

        public async Task<List<Plot>> GetAll()
        {
            var plots = await _unitOfWork.PlotRepository.GetAll();
            if (plots == null) throw new CustomException(404, "Not Found");
            return plots;
        }

        public async Task<PlotListDto> GetAllByCategoryName(string categoryName, int itemPerPage, int page)
        {
            if (string.IsNullOrEmpty(categoryName)) throw new CustomException(400, "Category name cannot be null or empty");

            IQueryable<Plot> query = _unitOfWork.PlotRepository.GetAllAsQeuryable(null, "Scriptwriters");

            query = query.Where(x => x.PlotCategories.Any(pc => pc.Categories.CategoryName.ToLower() == categoryName.ToLower()));

            int totalCount = await query.CountAsync();

            List<Plot> paginatedPlots = await query
                .Skip((page - 1) * itemPerPage)
                .Take(itemPerPage)
                .ToListAsync();

            if (!paginatedPlots.Any()) throw new CustomException(404, "No plots found for the given category");

            List<PlotDto> plotDtos = _mapper.Map<List<PlotDto>>(paginatedPlots);

            PlotListDto plotListDto = new()
            {
                Page = page,
                TotalCount = totalCount,
                Items = plotDtos
            };

            return plotListDto;
        }


        public async Task<Plot> GetById(int id)
        {
            if (id <= 0 || id == null) throw new CustomException(404, "Null Exception");
            var plot = await _unitOfWork.PlotRepository.GetEntity(x => x.Id == id);
            if (plot == null) throw new CustomException(404, "Not Found");
            plot.ReadCount++;
            await _unitOfWork.PlotRepository.Update(plot);
            return plot;
        }

        public async Task<Plot> GetByName(string name)
        {
            if (name == null) throw new CustomException(400, "Null Exception");
            var plotName = await _unitOfWork.PlotRepository.GetEntity(x => x.Header == name);
            if (plotName == null) throw new CustomException(404, "Not Found");
            return plotName;
        }

        public async Task<PlotListDto> GetAllByNameOrScriptwriter(string search, int itemPerPage, int page)
        {
            if (search == null) throw new CustomException(400, "Null Exception");
            IQueryable<Plot> query = _unitOfWork.PlotRepository.GetAllAsQeuryable(
                     null,
                     "Scriptwriters"
                 );

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(x => x.Header.ToLower().Contains(search.ToLower()) || x.Scriptwriter.Name.ToLower().Contains(search.ToLower()) || x.Scriptwriter.Surname.ToLower().Contains(search.ToLower()));
            }

            int totalCount = query.Count();

            List<Plot> paginatedMovies = query
                .Skip((page - 1) * itemPerPage)
                .Take(itemPerPage)
                .ToList();
            if (paginatedMovies == null) throw new CustomException(404, "Not Found");

            List<PlotDto> plotDtos = _mapper.Map<List<PlotDto>>(paginatedMovies);


            PlotListDto plotListDto = new()
            {
                Page = page,
                TotalCount = totalCount,
                Items = plotDtos
            };

            return plotListDto;
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
    }
}
