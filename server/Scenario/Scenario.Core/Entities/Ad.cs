using Scenario.Core.Entities.Common;

namespace Scenario.Core.Entities
{
    public class Ad : BaseEntity
    {
        public string? Image { get; set; }
        public string? Title { get; set; }
        public string? Subtitle { get; set; }
    }
}
