using Scenario.Core.Entities.Common;

namespace Scenario.Core.Entities
{
    public class PlotCategory : BaseEntity
    {
        public int PlotId { get; set; }
        public Plot Plot { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
