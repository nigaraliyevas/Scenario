using Scenario.Core.Entities.Common;

namespace Scenario.Core.Entities
{
    public class Category : BaseEntity
    {
        public string CategoryName { get; set; }
        public List<PlotCategory> PlotCategories { get; set; }
    }
}
