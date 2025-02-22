using Scenario.Application.Dtos.SettingsDtos;

namespace Scenario.Application.Service.Interfaces
{
    public interface ISettingsService
    {
        Task<Dictionary<string, string>> GetAllSettingsAsync();
        Task<string> GetSettingByKeyAsync(string key);
        Task UpdateSettingsAsync(Dictionary<string, string> settings);
        Task CreateSettingAsync(SettingsCreateDto settingsCreateDto);
    }
}
