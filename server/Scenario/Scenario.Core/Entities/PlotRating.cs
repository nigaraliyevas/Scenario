using Scenario.Core.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace Scenario.Core.Entities
{
    public class PlotRating : BaseEntity
    {
        public int PlotId { get; set; }
        public Plot Plot { get; set; }

        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }
    }
}
