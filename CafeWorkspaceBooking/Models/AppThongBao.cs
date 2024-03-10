namespace CafeWorkspaceBooking.Models
{
	public class AppThongBao
	{
		public int Id { get; set; }
		public string TenThongBao { get; set; }
		public string NDThongbao { get; set; }
		public DateTime CreateAt { get; set; }
		public bool Checked { get; set; }
		public bool CheckAll { get; set; }

		public int? IdPhong {  get; set; }					// lưu trữ thông tin phòng về thông báo đó				1:n
		public AppPhong? appPhong { get; set; }
		public int? IdDatPhong { get; set; }                    // Thông báo về lịch đặt phòng của khách hàng		1:n 
		public AppDatPhong appDatPhong { get; set; }
		public int? IdHuyDatPhong { get; set; }             // thông báo khách hàng hủy đặt phòng					1:1
		public AppHuyDatPhong appHuyDatPhong { get; set; }
		public int? IdKhachHang { get; set; }               // lưu trữ thông tin khách hàng về thông báo đó			1:n 
		public AppKhachHang appKhachHang { get; set; }
		public int? IdDanhGia { get; set; }                 // thông báo về đánh giá khách hàng						1:n
		public AppDanhGia appDanhGia { get;set; }

        public int? IdHuyDatDV { get; set; }                 // thông báo về đánh giá khách hàng						1:n
        public AppHuyDatDV appHuyDatDV { get; set; }
    }
}
/*
	các dạng thông báo:
			1. thông báo khách hàng đăng kí đặt phòng 
			2. thông bao skhachs hàng Hủy phòng 
			3. thông báo tới giờ nhận phòng trc 15' nếu trong TG đặt phòng khách đã tới checkIn thì xóa thông báo 
			4. thông báo 
 * 
 */