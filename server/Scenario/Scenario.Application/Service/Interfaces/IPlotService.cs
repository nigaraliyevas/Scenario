using Scenario.Application.Dtos.ActorDtos;
using Scenario.Core.Entities;

namespace Scenario.Application.Service.Interfaces
{
    public interface IPlotService
    {
        Task<int> Create(ActorCreateDto actorCreateDto);
        Task<int> Update(ActorUpdateDto actorUpdateDto, int id);
        Task<int> Delete(int id);
        Task<List<Plot>> GetAll();
        Task<List<Plot>> GetAllByMovieId(int id);
        Task<Plot> GetById(int id);
    }
}
