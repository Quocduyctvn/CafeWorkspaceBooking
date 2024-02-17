using System.ComponentModel.DataAnnotations;

namespace CafeWorkspaceBooking.Models
{
    public class AppHuyDatPhong
    {
        [Key]
        public int IdHuyDatPhong { get; set; }
        public DateTime  TGHuy { get; set; }
        public string LiDoHuy { get; set; }

		public int IdKhachHang { get; set; }   // lấy thông khách hàng 
		public AppKhachHang appKhachHang { get; set; }
		public int IdDatPhong { get; set; }   // lấy thông dặt hàng 
        public AppDatPhong appDatPhong { get; set; }
    }
}
