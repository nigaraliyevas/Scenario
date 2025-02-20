namespace Scenario.Application.Dtos.LikeDislikeDtos
{
    public class LikeDislikeDto
    {
        public int Id { get; set; }
        public int CommentId { get; set; }
        public int LikeCount { get; set; } 
        public int DislikeCount { get; set; }
    }
}
