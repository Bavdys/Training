using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RMSysProj.ViewModels
{
    public class LoginModel
    {
        [DisplayName("Login")]
        [Required(ErrorMessage = "Login is required")]
        public string Email { get; set; }

        [DisplayName("Password")]
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
