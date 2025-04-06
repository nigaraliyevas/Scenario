namespace Scenario.Application.Dtos.PlotRatingDtos
{
    public class PlotRatingDto
    {
        public int Id { get; set; }
        public int PlotId { get; set; }
        public string AppUserId { get; set; }
        public string UserName { get; set; }
        public int Rating { get; set; }
    }
}
