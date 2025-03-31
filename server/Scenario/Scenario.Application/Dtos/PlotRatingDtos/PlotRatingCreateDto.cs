using System.ComponentModel.DataAnnotations;

namespace Scenario.Application.Dtos.PlotRatingDtos
{
    public class PlotRatingCreateDto
    {
        [Required]
        public int PlotId { get; set; } 

        [Required]
        public string UserId { get; set; } 

        [Range(1, 5)]
        public int Rating { get; set; } 
    }
}
