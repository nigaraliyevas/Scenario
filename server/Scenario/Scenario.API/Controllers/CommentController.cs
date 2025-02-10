using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CommentCreateDto commentCreateDto)
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

        [HttpPut]
        public async Task<IActionResult> Update(CommentUpdateDto commentUpdateDto, int id)
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
