using Scenario.Application.Dtos.ActorDtos;
using Scenario.Core.Entities;

namespace Scenario.Application.Service.Interfaces
{
    public interface IActorService
    {
        Task<int> Create(ActorCreateDto actorCreateDto);
        Task<int> Update(ActorUpdateDto actorUpdateDto, int id);
        Task<int> Delete(int id);
        Task<List<Actor>> GetAll();
        Task<List<Actor>> GetAllByMovieId(int id);
        Task<Actor> GetById(int id);
    }
}
