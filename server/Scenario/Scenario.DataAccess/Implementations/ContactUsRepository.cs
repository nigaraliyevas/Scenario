using Scenario.Core.Entities;
using Scenario.Core.Repositories;
using Scenario.DataAccess.Data;

namespace Scenario.DataAccess.Implementations
{
    public class ContactUsRepository : Repository<ContactUs>, IContactUsRepository

    {
        public ContactUsRepository(ScenarioAppDbContext context) : base(context)
        {
        }
    }
}
