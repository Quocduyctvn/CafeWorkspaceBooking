using System.ComponentModel.DataAnnotations;

namespace CafeWorkspaceBooking.Models
{
    public class AppDmDichVu
    {
        [Key]
        public int IdDmDichVu { get; set; }
        public string TenDmDv { get; set; }

        public ICollection<AppDichVu> appDichVus { get; set; } 


    }
}
