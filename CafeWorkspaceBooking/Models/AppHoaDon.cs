using System.ComponentModel.DataAnnotations;

namespace CafeWorkspaceBooking.Models
{
    public class AppHoaDon
    {
        [Key]
        public int IdHoaDon { get; set; }
        public double TongThanhToan { get; set; }
        public DateTime TGLapHD { get; set; }


        public int IdDatPhong {  get; set; }
        public int IdNhanVien { get; set; }
        public int IdKhachHang  { get; set; }
        public AppDatPhong appDatPhong { get; set; }
        public AppNhanVien appNhanVien { get; set; }
        public AppKhachHang? appkhachHang  { get; set; }


    }
}
