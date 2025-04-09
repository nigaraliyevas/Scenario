using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scenario.Application.Dtos.UserDtos;
using Scenario.Application.Service.Interfaces;
using System.Security.Claims;

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

        //[HttpPost]
        //public async Task<IActionResult> Register([FromBody] UserRegisterDto userRegisterDto)
        //{
        //    await _authService.Register(userRegisterDto);
        //    return StatusCode(201);
        //}

        [HttpPost("Role")]
        public async Task<IActionResult> CreateRole()
        {
            await _authService.CreateRole();
            return StatusCode(201);
        }

        //[HttpPost("Login")]
        //public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        //{
        //    var token = await _authService.Login(userLoginDto);
        //    return Ok(new { token });
        //}

        //[HttpGet("Profile")]
        ////[Authorize]
        //[HttpGet("me")]
        //public async Task<IActionResult> GetUser()
        //{
        //    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    if (userId == null) return Unauthorized();

        //    var userProfile = await _authService.GetUserProfile(userId);
        //    return Ok(userProfile);
        //}

        //[HttpPost("forgetpassword")]
        //public async Task<IActionResult> ForgetPassword([FromBody] ForgetPasswordDto forgetPasswordDto)
        //{
        //    if (string.IsNullOrEmpty(forgetPasswordDto.Email))
        //    {
        //        return BadRequest("Email is required");
        //    }

        //    try
        //    {
        //        var token = await _authService.GeneratePasswordResetToken(forgetPasswordDto);

        //        return Ok("Password reset link has been sent to your email.");
        //    }
        //    catch (CustomException ex)
        //    {
        //        return StatusCode(ex.Code, ex.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, "An error occurred while processing your request.");
        //    }
        //}

        //[HttpPost("resetpassword")]
        //public async Task<IActionResult> ResetPassword([FromBody] RecoverPasswordDto recoverPasswordDto)
        //{
        //    if (string.IsNullOrEmpty(recoverPasswordDto.Token) || string.IsNullOrEmpty(recoverPasswordDto.NewPassword))
        //    {
        //        return BadRequest("Token and new password are required");
        //    }

        //    try
        //    {
        //        await _authService.ResetPassword(recoverPasswordDto);

        //        return Ok("Password has been successfully reset.");
        //    }
        //    catch (CustomException ex)
        //    {
        //        return StatusCode(ex.Code, ex.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, "An error occurred while processing your request.");
        //    }
        //}
        //[HttpDelete("id")]
        //public async Task<IActionResult> DeleteUser(string id)
        //{
        //    //return Ok(await _authService.RemoveUser(id));
        //    return Ok(new { id });
        //}

        ////favorite part
        //[HttpPost("toggle/{plotId}")]
        //public async Task<IActionResult> ToggleFavorite(int plotId)
        //{
        //    var userId = _userManager.GetUserId(User);
        //    if (userId == null) return Unauthorized();

        //    bool isFavorited = await _favoritePlotService.ToggleFavoritePlotAsync(userId, plotId);
        //    return Ok(new { isFavorite = isFavorited, message = isFavorited ? "Plot added to favorites" : "Plot removed from favorites" });
        //}
        //[HttpGet]
        //public async Task<IActionResult> GetUserFavorites()
        //{
        //    var userId = _userManager.GetUserId(User);
        //    if (userId == null) return Unauthorized();

        //    var favorites = await _favoritePlotService.GetUserFavoritePlotsAsync(userId);
        //    return Ok(favorites);
        //}

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto userRegisterDto)
        {
            return Ok(await _authService.Register(userRegisterDto));
        }

        [HttpPost("register/admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] UserRegisterDto userRegisterDto)
        {
            return Ok(await _authService.RegisterAdmin(userRegisterDto));

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
        {

            return Ok(await _authService.Login(userLoginDto));
        }

        [HttpGet("profile")]
        public async Task<IActionResult> GetUserProfile()
        {
            var userId = User.FindFirstValue("id");
            return Ok(await _authService.GetUserProfile(userId));
        }

        [HttpPut("{userId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateUser(string userId, [FromBody] UpdateUserDto updateUserDto)
        {

            return Ok(await _authService.Update(userId, updateUserDto));

        }

        [HttpDelete("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            return Ok(await _authService.Delete(id));

        }

        [HttpPost("forgetPassword")]
        public async Task<IActionResult> ForgetPassword([FromBody] ForgetPasswordDto forgetPasswordDto)
        {

            return Ok(await _authService.ForgetPassword(forgetPasswordDto));
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] RecoverPasswordDto recoverPasswordDto)
        {

            return Ok(_authService.ResetPassword(recoverPasswordDto));

        }

        [HttpPost("external-login")]
        public async Task<IActionResult> ExternalLogin([FromBody] ExternalLoginDto externalLoginDto)
        {
            return Ok(await _authService.ExternalLogin(externalLoginDto));
        }

        [HttpPost("toggle-favorite-plot")]
        [Authorize]
        public async Task<IActionResult> ToggleFavoritePlot([FromBody] PlotAppUserDto plotAppUserDto)
        {

            return Ok(await _authService.ToggleFavoritePlot(plotAppUserDto));
        }

        [HttpGet("user-favorite-plots/{userId}")]
        [Authorize]
        public async Task<IActionResult> GetUserFavoritePlots(string userId)
        {
            return Ok(await _authService.GetUserFavoritePlots(userId));

        }

        [HttpGet("all-users")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUsers()
        {
            return Ok(await _authService.GetAllUsers());

        }
    }
}

