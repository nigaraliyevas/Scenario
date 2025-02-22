using System.ComponentModel.DataAnnotations;

namespace Scenario.Application.Dtos.SettingsDtos
{
    public class SettingsUpdateDto
    {
        [Required]
        public Dictionary<string, string> Settings { get; set; }
    }
}
