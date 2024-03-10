using CafeWorkspaceBooking.WebConfig.Contants;
using System.ComponentModel.DataAnnotations;

namespace CafeWorkspaceBooking.Models
{
    public class AppDatPhong
    {
        [Key]
        public int IdDatPhong {  get; set; }
        public double TongTien { get; set; }
		[Required(ErrorMessage = "Dữ liệu là bắt buộc")]
		public int SLKhach {  get; set; }
		//======================================================================
		public DateTime TGDatOnline { get; set; }
		[Required(ErrorMessage = "Dữ liệu là bắt buộc")]
		public DateTime TGBatDau { get; set; }
		[Required(ErrorMessage = "Dữ liệu là bắt buộc")]
		public DateTime TGKetThuc { get; set; }
		public double TongThoiLuong { get; set; }
		public TrangThaiDP TTDatPhong { get; set; }    //Trạng thái đặt phòng - cài đặt giá trị Tĩnh : Bị hủy bởi chủ quán/Bị hủy bởi khách hàng/Đặt thành công/Chờ duyệt
        public DateTime? TGCheckIn { get; set; }
        public DateTime? TGCheckOut { get; set; }

        //======================================================================
        public int IdPhong { get; set; }
		public AppPhong appPhong { get; set; }
		//======================================================================

		public int  IdKhachHang { get; set; }
        // public int IdPhong { get; set; }             // không có quan hệ Phòng - datphong Do Sinh ra bảng TGDatphong 
        // public int? IdHuyDatPhong { get; set; }      1:1

        public AppKhachHang appKhachHang { get; set; }
        // public AppPhong appPhong  { get; set; }      // không có quan hệ Phòng - datphong Do Sinh ra bảng TGDatphong 
        public AppHuyDatPhong appHuyDatPhong { get; set; } //  1;1 
        public AppHoaDon appHoaDon { get; set; }            // 1:1 
															// public AppTGDatPhong appTGDatPhong { get; set;}

		public ICollection<AppThongBao> appThongBao { get; set; }
        public ICollection<AppDatDV> appDatDV { get; set; } // 1 đặt phòng có thể k có dịch vụ nào
    }
}
