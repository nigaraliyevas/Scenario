using Scenario.Core.Entities.Common;

namespace Scenario.Core.Entities
{
    public class Chapter : BaseEntity
    {
        public string Name { get; set; }
        public string Content { get; set; }
        public int Page { get; set; }


        public int PlotId { get; set; }
        public Plot Plot { get; set; }
        public List<Comment> Comments { get; set; }

    }
}
