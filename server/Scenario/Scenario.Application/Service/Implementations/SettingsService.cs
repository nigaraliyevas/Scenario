using Scenario.Application.Dtos.SettingsDtos;
using Scenario.Application.Exceptions;
using Scenario.Application.Service.Interfaces;
using Scenario.DataAccess.Implementations.UnitOfWork;

namespace Scenario.Application.Service.Implementations
{
    public class SettingsService : ISettingsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SettingsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Dictionary<string, string>> GetAllSettingsAsync()
        {
            var settings = await _unitOfWork.SettingsRepository.GetAll();
            return settings.ToDictionary(s => s.Key, s => s.Value);
        }

        public async Task<string> GetSettingByKeyAsync(string key)
        {
            var setting = await _unitOfWork.SettingsRepository.GetEntity(s => s.Key == key);
            return setting?.Value ?? string.Empty;
        }
        public async Task CreateSettingAsync(SettingsCreateDto settingsCreateDto)
        {
            var existingSetting = await _unitOfWork.SettingsRepository.GetEntity(s => s.Key == settingsCreateDto.Key);
            if (existingSetting != null)
                throw new CustomException(400,"Setting with this key already exists.");

            var newSetting = new Core.Entities.Settings { Key = settingsCreateDto.Key, Value = settingsCreateDto.Value };
            await _unitOfWork.SettingsRepository.Create(newSetting);
             _unitOfWork.Commit();
        }

        public async Task UpdateSettingsAsync(Dictionary<string, string> settings)
        {
            foreach (var item in settings)
            {
                var existingSetting = await _unitOfWork.SettingsRepository.GetEntity(s => s.Key == item.Key);
                if (existingSetting != null)
                {
                    existingSetting.Value = item.Value;
                    await _unitOfWork.SettingsRepository.Update(existingSetting);
                }
                else
                {
                    var newSetting = new Core.Entities.Settings { Key = item.Key, Value = item.Value };
                    await _unitOfWork.SettingsRepository.Create(newSetting);
                }
            }
             _unitOfWork.Commit();
        }


    }
}
