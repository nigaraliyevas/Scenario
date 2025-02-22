using MovieApp.DataAccess.Implementations;
using Scenario.Core.Entities;
using Scenario.Core.Repositories;
using Scenario.DataAccess.Data;

namespace Scenario.DataAccess.Implementations
{
    public class UserScenarioFavoriteRepository:Repository<UserScenarioFavorite>, IUserScenarioFavoriteRepository
    {
        public UserScenarioFavoriteRepository(ScenarioAppDbContext context):base(context)
        {

        }
    }
}
