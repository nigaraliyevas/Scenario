

using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Scenario.Application.Dtos.CommentDtos;
using Scenario.Application.Dtos.UserDtos;
using Scenario.Application.Enums;
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
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IEmailSenderService _emailSender;
        private readonly EmailConfigurationSettings _emailConfig;
        public AuthenticationService(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, IMapper mapper, IOptions<JwtSettings> jwtSettings, IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork, SignInManager<AppUser> signInManager, IEmailSenderService emailSender = null, EmailConfigurationSettings emailConfig = null)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _mapper = mapper;
            _jwtSettings = jwtSettings.Value;
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _emailConfig = emailConfig;
        }

        public async Task<string> Register(UserRegisterDto userRegisterDto)
        {
            var existUser = await _userManager.FindByNameAsync(userRegisterDto.Username);
            if (existUser != null) throw new CustomException("404", "Conflict");
            if (userRegisterDto.Password != userRegisterDto.RePassword) throw new CustomException("409", "Passwords don't match");

            string userImage = null;
            if (!string.IsNullOrEmpty(userRegisterDto.Image))
            {
                if (!userRegisterDto.Image.CheckContentType("image"))
                    throw new CustomException(400, "The file has to be img");

                if (userRegisterDto.Image.CheckSize(1024))
                    throw new CustomException(400, "The file is too large");

                userImage = await userRegisterDto.Image.SaveFile("userImages", _httpContextAccessor);
            }

            var newUser = _mapper.Map<AppUser>(userRegisterDto);
            newUser.UserImg = userImage;

            var result = await _userManager.CreateAsync(newUser, userRegisterDto.Password);
            if (!result.Succeeded)
                throw new CustomException(400, "Not Succeeded");

            await _userManager.AddToRoleAsync(newUser, UserRoles.Member.ToString());

            return newUser.Id;
        }
        //admin register
        public async Task<string> RegisterAdmin(UserRegisterDto userRegisterDto)
        {
            var existUser = await _userManager.FindByNameAsync(userRegisterDto.Username);
            if (existUser != null) throw new CustomException("404", "Conflict");
            if (userRegisterDto.Password != userRegisterDto.RePassword) throw new CustomException("409", "Passwords don't match");
            string userImage = null;
            if (!string.IsNullOrEmpty((userRegisterDto.Image)))
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

            await _userManager.AddToRoleAsync(newUser, UserRoles.Admin.ToString());

            return newUser.Id;
        }

        //login user and admin
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

        //creating role
        public async Task CreateRole()
        {
            foreach (var role in Enum.GetNames(typeof(UserRoles)))
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new IdentityRole { Name = role });
                }
            }
        }

        //
        public async Task<UserProfileDto> GetUserProfile(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) throw new CustomException(404, "User not found");

            var favoritePlots = await _unitOfWork.PlotAppUserRepository
                .GetAll(p => p.AppUserId == userId && p.IsFavorite, "Plot", "AppUser");
            var favoritePlotDtos = _mapper.Map<List<PlotDto>>(favoritePlots.Select(p => p.Plot));

            var userComments = await _unitOfWork.CommentRepository
                .GetAll(c => c.AppUserId == userId);
            var userCommentDtos = _mapper.Map<List<CommentDto>>(userComments);

            return new UserProfileDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Image = user.UserImg,
                FavoritePlots = favoritePlotDtos,
                Comments = userCommentDtos
            };
        }
        public async Task<string> Delete(string id)
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
        public async Task<bool> Update(string userId, UpdateUserDto updateUserDto)
        {
            if (updateUserDto == null) throw new CustomException(400, "Cannot be empty");
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) throw new CustomException(404, "User not found");
            user.UserName = updateUserDto.UserName ?? user.UserName;
            user.Email = updateUserDto.Email ?? user.Email;
            string userImage = null;
            if (!string.IsNullOrEmpty(updateUserDto.UserImg))
            {
                if (!updateUserDto.UserImg.CheckContentType("image"))
                    throw new CustomException(400, "The file has to be img");

                if (updateUserDto.UserImg.CheckSize(1024))
                    throw new CustomException(400, "The file is too large");

                userImage = await updateUserDto.UserImg.SaveFile("userImages", _httpContextAccessor);
            }
            user.UserImg = userImage;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                throw new CustomException(500, "Failed to update user");

            return true;
        }
        //public async Task<string> GeneratePasswordResetToken(ForgetPasswordDto forgetPasswordDto)
        //{
        //    var user = await _userManager.FindByEmailAsync(forgetPasswordDto.Email);
        //    if (user == null) throw new CustomException(404, "User Not found");

        //    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        //    return token;
        //}

        //public async Task ResetPassword(RecoverPasswordDto recoverPasswordDto)
        //{
        //    var user = await _userManager.FindByEmailAsync(recoverPasswordDto.Email);
        //    if (user == null) throw new CustomException(404, "User Not found");

        //    var result = await _userManager.ResetPasswordAsync(user, recoverPasswordDto.Token, recoverPasswordDto.NewPassword);

        //    if (!result.Succeeded)
        //    {
        //        throw new CustomException(400, "Password reset failed");
        //    }
        //}


        public async Task<string> ForgetPassword(ForgetPasswordDto forgetPasswordDto)
        {
            var user = await _userManager.FindByEmailAsync(forgetPasswordDto.Email);
            if (user == null)
            {
                throw new CustomException(404, "User not found");
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var resetLink = $"{forgetPasswordDto.ClientUri}/resetpassword?token={Uri.EscapeDataString(token)}&email={Uri.EscapeDataString(user.Email)}";

            var subject = "Şifrənin Bərpası";
            var message = $"Şifrənizi sıfırlamaq üçün, klik edin: <a href='{resetLink}'>Şifrəni sıfırla</a>";

            await _emailSender.SendEmailAsync(user.Email, subject, message);

            return token;
        }

        // Reset the password
        public async Task ResetPassword(RecoverPasswordDto recoverPasswordDto)
        {
            if (recoverPasswordDto == null) throw new CustomException(400, "Can't be null");
            var user = await _userManager.FindByEmailAsync(recoverPasswordDto.Email);
            if (user == null)
            {
                throw new CustomException(404, "User not found");
            }

            // Reset the password using the token
            var result = await _userManager.ResetPasswordAsync(user, recoverPasswordDto.Token, recoverPasswordDto.NewPassword);

            if (!result.Succeeded)
            {
                throw new CustomException(400, "Password reset failed");
            }
        }
        public async Task<string> ExternalLogin(ExternalLoginDto externalLoginDto)
        {
            try
            {
                var loginInfo = new UserLoginInfo(externalLoginDto.Provider, externalLoginDto.ProviderKey, externalLoginDto.Provider);
                var user = await _userManager.FindByLoginAsync(externalLoginDto.Provider, externalLoginDto.ProviderKey);

                if (user == null)
                {
                    user = new AppUser { UserName = externalLoginDto.Email, Email = externalLoginDto.Email };
                    var result = await _userManager.CreateAsync(user);
                    if (!result.Succeeded)
                        throw new CustomException(400, "User creation failed");

                    await _userManager.AddLoginAsync(user, loginInfo);
                }

                await _signInManager.SignInAsync(user, isPersistent: false);
                var privateKey = Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]);
                var credentials = new SigningCredentials(new SymmetricSecurityKey(privateKey), SecurityAlgorithms.HmacSha256);

                var ci = new ClaimsIdentity();
                ci.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));
                ci.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
                ci.AddClaim(new Claim(ClaimTypes.Email, user.Email));

                var tokenExpiration = DateTime.UtcNow.AddDays(7);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    SigningCredentials = credentials,
                    Expires = tokenExpiration,
                    Subject = ci,
                    Audience = _configuration["JwtSettings:Audience"],
                    Issuer = _configuration["JwtSettings:Issuer"],
                    NotBefore = DateTime.UtcNow
                };

                var handler = new JwtSecurityTokenHandler();
                var token = handler.CreateToken(tokenDescriptor);
                return handler.WriteToken(token);
            }
            catch (Exception ex)
            {
                throw new CustomException(500, $"{ex.Message}");
            }
        }

        public async Task<bool> ToggleFavoritePlot(PlotAppUserDto plotAppUserDto)
        {
            if (plotAppUserDto.AppUserId == null || plotAppUserDto.PlotId <= 0) throw new CustomException(400, "user id or plot id is not right");
            //var existingFavorite = await _unitOfWork.PlotAppUserRepository.GetEntity(f => f.AppUser.Id == plotAppUserDto.UserId && f.Plot.Id == plotAppUserDto.PlotId, "Plot", "AppUser", "PlotAppUser");
            var existingFavorite = await _unitOfWork.PlotAppUserRepository.GetEntity(
                f => f.AppUser.Id == plotAppUserDto.AppUserId && f.Plot.Id == plotAppUserDto.PlotId,
                "Plot", "AppUser"
            );

            if (existingFavorite != null)
            {
                existingFavorite.IsFavorite = !existingFavorite.IsFavorite;
                await _unitOfWork.PlotAppUserRepository.Update(existingFavorite);
                _unitOfWork.Commit();
            }
            else
            {
                // If no record exists, create a new one and mark as favorite

                var newFavorite = _mapper.Map<PlotAppUser>(plotAppUserDto);
                await _unitOfWork.PlotAppUserRepository.Create(newFavorite);
            }

            _unitOfWork.Commit();
            return existingFavorite?.IsFavorite ?? true;
        }

        public async Task<List<PlotDto>> GetUserFavoritePlots(string userId)
        {
            if (string.IsNullOrEmpty(userId)) throw new CustomException(400, "User ID is required");

            var favoriteRepo = _unitOfWork.PlotAppUserRepository;
            var plotRepo = _unitOfWork.PlotRepository;

            var favoritePlots = await favoriteRepo.GetAll(f => f.AppUserId == userId && f.IsFavorite);
            var plotIds = favoritePlots.Select(f => f.PlotId).ToList();

            if (!plotIds.Any()) return new List<PlotDto>();

            var plots = await plotRepo.GetAll(p => plotIds.Contains(p.Id));
            return _mapper.Map<List<PlotDto>>(plots);
        }

        public async Task<List<AppUser>> GetAllUsers()
        {
            return await _userManager.Users.ToListAsync();
        }

    }

}
