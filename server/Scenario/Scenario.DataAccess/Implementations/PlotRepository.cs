using Scenario.Core.Entities;
using Scenario.Core.Repositories;
using Scenario.DataAccess.Data;

namespace Scenario.DataAccess.Implementations
{
    public class PlotRepository : Repository<Plot>, IPlotRepository
    {
        public PlotRepository(ScenarioAppDbContext context) : base(context)
        {
        }
    }
}
