namespace BlazorMicroservices.Services.AuthApi.Models.Dtos
{
    public class RegisterResponseDto
    {
        public bool IsSuccessful { get; set; }
        public List<string> Errors { get; set; } = new();
    }
}
