using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CafeWorkspaceBooking.Models
{
    public class AppDanhGia
    {
        [Key]
        public int IdDanhGia { get; set; }
        public string CapDo { get; set; } // ddánh giá *****
        public string NhanXet { get; set; } 
        public DateTime TGDanhGia { get; set; }



        public int? IdKhachHang { get; set; }
        public int IdPhong { get; set; }
        public int IdDichVu { get; set; }

        //[Required]
        //[ForeignKey("IdDanhGia")]
        public AppKhachHang appKhachHang { get; set; }
        public AppPhong appPhong { get; set; }
        public AppDichVu appDichVu { get; set; }
    }
}
