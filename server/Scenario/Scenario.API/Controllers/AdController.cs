using Microsoft.AspNetCore.Mvc;
using Scenario.Application.Dtos.AdDtos;
using Scenario.Application.Service.Interfaces;

namespace Scenario.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdController : ControllerBase
    {
        private readonly IAdService _adService;

        public AdController(IAdService adService)
        {
            _adService = adService;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AdCreateDto adCreateDto)
        {
            return Ok(await _adService.Create(adCreateDto));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] AdUpdateDto adUpdateDto)
        {
            return Ok(await _adService.Update(adUpdateDto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _adService.Delete(id));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _adService.GetAll());
        }

        [HttpGet("take/{takeCount}")]
        public async Task<IActionResult> GetAll(int takeCount)
        {
            return Ok(await _adService.GetAll(takeCount));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _adService.GetById(id));
        }
    }
}
