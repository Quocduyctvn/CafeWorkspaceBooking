using CafeWorkspaceBooking.WebConfig.Contants;
using System.ComponentModel.DataAnnotations;

namespace CafeWorkspaceBooking.Models
{
    public class AppDatDV  // bang đc sinh ra do quan hệ nhìu - nhìu <PHONG & DICH VU>
    {
        [Key]
        public int IdDatDV { get; set; }

        public double TongTien { get; set; }
        public int SoLuong { get; set; }

		public SizeWater? Size { get; set; }           // có thể rỗng do cơm 
													   //======================================================================
		public DateTime? TGDatOnline { get; set; }      // có thể null do uốn  nước tại quán 
        public DateTime TGNhan { get; set; }
        public TrangThaiDatDV TT_DatDV { get; set; }

        public int IdDichVu { get; set; }
        public AppDichVu appDichVus { get; set;}

        public int IdKhachHang { get; set;}
        public AppKhachHang appKhachHang { get; set;}

        public int IdDatPhong { get; set;}
        public AppDatPhong? appDatPhong { get; set;}    // 1 đơn đặt Dịch vụ có thể có trong 1 đặt phòng
        public AppHuyDatDV appHuyDatDV { get; set; }
        public AppHoaDon? appHoaDon { get; set; }

    }
}
