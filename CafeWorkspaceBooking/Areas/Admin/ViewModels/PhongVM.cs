using CafeWorkspaceBooking.Models;

namespace CafeWorkspaceBooking.Areas.Admin.ViewModels
{
	public class PhongVM
	{
		public int IdPhong { get; set; }
		public string TenPhong { get; set; } 
		public int SucChua { get; set; }   //  số ghế
		public double Gia { get; set; }
		public IFormFile? Img { get; set; }         // lưu trữ giá trị khi create 
		public string MoTa { get; set; }
		public IFormFileCollection? appImgPhongs { get; set; }  // 1 phòng có nhìu ảnh  // lưu trữ giá trị khi create 

		public string? Imgs { get; set; }  // luuư giá trị khi lấy từ csdl => lấy lên đer edit 
		public List<string>? ImgPhong { get; set; }   // luuư giá trị khi lấy từ csdl 
    }
}
