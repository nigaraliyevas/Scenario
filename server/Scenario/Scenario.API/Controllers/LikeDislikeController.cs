using Microsoft.AspNetCore.Mvc;
using Scenario.Application.Dtos.LikeDislikeDtos;
using Scenario.Application.Service.Interfaces;

namespace Scenario.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikeDislikeController : ControllerBase
    {
        private readonly ILikeDislikeService _likeDislikeService;

        public LikeDislikeController(ILikeDislikeService likeDislikeService)
        {
            _likeDislikeService = likeDislikeService;
        }

        [HttpPost("like")]
        public async Task<IActionResult> CreateLike([FromBody] LikeDislikeCreateDto likeDislikeCreateDto)
        {
            return Ok(await _likeDislikeService.CreateLike(likeDislikeCreateDto));
        }

        // Create Dislike
        [HttpPost("dislike")]
        public async Task<IActionResult> CreateDislike([FromBody] LikeDislikeCreateDto likeDislikeCreateDto)
        {
            return Ok(await _likeDislikeService.CreateDislike(likeDislikeCreateDto));
        }
    }
}
