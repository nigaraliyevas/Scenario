namespace Scenario.Application.Dtos.CommentDtos
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public string AppUserId { get; set; }
        public string AppUserName { get; set; }
        public int ChapterId { get; set; }
        public int? ParentCommentId { get; set; }
        public List<CommentDto> Replies { get; set; } // Nested replies
    }
}
