/* using CafeWorkspaceBooking.WebConfig.Contants;
using System.ComponentModel.DataAnnotations;

namespace CafeWorkspaceBooking.Models
{
	public class AppTGDatPhong
	{
		[Key]
		public int IdTGDatPhong { get; set; }
		public DateTime TGDatOnline { get; set; }
		[Required(ErrorMessage = "Dữ liệu là bắt buộc")]
		public DateTime TGBatDau { get; set; }
		[Required(ErrorMessage = "Dữ liệu là bắt buộc")]
		public DateTime TGKetThuc { get; set; }
		public double TongThoiLuong { get; set; }
		public TrangThaiDP TTDatPhong { get; set; }    //Trạng thái đặt phòng - cài đặt giá trị Tĩnh : Bị hủy bởi chủ quán/Bị hủy bởi khách hàng/Đặt thành công/Chờ duyệt

		public int? IdDatPhong { get; set; }          // lưu trữ thông tin TG khách đặt phòng 
		public int IdPhong { get; set; }
		public AppPhong appPhong { get; set; }
		public AppDatPhong appDatPhong { get; set; }   // 1:1

	}
}
*/