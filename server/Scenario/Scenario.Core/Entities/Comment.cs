using Scenario.Core.Entities.Common;

namespace Scenario.Core.Entities
{
    public class Comment : BaseEntity
    {
        public string CommentText { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }


        public int? ParentCommentId { get; set; }

        public Comment ParentComment { get; set; }
        public List<Comment> Replies { get; set; }  // Collection of replies


    }
}
