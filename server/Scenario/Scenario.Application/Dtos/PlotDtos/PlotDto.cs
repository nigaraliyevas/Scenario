using Scenario.Application.Dtos.CategoryDtos;
using Scenario.Application.Dtos.PlotRatingDtos;
using Scenario.Core.Entities;

public class PlotDto
{
    public int Id { get; set; }
    public string Header { get; set; }
    public string Image { get; set; }
    public string Description { get; set; }

    public List<int> CategoryIds { get; set; }
    public List<CategoryDto> Categories { get; set; }

    public double AverageRating { get; set; }
    public int ReadCount { get; set; }
    public bool Status { get; set; }

    public int ScriptwriterId { get; set; }
    public string ScriptwriterName { get; set; }
    public Scriptwriter Scriptwriter { get; set; }

    public List<Chapter> Chapters { get; set; }
    public List<PlotCategory> PlotCategories { get; set; }

    public List<PlotRatingDto> Ratings { get; set; }
    public int CommentedCount { get; set; }

}
