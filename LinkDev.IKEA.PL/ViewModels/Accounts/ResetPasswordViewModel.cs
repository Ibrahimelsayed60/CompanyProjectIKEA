using System.ComponentModel.DataAnnotations;

namespace LinkDev.IKEA.PL.ViewModels.Accounts
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "New Password is required")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Confirm New Password is required")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Password doesn't match")]
        public string ConfirmNewPassword { get; set; }
    }
}
