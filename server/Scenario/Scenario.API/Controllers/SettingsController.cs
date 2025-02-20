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
        public async Task<IActionResult> Get()
        {
            return Ok(await _settingsService.GetSettingsAsync());
        }
                
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] SettingsUpdateDto settingsUpdateDto)
        {            
            return Ok(await _settingsService.Update(settingsUpdateDto));
        }
      
        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            return Ok(await _settingsService.Delete());
        }

        [HttpPost]
        public async Task<IActionResult> Create()
        {
            return Ok(await _settingsService.Create());
        }
    }
}
