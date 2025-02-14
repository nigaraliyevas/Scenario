using Scenario.Core.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace Scenario.Core.Entities
{
    public class PlotRating : BaseEntity
    {
        public int PlotId { get; set; }
        public Plot Scenario { get; set; }

        public string UserId { get; set; }
        public AppUser User { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }
    }
}
