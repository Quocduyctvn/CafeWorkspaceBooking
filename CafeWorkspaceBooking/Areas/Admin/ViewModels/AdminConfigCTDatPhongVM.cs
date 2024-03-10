using CafeWorkspaceBooking.Models;

namespace CafeWorkspaceBooking.Areas.Admin.ViewModels
{
	public class AdminConfigCTDatPhongVM
	{
		public DateTime TGBatDau { get; set; }
		public DateTime TGKetThuc { get; set; }
		public AppKhachHang AppKhachHang { get; set; }
		public ICollection<AppDatDV> appDatDVs { get; set; }

	}
}
