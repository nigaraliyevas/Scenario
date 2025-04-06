using AutoMapper;
using Scenario.Application.Dtos.PlotRatingDtos;
using Scenario.Application.Exceptions;
using Scenario.Application.Service.Interfaces;
using Scenario.Core.Entities;
using Scenario.DataAccess.Implementations.UnitOfWork;

namespace Scenario.Application.Service.Implementations
{
    public class PlotRatingService : IPlotRatingService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public PlotRatingService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> RatePlot(PlotRatingCreateDto ratingDto, string userId)
        {
            if (ratingDto == null) throw new CustomException(400, "Invalid rating data");
            if (ratingDto.Rating > 5 || ratingDto.Rating < 1)
                throw new CustomException(400, "Rating must be between 1 and 5");

            // Check if the user has already rated this plot
            var existingRating = await _unitOfWork.PlotRatingRepository
                .GetEntity(x => x.PlotId == ratingDto.PlotId && x.AppUserId == userId);

            if (existingRating == null)
            {
                var newRating = _mapper.Map<PlotRating>(ratingDto);
                newRating.AppUserId = userId;
                await _unitOfWork.PlotRatingRepository.Create(newRating);
            }
            else
            {
                existingRating.Rating = ratingDto.Rating;
                await _unitOfWork.PlotRatingRepository.Update(existingRating);
            }

            _unitOfWork.Commit();

            await UpdatePlotAverageRating(ratingDto.PlotId);

            return ratingDto.PlotId;
        }



        public async Task<int> Delete(int id)
        {
            var rating = await _unitOfWork.PlotRatingRepository.GetEntity(x => x.Id == id);
            if (rating == null) throw new CustomException(404, "Rating not found");

            int plotId = rating.PlotId;
            await _unitOfWork.PlotRatingRepository.Delete(rating);
            _unitOfWork.Commit();

            await UpdatePlotAverageRating(plotId);

            return rating.Id;
        }

        public async Task<List<PlotRatingDto>> GetAll()
        {
            var ratings = await _unitOfWork.PlotRatingRepository.GetAll(null, "AppUser");
            return _mapper.Map<List<PlotRatingDto>>(ratings);
        }

        public async Task<PlotRatingDto> GetById(int id)
        {
            var rating = await _unitOfWork.PlotRatingRepository.GetEntity(x => x.Id == id, "User");
            if (rating == null) throw new CustomException(404, "Rating not found");

            return _mapper.Map<PlotRatingDto>(rating);
        }

        private async Task UpdatePlotAverageRating(int plotId)
        {
            var ratings = await _unitOfWork.PlotRatingRepository
                .GetEntity(x => x.PlotId == plotId);

            if (ratings == null) throw new CustomException(400, "Null exception");

            double average = ratings.Rating;

            var plot = await _unitOfWork.PlotRepository.GetEntity(p => p.Id == plotId);
            if (plot == null) throw new CustomException(404, "Plot not found");

            plot.AverageRating = Math.Round(average, 2);
            await _unitOfWork.PlotRepository.Update(plot);

            _unitOfWork.Commit();
        }

    }
}
