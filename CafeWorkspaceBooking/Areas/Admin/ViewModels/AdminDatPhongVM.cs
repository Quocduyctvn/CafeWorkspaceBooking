using CafeWorkspaceBooking.WebConfig.Contants;

namespace CafeWorkspaceBooking.Areas.Admin.ViewModels
{
	public class AdminDatPhongVM
	{
		public int IdPhong { get; set; }
		public int IdKhachHang { get; set; }
		public int IdDatPhong { get; set; }
		public string TenKhachHang {  get; set; }
		public string TenPhong { get; set; }
		public DateTime TGBatDau { get; set; }
		public DateTime TGKetThuc { get; set; }
		public TrangThaiDP TrangThaiDP {  get; set; }
		public TrangThaiPhong TrangThaiPhong { get; set; }
	}
}
