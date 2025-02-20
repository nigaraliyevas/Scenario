using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Scenario.Application.Service.Interfaces;

namespace Scenario.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserScenarioFavoriteController : ControllerBase
    {
        private readonly IUserScenarioFavoriteService _userScenarioFavoriteService;

        public UserScenarioFavoriteController(IUserScenarioFavoriteService userScenarioFavoriteService)
        {
            _userScenarioFavoriteService = userScenarioFavoriteService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddToFavorites(int scenarioId, string userId)
        {
            return Ok(await _userScenarioFavoriteService.AddToFavorites(scenarioId, userId));
        }

        [HttpDelete("remove")]
        public async Task<IActionResult> RemoveFromFavorites(int scenarioId, string userId)
        {
            return Ok(await _userScenarioFavoriteService.RemoveFromFavorites(scenarioId, userId));
        }

        [HttpGet("{userId}/liked-scenarios")]
        public async Task<IActionResult> GetLikedScenariosByUserId(string userId)
        {
            return Ok(await _userScenarioFavoriteService.GetLikedScenariosByUserId(userId));
        }
    }
}
