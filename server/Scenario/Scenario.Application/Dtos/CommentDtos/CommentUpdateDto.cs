namespace Scenario.Application.Dtos.CommentDtos
{
    public class CommentUpdateDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int ChapterId { get; set; }
        public int? ParentCommentId { get; set; }
        public string AppUserId { get; set; }

    }
}
