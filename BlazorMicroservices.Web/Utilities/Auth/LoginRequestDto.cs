using System.ComponentModel.DataAnnotations;

namespace BlazorMicroservices.Web.Utilities.Auth
{
    public class LoginRequestDto
    {
        [Required(ErrorMessage = "UserName is required")]
        public string UserName { get; set; }


        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
