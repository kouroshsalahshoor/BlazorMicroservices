using BlazorMicroservices.Services.AuthApi.Data;
using BlazorMicroservices.Services.AuthApi.Models;
using BlazorMicroservices.Services.AuthApi.Models.Dtos;
using BlazorMicroservices.Services.AuthApi.Services.IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorMicroservices.Services.AuthApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthService(ApplicationDbContext db, IJwtTokenGenerator jwtTokenGenerator, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _db = db;
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<RegisterResponseDto> Register(RegisterRequestDto dto)
        {
            ApplicationUser user = new()
            {
                UserName = dto.UserName,
                Email = dto.Email,
                NormalizedEmail = dto.Email.ToUpper(),
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                PhoneNumber = dto.PhoneNumber
            };

            try
            {
                var result = await _userManager.CreateAsync(user, dto.Password);
                if (result.Succeeded)
                {
                    var userToReturn = _db.ApplicationUsers.First(u => u.UserName == dto.UserName);

                    if (userToReturn != null)
                    {
                        UserDto userDto = new()
                        {
                            Id = userToReturn.Id,
                            UserName = userToReturn.UserName!,
                            Email = userToReturn.Email!,
                            FirstName = userToReturn.FirstName,
                            LastName = userToReturn.LastName,
                            PhoneNumber = userToReturn.PhoneNumber!
                        };

                        return new RegisterResponseDto
                        {
                            IsSuccessful = true,
                        };
                    }
                }
                else
                {
                    return new RegisterResponseDto
                    {
                        IsSuccessful = false,
                        Errors = result.Errors.Select(x => x.Description).ToList()
                    };
                }

            }
            catch (Exception ex)
            {

            }
            return new RegisterResponseDto
            {
                IsSuccessful = false,
                Errors = new List<string> { "Error at register" }
            };
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto dto)
        {

            var result = await _signInManager.PasswordSignInAsync(dto.UserName, dto.Password, isPersistent: true, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                return new LoginResponseDto() { IsSuccessful = false, UserDto = null, Token = "" };
            }

            var user = await _userManager.FindByNameAsync(dto.UserName);
            if (user == null)
            {
                return new LoginResponseDto() { IsSuccessful = false, UserDto = null, Token = "" };
            }

            //if user was found , Generate JWT Token
            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtTokenGenerator.GenerateToken(user, roles);

            UserDto userDto = new()
            {
                Id = user.Id,
                UserName = user.UserName!,
                Email = user.Email!,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber!,
            };

            return new LoginResponseDto()
            {
                IsSuccessful = true,
                UserDto = userDto,
                Token = token
            };
        }

        //public async Task<LoginResponseDto> Login(LoginRequestDto dto)
        //{
        //    var user = _db.ApplicationUsers.FirstOrDefault(u => u.UserName.ToLower() == dto.UserName.ToLower());

        //    bool isValid = await _userManager.CheckPasswordAsync(user, dto.Password);

        //    if (user == null || isValid == false)
        //    {
        //        return new LoginResponseDto() { UserDto = null, Token = "" };
        //    }

        //    //if user was found , Generate JWT Token
        //    var roles = await _userManager.GetRolesAsync(user);
        //    var token = _jwtTokenGenerator.GenerateToken(user, roles);

        //    UserDto userDto = new()
        //    {
        //        Id = user.Id,
        //        UserName = user.UserName,
        //        Email = user.Email,
        //        FirstName = user.FirstName,
        //        LastName = user.LastName,
        //        PhoneNumber = user.PhoneNumber,
        //    };

        //    LoginResponseDto loginResponseDto = new LoginResponseDto()
        //    {
        //        UserDto = userDto,
        //        Token = token
        //    };

        //    return loginResponseDto;
        //}

        public async Task<bool> AssignRole(string userName, string roleName)
        {
            var user = await _db.ApplicationUsers.FirstOrDefaultAsync(u => u.UserName!.ToLower() == userName.ToLower());
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
