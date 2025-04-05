using Scenario.Application.Dtos.UserDtos;
using Scenario.Core.Entities;

namespace Scenario.Application.Service.Interfaces
{
    public interface IAuthenticationService
    {
        Task<string> Register(UserRegisterDto userRegisterDto);
        Task<string> RegisterAdmin(UserRegisterDto userRegisterDto);
        Task<string> Login(UserLoginDto userLoginDto);
        Task CreateRole();
        Task<UserProfileDto> GetUserProfile(string username);
        Task<string> Delete(string id);
        Task<bool> Update(string userId, UpdateUserDto updateUserDto);
        Task<string> ForgetPassword(ForgetPasswordDto forgetPasswordDto);
        Task ResetPassword(RecoverPasswordDto recoverPasswordDto);
        Task<string> ExternalLogin(ExternalLoginDto externalLoginDto);
        Task<bool> ToggleFavoritePlot(PlotAppUserDto plotAppUserDto);
        Task<List<PlotDto>> GetUserFavoritePlots(string userId);
        Task<List<AppUser>> GetAllUsers();


    }
}
