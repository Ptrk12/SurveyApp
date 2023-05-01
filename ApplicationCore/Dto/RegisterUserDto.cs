
using System.ComponentModel.DataAnnotations;

namespace SurveyApp.Dto
{
    public class RegisterUserDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Not a valid email")]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "Passwords are not matched")]
        public string ConfirmPassword { get; set; }
    }
}
