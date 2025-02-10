using Scenario.Core.Entities.Common;

namespace Scenario.Core.Entities
{
    public class Comment : BaseEntity
    {
        public string Content { get; set; }

        public string ChapterId { get; set; }
        public int? CommentId { get; set; }
        public string AppUserId { get; set; }


        public AppUser AppUser { get; set; }
        public Comment ParentComment { get; set; }
        public ICollection<Comment> Replies { get; set; }

        public ICollection<LikeDislike> LikeDislikes { get; set; }

        /*public string CommentText { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }


        public int? ParentCommentId { get; set; }

        public Comment ParentComment { get; set; }
        public List<Comment> Replies { get; set; }  // Collection of replies
        */



        
    }
}
