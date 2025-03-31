namespace Scenario.Application.Dtos.CommentDtos
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; } // For displaying user info
        public int ChapterId { get; set; }
        public int? ParentCommentId { get; set; }
        public List<CommentDto> Replies { get; set; } // Nested replies
    }
}
