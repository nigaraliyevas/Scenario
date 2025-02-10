using Scenario.Application.Dtos.SettingsDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scenario.Application.Service.Interfaces
{
    public interface ISettingsService
    {
        Task<SettingsDto> GetSettingsAsync();
        Task<int> Update(SettingsUpdateDto settingsUpdateDto);
        Task<int> Delete();
        Task<int> Create();
    }
}
