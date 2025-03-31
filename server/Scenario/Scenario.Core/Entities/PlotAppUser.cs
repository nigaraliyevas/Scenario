using Scenario.Core.Entities.Common;

namespace Scenario.Core.Entities
{
    public class PlotAppUser : BaseEntity
    {
        public int PlotId { get; set; }
        public Plot Plot { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public bool IsFavorite { get; set; }
    }
}
