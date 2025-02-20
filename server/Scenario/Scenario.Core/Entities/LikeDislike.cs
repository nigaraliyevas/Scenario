using Scenario.Core.Entities.Common;

namespace Scenario.Core.Entities
{
    public class LikeDislike:BaseEntity
    {
        public int CommentId { get; set; }
        public int LikeCount { get; set; }
        public int DislikeCount { get; set; }

        //public string AppUserId { get; set; }
        //public bool IsLike { get; set; }
    }
}
