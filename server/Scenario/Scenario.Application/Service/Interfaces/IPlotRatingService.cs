using Scenario.Application.Dtos.PlotRatingDtos;

namespace Scenario.Application.Service.Interfaces
{
    public interface IPlotRatingService
    {
        Task<int> RatePlot(PlotRatingCreateDto ratingDto, string userId);
        Task<int> Delete(int id);
        Task<List<PlotRatingDto>> GetAll();
        Task<PlotRatingDto> GetById(int id);
    }
}
