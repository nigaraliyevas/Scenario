using Microsoft.AspNetCore.Mvc;
using Scenario.Application.Dtos.ChapterDtos;
using Scenario.Application.Service.Interfaces;

namespace Scenario.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChapterController : ControllerBase
    {
        private readonly IChapterService _chapterService;

        public ChapterController(IChapterService chapterService)
        {
            _chapterService = chapterService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _chapterService.GetById(id));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _chapterService.GetAll());
        }

        //[Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> Create(ChapterCreateDto chapterCreateDto)
        {
            return Ok(await _chapterService.Create(chapterCreateDto));
        }

        //[Authorize(Roles = "admin")]
        [HttpPut]
        public async Task<IActionResult> Update(ChapterUpdateDto chapterUpdateDto)
        {
            return Ok(await _chapterService.Update(chapterUpdateDto));
        }

        //[Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _chapterService.Delete(id));
        }
    }
}
