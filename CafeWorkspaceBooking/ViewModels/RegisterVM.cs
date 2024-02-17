using System.ComponentModel.DataAnnotations;

namespace CafeWorkspaceBooking.ViewModels
{
	public class RegisterVM
	{
		[Required(ErrorMessage = "HoTen là bắt buộc")]
		public string HoTen { get; set; }
		[Required(ErrorMessage = "Mật khẩu là bắt buộc")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		[Required(ErrorMessage = "GioiTinh là bắt buộc")]
		public bool GioiTinh { get; set; }
		[Required(ErrorMessage = "NgaySinh là bắt buộc")]
		public DateTime NgaySinh { get; set; }
		[Required(ErrorMessage = "CCCD là bắt buộc")]
		public string CCCD { get; set; }

		[Required(ErrorMessage = "SDT là bắt buộc")]
		public string SDT { get; set; }
		[Required(ErrorMessage = "Email là bắt buộc")]
		public string? Email { get; set; }
	}
}
