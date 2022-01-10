using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RMSysProj.Areas.Admin.ViewModels
{
    public class UserEditViewModel
    {
        public Guid Id { get; set; }

        [DisplayName("New Password")]
        [Required(ErrorMessage ="Password is required")]
        public string Password { get; set; }

        [DisplayName("Repeat Password")]
        [Compare("Password", ErrorMessage = "Password mismatch")]
        public string PasswordConfirm { get; set; }
    }
}
