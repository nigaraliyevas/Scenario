using MovieApp.Core.Repositories;
using MovieApp.DataAccess.Implementations;
using Scenario.Core.Entities;
using Scenario.DataAccess.Data;

namespace Scenario.DataAccess.Implementations
{
    public class ActorRepository : Repository<Actor>, IActorRepository
    {
        public ActorRepository(ScenarioAppDbContext context) : base(context)
        {
        }
    }
}
