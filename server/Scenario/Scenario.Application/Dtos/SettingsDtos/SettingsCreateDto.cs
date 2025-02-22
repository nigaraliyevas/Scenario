using System.ComponentModel.DataAnnotations;

namespace Scenario.Application.Dtos.SettingsDtos
{
    public class SettingsCreateDto
    {
        [Required]
        public string Key { get; set; }

        [Required]
        public string Value { get; set; }
    }
}
