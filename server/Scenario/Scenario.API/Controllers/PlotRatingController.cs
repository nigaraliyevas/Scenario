using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scenario.Application.Dtos.PlotRatingDtos;
using Scenario.Application.Service.Interfaces;
using System.Security.Claims;

namespace Scenario.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlotRatingController : ControllerBase
    {
        private readonly IPlotRatingService _plotRatingService;

        public PlotRatingController(IPlotRatingService plotRatingService)
        {
            _plotRatingService = plotRatingService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _plotRatingService.GetById(id));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _plotRatingService.GetAll());
        }
        [Authorize]
        [HttpPost("rate")]
        public async Task<IActionResult> RatePlot([FromBody] PlotRatingCreateDto ratingDto)
        {
            var userId = User.FindFirstValue("id");
            if (string.IsNullOrEmpty(userId)) return Unauthorized();
            return Ok(await _plotRatingService.RatePlot(ratingDto, userId));
        }


        [Authorize] 
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _plotRatingService.Delete(id));
        }
    }
}
