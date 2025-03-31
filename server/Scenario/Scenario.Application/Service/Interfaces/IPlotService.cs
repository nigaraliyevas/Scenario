using Scenario.Application.Dtos.PlotDtos;
using Scenario.Core.Entities;

namespace Scenario.Application.Service.Interfaces
{
    public interface IPlotService
    {
        Task<int> Create(PlotCreateDto plotCreateDto);
        Task<int> Update(PlotUpdateDto plotUpdateDto);
        Task<int> Delete(int id);
        Task<List<Plot>> GetAll();
        Task<Plot> GetById(int id);
        Task<PlotListDto> GetAllByCategoryName(string categoryName, int itemPerPage, int page); //for get "choxsayli,tamamlanmis,senedli"
        Task<Plot> GetByName(string name); //for filter
        Task<PlotListDto> GetAllByNameOrScriptwriter(string search, int itemPerPage, int page); //for filter

    }
}
