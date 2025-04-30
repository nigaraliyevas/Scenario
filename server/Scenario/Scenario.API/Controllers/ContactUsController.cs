using Microsoft.AspNetCore.Mvc;
using Scenario.Application.Dtos.ContactUsDtos;
using Scenario.Application.Service.Interfaces;

namespace Scenario.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactUsController : ControllerBase
    {
        private readonly IContactUsService _contactService;

        public ContactUsController(IContactUsService contactService)
        {
            _contactService = contactService;
        }

        [HttpPost("submit")]
        public async Task<IActionResult> SubmitContact([FromBody] ContactUsCreateDto contactUsCreateDto)
        {
            return Ok(await _contactService.SubmitRequest(contactUsCreateDto));
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _contactService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _contactService.GetById(id));
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ContactUsUpdateDto contactUsUpdateDto)
        {
            return Ok(await _contactService.Update(id, contactUsUpdateDto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _contactService.Delete(id));
        }
    }
}
