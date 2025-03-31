using Scenario.Application.Dtos.CommentDtos;

namespace Scenario.Application.Dtos.UserDtos
{
    public class UserProfileDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string? Image { get; set; }
        public List<PlotDto> FavoritePlots { get; set; }
        public List<CommentDto> Comments { get; set; }
    }
}
