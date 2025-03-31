using Scenario.Core.Entities;
using Scenario.Core.Repositories;
using Scenario.DataAccess.Data;

namespace Scenario.DataAccess.Implementations
{
    public class PlotCategoryRepository : Repository<PlotCategory>, IPlotCategoryRepository
    {
        public PlotCategoryRepository(ScenarioAppDbContext context) : base(context)
        {
        }
    }
}
