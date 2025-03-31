namespace Scenario.Application.Dtos.ChapterDtos
{
    public class ChapterListDto
    {
        public int Page { get; set; }
        public int TotalCount { get; set; }
        public List<ChapterDto> Items { get; set; }
    }
}
