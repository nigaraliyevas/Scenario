using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scenario.Application.Dtos.CommentDtos;
using Scenario.Application.Service.Interfaces;

namespace MovieApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CommentCreateDto commentCreateDto)
        {
            return Ok(await _commentService.Create(commentCreateDto));
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _commentService.GetAll());
        }

        [HttpGet("id/")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _commentService.GetById(id));
        }

        [HttpGet("Chapter/{chapterId}")]
        public async Task<IActionResult> GetAllByChapterId(int chapterId)
        {
            return Ok(await _commentService.GetAllByChapterId(chapterId));
        }
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] CommentUpdateDto commentUpdateDto, int id)
        {
            return Ok(await _commentService.Update(commentUpdateDto, id));
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _commentService.Delete(id));
        }
    }
}
