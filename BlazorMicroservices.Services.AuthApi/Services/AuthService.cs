using BlazorMicroservices.Services.AuthApi.Data;
using BlazorMicroservices.Services.AuthApi.Models;
using BlazorMicroservices.Services.AuthApi.Models.Dto;
using BlazorMicroservices.Services.AuthApi.Services.IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlazorMicroservices.Services.AuthApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthService(ApplicationDbContext db, IJwtTokenGenerator jwtTokenGenerator,
            UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _jwtTokenGenerator = jwtTokenGenerator;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<string> Register(RegisterRequestDto registerRequestDto)
        {
            ApplicationUser user = new()
            {
                UserName = registerRequestDto.UserName,
                Email = registerRequestDto.Email,
                NormalizedEmail = registerRequestDto.Email.ToUpper(),
                FirstName = registerRequestDto.FirstName,
                LastName = registerRequestDto.LastName,
                PhoneNumber = registerRequestDto.PhoneNumber
            };

            try
            {
                var result = await _userManager.CreateAsync(user, registerRequestDto.Password);
                if (result.Succeeded)
                {
                    var userToReturn = _db.ApplicationUsers.First(u => u.UserName == registerRequestDto.UserName);

                    UserDto userDto = new()
                    {
                        Id = userToReturn.Id,
                        UsertName = userToReturn.UserName,
                        Email = userToReturn.Email,
                        FirstName = userToReturn.FirstName,
                        LastName = userToReturn.LastName,
                        PhoneNumber = userToReturn.PhoneNumber
                    };

                    return "";

                }
                else
                {
                    return result.Errors.FirstOrDefault().Description;
                }

            }
            catch (Exception ex)
            {

            }
            return "Error Encountered";
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(u => u.UserName.ToLower() == loginRequestDto.UserName.ToLower());

            bool isValid = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);

            if (user == null || isValid == false)
            {
                return new LoginResponseDto() { User = null, Token = "" };
            }

            //if user was found , Generate JWT Token
            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtTokenGenerator.GenerateToken(user, roles);

            UserDto userDto = new()
            {
                Id = user.Id,
                UsertName = user.UserName,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
            };

            LoginResponseDto loginResponseDto = new LoginResponseDto()
            {
                User = userDto,
                Token = token
            };

            return loginResponseDto;
        }

        public async Task<bool> AssignRole(string userName, string roleName)
        {
            var user = await _db.ApplicationUsers.FirstOrDefaultAsync(u => u.UserName.ToLower() == userName.ToLower());
            if (user != null)
            {
                if (!await _roleManager.RoleExistsAsync(roleName))
                {
                    //create role if it does not exist
                    await _roleManager.CreateAsync(new IdentityRole(roleName));
                }
                await _userManager.AddToRoleAsync(user, roleName);
                return true;
            }
            return false;
        }
    }
}
