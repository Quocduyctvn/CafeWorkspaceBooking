using CafeWorkspaceBooking.Models;
using CafeWorkspaceBooking.WebConfig.Contants;

namespace CafeWorkspaceBooking.Areas.Admin.ViewModels
{
	public class AdminDichVuVM
	{
		public int IdDichVu { get; set; }
		public string TenDv { get; set; }               // Bạc xĩu - cafe đá - liton - trà Chanh 
		public double Gia { get; set; }
		public  IFormFile ImageUrlFormFile { get; set; }
		public string ThanhPhan { get; set; }
		public string Mota { get; set; }
		public IFormFileCollection appImgDVFormFile { get; set; }   // 1 dicch vu  có nhìu Image DICH VU

		public int IdDmDichVu { get; set; }
		public string ImageUrl { get; set; }   // 1 dicch vu  có nhìu DICH VU PHONG
		public List<string>? appImgDV { get; set; }
	}
}