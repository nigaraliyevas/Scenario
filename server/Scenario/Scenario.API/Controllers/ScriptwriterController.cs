using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scenario.Application.Dtos.ScriptwriterDtos;
using Scenario.Application.Service.Interfaces;

namespace Scenario.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScriptwriterController : ControllerBase
    {
        private readonly IScriptwriterService _scriptwriterService;

        public ScriptwriterController(IScriptwriterService scriptwriterService)
        {
            _scriptwriterService = scriptwriterService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _scriptwriterService.GetById(id));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _scriptwriterService.GetAll());
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ScriptwriterCreateDto scriptwriterCreateDto)
        {
            return Ok(await _scriptwriterService.Create(scriptwriterCreateDto));
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ScriptwriterUpdateDto scriptwriterUpdateDto)
        {
            return Ok(await _scriptwriterService.Update(scriptwriterUpdateDto));
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _scriptwriterService.Delete(id));
        }
    }
}
