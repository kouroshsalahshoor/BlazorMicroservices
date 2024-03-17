using System.ComponentModel.DataAnnotations;

namespace BlazorMicroservices.Services.AuthApi.Models.Dtos
{
    public class LoginRequestDto
    {
        [Required(ErrorMessage = "{0} is required")]
        public string UserName { get; set; }


        [Required(ErrorMessage = "{0} is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
