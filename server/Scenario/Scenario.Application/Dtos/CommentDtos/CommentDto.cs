namespace Scenario.Application.Dtos.CommentDtos
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string CommentText { get; set; }
        public DateTime CreatedAt { get; set; }
        public string AppUser { get; set; }
        public string AppUserId { get; set; }
        public string UserImg { get; set; }
    }
}
