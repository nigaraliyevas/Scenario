using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Scenario.Application.Dtos.SettingsDtos;
using Scenario.Application.Service.Interfaces;

namespace Scenario.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private readonly ISettingsService _settingsService;

        public SettingsController(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSettings()
        {
            return Ok(await _settingsService.GetAllSettingsAsync());
        }

        [HttpGet("{key}")]
        public async Task<IActionResult> GetSettingByKey(string key)
        {
            return Ok(await _settingsService.GetSettingByKeyAsync(key));
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateSettings([FromBody] Dictionary<string, string> settings)
        {
            await _settingsService.UpdateSettingsAsync(settings);
            return Ok();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateSetting([FromBody] SettingsCreateDto settingsCreateDto)
        {
            await _settingsService.CreateSettingAsync(settingsCreateDto);
            return Ok();
        }
    }
}
