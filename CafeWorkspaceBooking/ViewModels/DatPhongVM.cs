using System.ComponentModel.DataAnnotations;

namespace CafeWorkspaceBooking.ViewModels
{
	public class DatPhongVM
	{
		[Required(ErrorMessage ="Dữ liệu là bắt buộc")]
		public DateTime TGBatDau { get; set; }
		[Required(ErrorMessage = "Dữ liệu là bắt buộc")]
		public DateTime TGKetThuc { get; set; }
		[Required(ErrorMessage = "Dữ liệu là bắt buộc")]
		public int SLKhach { get; set; }
	}
}
