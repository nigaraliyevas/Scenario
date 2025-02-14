namespace Scenario.Application.Dtos.PlotDtos
{
    public class PlotListDto
    {
        public int Page { get; set; }
        public int TotalCount { get; set; }
        public List<PlotDto> Items { get; set; }

    }
}
