using MovieApp.DataAccess.Implementations;
using Scenario.Core.Entities;
using Scenario.Core.Repositories;
using Scenario.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scenario.DataAccess.Implementations
{
    public class LikeDislikeRepository : Repository<LikeDislike>, ILikeDislikeRepository
    {
        public LikeDislikeRepository(ScenarioAppDbContext context) : base(context)
        {
        }
    }
}
