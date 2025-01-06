using System.ComponentModel.DataAnnotations;

namespace LinkDev.IKEA.PL.ViewModels.Accounts
{
	public class ForgetPasswordViewModel
	{
		[Required(ErrorMessage = "Email is required")]
		[EmailAddress(ErrorMessage = "Invalid Email")]
		public string Email { get; set; }
	}
}
