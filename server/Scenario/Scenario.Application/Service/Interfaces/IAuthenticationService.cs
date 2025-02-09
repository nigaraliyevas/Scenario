using Scenario.Application.Dtos.UserDtos;

namespace Scenario.Application.Service.Interfaces
{
    public interface IAuthenticationService
    {
        Task Register(UserRegisterDto userRegisterDto, int? planId, bool isAdmin);
        Task<string> Login(UserLoginDto userLoginDto);
        Task CreateRole();
        //Task<UserGetDto> GetUserProfile(string username);
        Task<UserGetDto> GetUserProfile(string username);
        Task<string> GeneratePasswordResetToken(ForgetPasswordDto forgetPasswordDto);
        Task ResetPassword(RecoverPasswordDto recoverPasswordDto);
        //Task<string> RemoveUser(string id);
    }
}
