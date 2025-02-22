using MovieApp.DataAccess.Implementations;
using Scenario.Core.Entities;
using Scenario.Core.Repositories;
using Scenario.DataAccess.Data;

namespace Scenario.DataAccess.Implementations
{
    public class AboutTestimonialRepository:Repository<AboutTestimonial>, IAboutTestimonialRepository
    {
        public AboutTestimonialRepository(ScenarioAppDbContext context):base(context)
        {

        }
    }
}
