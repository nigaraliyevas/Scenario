namespace Scenario.Application.Dtos.ChapterDtos
{
    public class ChapterDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Dictionary<string, string> Content { get; set; }
        public int Page { get; set; }
        public int PlotId { get; set; }
    }
}
