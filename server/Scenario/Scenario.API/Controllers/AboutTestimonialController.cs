using Microsoft.AspNetCore.Mvc;
using Scenario.Application.Dtos.AboutTestimonialDtos;
using Scenario.Application.Service.Interfaces;

namespace Scenario.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AboutTestimonialController : ControllerBase
    {
        private readonly IAboutTestimonialService _aboutTestimonialService;

        public AboutTestimonialController(IAboutTestimonialService aboutTestimonialService)
        {
            _aboutTestimonialService = aboutTestimonialService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromForm] AboutTestimonialCreateDto aboutTestimonialCreateDto)
        {
            return Ok(await _aboutTestimonialService.Create(aboutTestimonialCreateDto));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _aboutTestimonialService.GetById(id));
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromForm] AboutTestimonialUpdateDto aboutTestimonialUpdateDto, int id)
        {
            //return Ok(await _aboutTestimonialService.Update(aboutTestimonialUpdateDto, id));
            return Ok();
        }
    }
}
