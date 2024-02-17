using System.ComponentModel.DataAnnotations;

namespace CafeWorkspaceBooking.Models
{
    public class AppPhong
    {
        public AppPhong()
        {
            appImgPhongs = new HashSet<AppImgPhong>();
        }
        [Key]
        public int IdPhong { get; set; }
        public string TenPhong  { get; set; } // tên phòng ghép bởi tên loại+ idPhong vd: V1
        public int SucChua { get; set; }   //  số ghế 
        public double  Gia { get; set; }
        public string Img { get; set; }
        public string MoTa { get; set; }

		public ICollection<AppImgPhong> appImgPhongs { get; set; }  // 1 phòng có nhìu ảnh

        public ICollection<AppDVPhong> appDVPhongs { get; set; }   // 1 phòng có nhìu DICH VU PHONG 

		public ICollection<AppDatPhong> appDatPhongs { get; set; }   // 1 phòng có nhìu DatPhong

		public ICollection<AppDanhGia> appDanhGias { get; set; }   // 1 phòng có nhìu DanhGia 


    }
}
