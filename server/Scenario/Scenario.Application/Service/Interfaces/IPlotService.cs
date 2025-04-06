using Scenario.Application.Dtos.PlotDtos;

namespace Scenario.Application.Service.Interfaces
{
    public interface IPlotService
    {
        Task<int> Create(PlotCreateDto plotCreateDto);
        Task<int> Update(PlotUpdateDto plotUpdateDto);
        Task<int> Delete(int id);
        Task<List<PlotDto>> GetAll();
        Task<PlotDto> GetById(int id);
        Task<PlotListDto> GetAllByCategoryName(string categoryName, int itemPerPage, int page); //for get "choxsayli,tamamlanmis,senedli"
        Task<PlotDto> GetByName(string name); //for filter
        Task<PlotListDto> GetAllByNameOrScriptwriter(string search, int itemPerPage, int page); //for filter

    }
}
