using Scenario.Core.Entities;
using Scenario.Core.Repositories;
using Scenario.DataAccess.Data;

namespace Scenario.DataAccess.Implementations
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(ScenarioAppDbContext context) : base(context) { }
    }
}
