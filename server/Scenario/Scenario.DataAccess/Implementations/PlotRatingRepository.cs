using Scenario.Core.Entities;
using Scenario.Core.Repositories;
using Scenario.DataAccess.Data;

namespace Scenario.DataAccess.Implementations
{
    public class PlotRatingRepository : Repository<PlotRating>, IPlotRatingRepository
    {
        public PlotRatingRepository(ScenarioAppDbContext context) : base(context)
        {
        }
    }
}
