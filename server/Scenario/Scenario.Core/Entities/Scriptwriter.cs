using Scenario.Core.Entities.Common;

namespace Scenario.Core.Entities
{
    public class Scriptwriter : BaseEntity
    {
        public string Fullname { get; set; }
        public string About { get; set; }
        public List<Plot> Plots { get; set; }
    }
}
