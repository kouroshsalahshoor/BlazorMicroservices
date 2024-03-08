using BlazorMicroservices.Services.AuthApi.Models.Dto;
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
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var errorMessage = await _authService.Register(registerRequestDto);
            if (!string.IsNullOrEmpty(errorMessage))
            {
                _response.IsSuccessful = false;
                _response.Message = errorMessage;
                return BadRequest(_response);
            }
            return Ok(_response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
        {
            var loginResponse = await _authService.Login(model);
            if (loginResponse.User == null)
            {
                _response.IsSuccessful = false;
                _response.Message = "Username or password is incorrect";
                return BadRequest(_response);
            }
            _response.Result = loginResponse;
            return Ok(_response);

        }

        [HttpPost("assignrole")]
        public async Task<IActionResult> AssignRole([FromBody] RegisterRequestDto registerRequestDto)
        {
            var assignRoleSuccessful = await _authService.AssignRole(registerRequestDto.UserName, registerRequestDto.Role);
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
