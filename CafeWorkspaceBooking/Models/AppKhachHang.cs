using CafeWorkspaceBooking.WebConfig.Contants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CafeWorkspaceBooking.Models
{
    public class AppKhachHang
    {
        [Key]
        public int IdKhachHang { get; set; }
        [StringLength(150)]
        [MinLength(3, ErrorMessage ="Họ Tên ít nhất 3 ký tự")]
        public string HoTen {  get; set; }
        public bool GioiTinh { get; set; }
        public DateTime NgaySinh { get; set; }
        [RegularExpression(@"^\d{12}$", ErrorMessage = "Số CCCD chỉ chứa đúng 12 chữ số")]  // /d chỉ chứa số - {12} là chỉ 12 chữ số 
        public string CCCD { get; set; }
        public string SDT { get; set; }
        public string? Email { get; set; }
        public string? ImgKH { get; set; }
        public bool TrangThaiTK { get; set; }  // Lưu trứ trạng thái tài khoản khách hàng - default là 'DangSuDung' 

		[Required(ErrorMessage = "Mật khẩu là bắt buộc")]
		[MaxLength(200)]
		[MinLength(4, ErrorMessage = "Mật khẩu chưa đủ mạnh!")]
        [DataType(DataType.Password)]
		public string Password { get; set; }
		[NotMapped]//ko tạo cột cfmPassword vào trong bảng database
		[MinLength(4, ErrorMessage = "Mật khẩu chưa đủ mạnh!")]
		[Compare("Password", ErrorMessage = "Mật khẩu không khớp")]
        [DataType(DataType.Password)]
        public string Cfm_Password { get; set; }
        public UserRole Role { get; set; }

		// public int IdRole  { get; set; }
        // public AppRole appRole { get; set; }   // 1 khách hàng có duy nhất 1 vai trò, VAI TRÒ KHÁCH 
        public ICollection<AppHoaDon>? appHoaDon { get; set; } // 1 khách hàng có n hóa đơn 
        public AppNhanVien? appNhanVien { get; set; } // 1 khách hàng có thể 0 OR trở thành 1 NhanVien 
        public ICollection<AppHuyDatPhong>? appHuyDatPhongs { get; set; }   // 1 khách hàng có thể hủy nhìu đăng kí đặt phòng 
        public ICollection<AppDatPhong>? appDatPhongs { get; set; }   // 1 khách hàng có nhiều đơn đặt phòng - vd: đặt cho mìh và đặt cho bn      

        // [Required]
        public ICollection<AppDanhGia>? appDanhGias { get; set; } // 1 khách hàng có nhìu đánh giá

		public ICollection<AppThongBao> appThongBao { get; set; }
        public ICollection<AppDatDV> appDatDVs { get; set; } // 1 khách hàng có nhìu đánh giá
    }
}