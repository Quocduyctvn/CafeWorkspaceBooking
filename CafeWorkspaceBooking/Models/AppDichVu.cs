using System.ComponentModel.DataAnnotations;

namespace CafeWorkspaceBooking.Models
{
    public class AppDichVu
    {
        [Key]
        public int  IdDichVu { get; set; }
        public string TenDv { get; set; }   // Nước(phí) - màn Hình(free) - Quạt (free)
        public string LoaiDV { get; set; }  // lưu trữ giá trị "free" hoặc "lấy phí"


        public ICollection<AppDVPhong> appDVPhongs { get; set; }   // 1 dicch vu  có nhìu DICH VU PHONG 
        public ICollection<AppDanhGia> appDanhGias { get; set; }   // 1 dicch vu  có nhìu DICH VU PHONG 

    }
}
