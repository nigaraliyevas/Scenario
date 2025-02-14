using Scenario.Core.Entities;
using Scenario.Core.Repositories;
using Scenario.DataAccess.Data;

namespace Scenario.DataAccess.Implementations
{
    public class ScriptwriterRepository : Repository<Scriptwriter>, IScriptwriterRepository
    {
        public ScriptwriterRepository(ScenarioAppDbContext context) : base(context)
        {
        }
    }
}
