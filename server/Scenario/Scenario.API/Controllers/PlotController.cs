using Microsoft.AspNetCore.Mvc;
using Scenario.Application.Dtos.PlotDtos;
using Scenario.Application.Service.Interfaces;

namespace Scenario.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlotController : ControllerBase
    {
        private readonly IPlotService _plotService;

        public PlotController(IPlotService plotService)
        {
            _plotService = plotService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _plotService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _plotService.GetById(id));
        }

        [HttpGet("getAllByCategoryName")]
        public async Task<IActionResult> GetAllByCategoryName(string categoryName, int itemPerPage = 8, int page = 1)
        {
            return Ok(await _plotService.GetAllByCategoryName(categoryName, itemPerPage, page));
        }

        [HttpGet("getAllByNameOrScriptwriter")]
        public async Task<IActionResult> GetAllByNameOrScriptwriter(string search, int itemPerPage = 14, int page = 1)
        {
            return Ok(await _plotService.GetAllByNameOrScriptwriter(search, itemPerPage, page));
        }

        [HttpGet("getByName")]
        public IActionResult GetByName(string name)
        {
            return Ok(_plotService.GetByName(name));
        }

        [HttpPost]
        public async Task<IActionResult> Create(PlotCreateDto plotCreateDto)
        {
            return Ok(await _plotService.Create(plotCreateDto));
        }

        [HttpPut]
        public async Task<IActionResult> Update(PlotUpdateDto plotUpdateDto)
        {
            return Ok(await _plotService.Update(plotUpdateDto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _plotService.Delete(id));
        }
    }
}
