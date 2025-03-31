using Scenario.Core.Entities.Common;

namespace Scenario.Core.Entities
{
    public class Comment : BaseEntity
    {
        public string Content { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }

        public int ChapterId { get; set; }
        public Chapter Chapter { get; set; }

        public int? ParentCommentId { get; set; }
        public Comment ParentComment { get; set; }
        public List<Comment> Replies { get; set; }


    }
}
