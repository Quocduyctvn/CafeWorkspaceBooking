using System.ComponentModel.DataAnnotations;

namespace CafeWorkspaceBooking.Models
{
    public class AppNhanVien  // thông tin nhân viên đc lấy lại từ thông tin khách lúc trc 
    {
        [Key]
        public int IdNhanVien { get; set; }
        public string ChucVu { get; set; }   //QuanLy - Pha Che 
        public DateTime NgayVaoLam { get; set; }
        public double Luong { get; set; }


        public int? IdKhachHang { get; set; }  //  trước khi lm NVien, pãi đăng ký thông qua Khachhang 
        public AppKhachHang appKhachHang { get; set; }
        public ICollection<AppHoaDon> appHoaDons { get; set; } // 1 Nvien co the lap 1|| nhìu HoaDon - QH lap hoaDon 

    }
}
