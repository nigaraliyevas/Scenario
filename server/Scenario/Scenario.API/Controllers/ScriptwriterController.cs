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

        //[Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> Create(ScriptwriterCreateDto scriptwriterCreateDto)
        {
            return Ok(await _scriptwriterService.Create(scriptwriterCreateDto));
        }

        //[Authorize(Roles = "admin")]
        [HttpPut]
        public async Task<IActionResult> Update(ScriptwriterUpdateDto scriptwriterUpdateDto)
        {
            return Ok(await _scriptwriterService.Update(scriptwriterUpdateDto));
        }

        //[Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _scriptwriterService.Delete(id));
        }
    }
}
