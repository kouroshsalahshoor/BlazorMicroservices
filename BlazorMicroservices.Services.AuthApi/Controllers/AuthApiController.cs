using BlazorMicroservices.Services.AuthApi.Models.Dtos;
using BlazorMicroservices.Services.AuthApi.Services.IServices;
using BlazorMicroservices.Services.AuthApi.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorMicroservices.Services.AuthApi.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthApiController : ControllerBase
    {
        private readonly IAuthService _authService;
        private ResponseDto _response;
        public AuthApiController(IAuthService authService)
        {
            _authService = authService;
            _response = new();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto dto)
        {
            _response.Errors.Clear();

            if (dto == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var registerResponse = await _authService.Register(dto);
            if (registerResponse.IsSuccessful == false)
            {
                _response.IsSuccessful = false;
                _response.Errors = registerResponse.Errors.ToList()!;
                _response.Message = registerResponse.Errors.FirstOrDefault()!;
                return BadRequest(_response);
            }

            bool roleResult = false;
            if (string.IsNullOrEmpty(dto.Role))
            {
                roleResult = await _authService.AssignRole(dto.UserName, SD.Role_Users);
            }
            else
            {
                roleResult = await _authService.AssignRole(dto.UserName, dto.Role);
            }

            if (roleResult == false)
            {
                _response.IsSuccessful = false;
                _response.Errors.Add("Error creating role at register");
                _response.Message = "Error creating role at register";
                return BadRequest(_response);
            }

            return Ok(_response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
        {
            if (dto == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var loginResponse = await _authService.Login(dto);
            if (loginResponse.IsSuccessful == false)
            {
                _response.IsSuccessful = false;
                _response.Message = "Invalid Authentication";
                return Unauthorized(_response);
            }
            else
            {
                if (loginResponse.UserDto == null)
                {
                    _response.IsSuccessful = false;
                    _response.Message = "Username or password is incorrect";
                    return BadRequest(_response);
                }
            }

            _response.Result = loginResponse;
            return Ok(_response);
        }

        [HttpPost("assignrole")]
        public async Task<IActionResult> AssignRole([FromBody] RegisterRequestDto dto)
        {
            var assignRoleSuccessful = await _authService.AssignRole(dto.UserName, dto.Role);
            if (!assignRoleSuccessful)
            {
                _response.IsSuccessful = false;
                _response.Message = "Error encountered";
                return BadRequest(_response);
            }
            return Ok(_response);

        }
    }
}
