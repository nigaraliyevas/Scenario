using Scenario.Core.Entities.Common;

namespace Scenario.Core.Entities
{
    public class Plot : BaseEntity
    {
        public string Header { get; set; }
        public string? Image { get; set; }
        public string Description { get; set; }
        //public string CategoryName { get; set; }


        public List<PlotRating> PlotRatings { get; set; }//one to many with plot
        public double AverageRating;

        public int ReadCount { get; set; }
        public bool Status { get; set; } //admin panel uchun aktiv deaktiv
        public int ScriptwriterId { get; set; }
        public Scriptwriter Scriptwriter { get; set; }

        public List<Chapter> Chapters { get; set; }

        public List<PlotCategory> PlotCategories { get; set; }
        public List<PlotAppUser> PlotAppUsers { get; set; }
        public int CommentedCount { get; set; }


    }
}
