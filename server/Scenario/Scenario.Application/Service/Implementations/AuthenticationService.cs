

using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Scenario.Application.Dtos.UserDtos;
using Scenario.Application.Exceptions;
using Scenario.Application.Extensions.Extension;
using Scenario.Application.Helpers.Helper;
using Scenario.Application.Service.Interfaces;
using Scenario.Application.Settings;
using Scenario.Core.Entities;
using Scenario.DataAccess.Implementations.UnitOfWork;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Scenario.Application.Service.Implementations
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly JwtSettings _jwtSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;

        public AuthenticationService(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, IMapper mapper, IOptions<JwtSettings> jwtSettings, IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _mapper = mapper;
            _jwtSettings = jwtSettings.Value;
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
        }

        //public async Task Register(UserRegisterDto userRegisterDto)
        //{
        //    var existUser = await _userManager.FindByNameAsync(userRegisterDto.Username);
        //    if (existUser != null) throw new CustomException("404", "Conflict");

        //    string userImage = null;
        //    if (userRegisterDto.Image != null)
        //    {
        //        if (!userRegisterDto.Image.CheckContentType("image"))
        //        {
        //            throw new CustomException(400, "The file has to be img");
        //        }

        //        if (userRegisterDto.Image.CheckSize(1024))
        //        {
        //            throw new CustomException(400, "The file is too large");
        //        }
        //        userImage = await userRegisterDto.Image.SaveFile("userImages", _httpContextAccessor);
        //    }


        //    var newUser = _mapper.Map<AppUser>(userRegisterDto);
        //    newUser.UserImg = userImage;

        //    var result = await _userManager.CreateAsync(newUser, userRegisterDto.Password);
        //    if (!result.Succeeded)
        //        throw new CustomException(400, "Not Succeeded");

        //    await _userManager.AddToRoleAsync(newUser, "member");
        //}
        public async Task Register(UserRegisterDto userRegisterDto, int? planId, bool isAdmin)
        {
            var existUser = await _userManager.FindByNameAsync(userRegisterDto.Username);
            if (existUser != null) throw new CustomException("404", "Conflict");
            if (userRegisterDto.Password != userRegisterDto.RePassword) throw new CustomException("409", "Passwords don't match");
            string userImage = null;
            if (userRegisterDto.Image != null)
            {
                if (!userRegisterDto.Image.CheckContentType("image"))
                {
                    throw new CustomException(400, "The file has to be img");
                }
                if (userRegisterDto.Image.CheckSize(1024))
                {
                    throw new CustomException(400, "The file is too large");
                }
                userImage = await userRegisterDto.Image.SaveFile("userImages", _httpContextAccessor);
            }

            var newUser = _mapper.Map<AppUser>(userRegisterDto);
            newUser.UserImg = userImage;

            var result = await _userManager.CreateAsync(newUser, userRegisterDto.Password);
            if (!result.Succeeded)
                throw new CustomException(400, "Not Succeeded");

            if (isAdmin)
                await _userManager.AddToRoleAsync(newUser, "admin");
            else
                await _userManager.AddToRoleAsync(newUser, "member");
        }


        public async Task<string> Login(UserLoginDto userLoginDto)
        {
            var user = await _userManager.FindByNameAsync(userLoginDto.Username);
            if (user == null) throw new CustomException(404, "User not found");

            var result = await _userManager.CheckPasswordAsync(user, userLoginDto.Password);
            if (!result) throw new CustomException(400, "Invalid credentials");

            var handler = new JwtSecurityTokenHandler();
            var privateKey = Encoding.UTF8.GetBytes(_jwtSettings.SecretKey);

            var credentials = new SigningCredentials(new SymmetricSecurityKey(privateKey), SecurityAlgorithms.HmacSha256);

            var ci = new ClaimsIdentity();
            ci.AddClaim(new Claim("id", user.Id));
            ci.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
            ci.AddClaim(new Claim(ClaimTypes.Email, user.Email));
            //ci.AddClaim(new Claim("userImage", user.UserImg ?? string.Empty));


            var roles = await _userManager.GetRolesAsync(user);
            ci.AddClaims(roles.Select(r => new Claim(ClaimTypes.Role, r)).ToList());

            var tokenExpiration = userLoginDto.RememberMe ? DateTime.UtcNow.AddDays(7) : DateTime.UtcNow.AddHours(1);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                SigningCredentials = credentials,
                Expires = tokenExpiration,
                Subject = ci,
                Audience = _jwtSettings.Audience,
                Issuer = _jwtSettings.Issuer,
                NotBefore = DateTime.UtcNow
            };

            var token = handler.CreateToken(tokenDescriptor);

            return handler.WriteToken(token);

        }

        public async Task CreateRole()
        {
            if (!await _roleManager.RoleExistsAsync("member"))
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = "member" });
            }

            if (!await _roleManager.RoleExistsAsync("admin"))
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = "admin" });
            }
        }

        public async Task<UserGetDto> GetUserProfile(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null) throw new CustomException(404, "User Not found");

            return _mapper.Map<UserGetDto>(user);
        }
        public async Task<string> RemoveUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) throw new CustomException(404, "User Not found");

            if (!string.IsNullOrEmpty(user.UserImg))
            {
                Helper.DeleteImageFromFolder(user.UserImg, "userImages");
            }
            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                throw new CustomException(400, "Failed delete user");
            }
            return user.Id;
        }

        public async Task<string> GeneratePasswordResetToken(ForgetPasswordDto forgetPasswordDto)
        {
            var user = await _userManager.FindByEmailAsync(forgetPasswordDto.Email);
            if (user == null) throw new CustomException(404, "User Not found");

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            return token;
        }

        public async Task ResetPassword(RecoverPasswordDto recoverPasswordDto)
        {
            var user = await _userManager.FindByEmailAsync(recoverPasswordDto.Email);
            if (user == null) throw new CustomException(404, "User Not found");

            var result = await _userManager.ResetPasswordAsync(user, recoverPasswordDto.Token, recoverPasswordDto.NewPassword);

            if (!result.Succeeded)
            {
                throw new CustomException(400, "Password reset failed");
            }
        }
    }
}
