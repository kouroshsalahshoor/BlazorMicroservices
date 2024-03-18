using System.ComponentModel.DataAnnotations;

namespace BlazorMicroservices.Services.AuthApi.Models.Dtos
{
    public class RegisterRequestDto
    {
        [Required(ErrorMessage = "{0} is required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        [RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "Invalid {0}")]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "{0} is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and confirm password is not matched")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "{0} is required.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "{0} is required.")]
        public string LastName { get; set; }
        public string? Role { get; set; }
    }
}
