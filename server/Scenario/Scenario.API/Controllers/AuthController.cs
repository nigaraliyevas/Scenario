using Microsoft.AspNetCore.Mvc;
using Scenario.Application.Dtos.UserDtos;
using Scenario.Application.Service.Interfaces;

namespace MovieApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authService;

        public AuthController(IAuthenticationService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto userRegisterDto, int? planId, [FromQuery] bool isAdmin = false)
        {
            await _authService.Register(userRegisterDto, planId, isAdmin);
            return StatusCode(201);
        }

        [HttpPost("Role")]
        public async Task<IActionResult> CreateRole()
        {
            await _authService.CreateRole();
            return StatusCode(201);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            var token = await _authService.Login(userLoginDto);
            return Ok(new { token });
        }

        [HttpGet("Profile")]
        //[Authorize]
        public async Task<IActionResult> UserProfile(string userName)
        {
            var userProfile = await _authService.GetUserProfile(userName);
            return Ok(userProfile);
        }

        //[HttpPost("ForgetPassword")]
        //public async Task<IActionResult> ForgetPassword(ForgetPasswordDto forgetPasswordDto)
        //{
        //    var token = await _authService.GeneratePasswordResetToken(forgetPasswordDto);
        //    return Ok(new { message = "Password reset token has been sent to your email" });
        //}

        //[HttpPost("RecoverPassword")]
        //public async Task<IActionResult> RecoverPassword(RecoverPasswordDto recoverPasswordDto)
        //{
        //    await _authService.ResetPassword(recoverPasswordDto);
        //    return Ok(new { message = "Password has been reset successfully" });
        //}
        [HttpDelete("id")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            //return Ok(await _authService.RemoveUser(id));
            return Ok(new { id });
        }

    }
}

