namespace Scenario.Application.Dtos.CommentDtos
{
    public class CommentCreateDto
    {
        public string AppUserId { get; set; }

        public string Content { get; set; }
        public int ChapterId { get; set; }
        public int? ParentCommentId { get; set; } // Optional (for replies)

    }
}
