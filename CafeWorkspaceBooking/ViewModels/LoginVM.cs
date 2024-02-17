using System.ComponentModel.DataAnnotations;

namespace CafeWorkspaceBooking.ViewModels
{
	public class LoginVM
	{

		[Required(ErrorMessage = "SDT là bắt buộc")]
		public string SDT { get; set; }
		[Required(ErrorMessage = "Mật khẩu là bắt buộc")]
		public string Password { get; set; }
	}
}
