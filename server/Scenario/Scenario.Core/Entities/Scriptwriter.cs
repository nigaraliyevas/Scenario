using Scenario.Core.Entities.Common;

namespace Scenario.Core.Entities
{
    public class Scriptwriter : BaseEntity
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? About { get; set; }
        public int PlotCount { get; set; }
        public DateTime? BirthDay { get; set; }
        public List<Plot> Plots { get; set; }
    }
}
