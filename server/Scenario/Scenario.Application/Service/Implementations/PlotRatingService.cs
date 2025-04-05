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



        //public async Task<int> Create(PlotRatingCreateDto plotRatingCreateDto)
        //{
        //    if (plotRatingCreateDto == null) throw new CustomException(400, "Invalid data");

        //    var plot = await _unitOfWork.PlotRepository.GetEntity(x => x.Id == plotRatingCreateDto.PlotId);
        //    if (plot == null) throw new CustomException(404, "Plot not found");

        //    var existingRating = await _unitOfWork.PlotRatingRepository.GetEntity(x => x.PlotId == plotRatingCreateDto.PlotId && x.UserId == plotRatingCreateDto.UserId);
        //    if (existingRating != null) throw new CustomException(400, "User has already rated this plot");

        //    var newRating = _mapper.Map<PlotRating>(plotRatingCreateDto);
        //    await _unitOfWork.PlotRatingRepository.Create(newRating);
        //    _unitOfWork.Commit();

        //    await UpdatePlotAverageRating(plotRatingCreateDto.PlotId);

        //    return newRating.Id;
        //}

        //public async Task<int> Update(PlotRatingUpdateDto plotRatingUpdateDto)
        //{
        //    if (plotRatingUpdateDto == null) throw new CustomException(400, "Invalid data");

        //    var existingRating = await _unitOfWork.PlotRatingRepository.GetEntity(x => x.Id == plotRatingUpdateDto.Id);
        //    if (existingRating == null) throw new CustomException(404, "Rating not found");

        //    existingRating.Rating = plotRatingUpdateDto.Rating;
        //    await _unitOfWork.PlotRatingRepository.Update(existingRating);
        //    _unitOfWork.Commit();

        //    await UpdatePlotAverageRating(existingRating.PlotId);

        //    return existingRating.Id;
        //}

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
            var ratings = await _unitOfWork.PlotRatingRepository.GetAll(null, "User");
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
            var plot = await _unitOfWork.PlotRepository.GetEntity(x => x.Id == plotId, "Ratings");
            if (plot == null) return;

            plot.AverageRating = plot.Ratings.Any() ? plot.Ratings.Average(r => r.Rating) : 0;
            await _unitOfWork.PlotRepository.Update(plot);
            _unitOfWork.Commit();
        }
    }
}
