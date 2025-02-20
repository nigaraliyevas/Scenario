using Scenario.Core.Entities.Common;

namespace Scenario.Core.Entities
{
    public class UserScenarioFavorite:BaseEntity
    {
        public Guid UserId { get; set; }       
        public int PlotId { get; set; }
        //public bool IsFavorite { get; set; }    
        public AppUser User { get; set; }
        public Plot Plot { get; set; }
    }
}
