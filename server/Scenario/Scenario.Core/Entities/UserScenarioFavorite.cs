using Scenario.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scenario.Core.Entities
{
    public class UserScenarioFavorite:BaseEntity
    {
        public Guid UserId { get; set; }       
        public Guid PlotId { get; set; }
        public bool IsFavorite { get; set; }    
        public AppUser User { get; set; }
        public Plot Plot { get; set; }
    }
}
