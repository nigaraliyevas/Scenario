using Scenario.Core.Entities;
using Scenario.Core.Repositories;
using Scenario.DataAccess.Data;

namespace Scenario.DataAccess.Implementations
{
    public class AdRepository : Repository<Ad>, IAdRepository
    {
        public AdRepository(ScenarioAppDbContext context) : base(context)
        {
        }
    }
}
