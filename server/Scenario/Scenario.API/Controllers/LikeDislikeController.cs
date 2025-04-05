using Microsoft.AspNetCore.Mvc;
using Scenario.Application.Dtos.LikeDislikeDtos;
using Scenario.Application.Service.Interfaces;


namespace Scenario.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikeDislikeController : ControllerBase
    {
        private readonly ILikeDislikeService _likeDislike;

        public LikeDislikeController(ILikeDislikeService likeDislikeService)
        {
            _likeDislike = likeDislikeService;
        }

        [HttpPost("like")]
        public async Task<IActionResult> CreateLike(LikeDislikeCreateDto likeDislikeCreateDto)
        {
            return Ok(await _likeDislike.CreateLike(likeDislikeCreateDto));
        }

        // Create Dislike
        [HttpPost("dislike")]
        public async Task<IActionResult> CreateDislike(LikeDislikeCreateDto likeDislikeCreateDto)
        {
            return Ok(await _likeDislike.CreateDislike(likeDislikeCreateDto));
        }
    }
}
