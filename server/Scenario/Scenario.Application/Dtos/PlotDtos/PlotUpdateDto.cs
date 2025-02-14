namespace Scenario.Application.Dtos.PlotDtos
{
    public class PlotUpdateDto
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }

        public int ScriptwriterId { get; set; }

        public bool Status { get; set; } = false;
        public List<int> CategoryIds { get; set; }
    }
}
