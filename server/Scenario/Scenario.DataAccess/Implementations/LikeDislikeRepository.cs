using Scenario.Core.Entities;
using Scenario.Core.Repositories;
using Scenario.DataAccess.Data;

namespace Scenario.DataAccess.Implementations
{
    public class LikeDislikeRepository : Repository<LikeDislike>, ILikeDislikeRepository
    {
        public LikeDislikeRepository(ScenarioAppDbContext context) : base(context)
        {
        }
    }
}
