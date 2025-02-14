namespace Scenario.Application.Dtos.PlotDtos
{
    public class PlotCreateDto
    {
        public string Header { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }


        public bool Status { get; set; } = false;
        public int ScriptwriterId { get; set; }
        public List<int> CategoryIds { get; set; } // List of category IDs
    }
}
