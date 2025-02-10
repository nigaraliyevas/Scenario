namespace Scenario.Application.Dtos.CommentDtos
{
    public class CommentCreateDto
    {
        public string Text { get; set; }
        public string AppUserId { get; set; }
        public int? MovieId { get; set; }
        public int? ParentCommentId { get; set; }  // for replies 
    }
}
