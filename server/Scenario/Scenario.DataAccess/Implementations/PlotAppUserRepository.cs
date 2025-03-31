using Scenario.Core.Entities;
using Scenario.Core.Repositories;
using Scenario.DataAccess.Data;

namespace Scenario.DataAccess.Implementations
{
    public class PlotAppUserRepository : Repository<PlotAppUser>, IPlotAppUserRepository
    {
        public PlotAppUserRepository(ScenarioAppDbContext context) : base(context)
        {
        }
    }
}
