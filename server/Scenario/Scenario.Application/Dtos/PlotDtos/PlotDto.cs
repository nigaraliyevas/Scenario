using Scenario.Application.Dtos.CategoryDtos;
using Scenario.Application.Dtos.PlotRatingDtos;
using Scenario.Core.Entities;

namespace Scenario.Application.Dtos.PlotDtos
{
    public class PlotDto
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }

        //public List<PlotRating> Ratings { get; set; }//one to many with plot
        public double AverageRating;

        public int ReadCount { get; set; }
        public bool Status { get; set; }

        public int ScriptwriterId { get; set; }
        public string ScriptwriterName { get; set; } // Include scriptwriter name
        public Scriptwriter Scriptwriter { get; set; }

        public List<Chapter> Chapters { get; set; }

        public List<PlotCategory> PlotCategories { get; set; }
        public List<CategoryDto> Categories { get; set; }

        public List<PlotRatingDto> Ratings { get; set; } // Include ratings


    }
}
