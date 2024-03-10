using CafeWorkspaceBooking.Models;

namespace CafeWorkspaceBooking.Areas.Admin.ViewModels
{
	public class AdminConfigDV_VM
	{
		public ICollection<AppDichVu> DisplayDichVuVM { get; set; }
		public AdminDichVuVM CreateDichVuVM { get; set; }
	}
}
