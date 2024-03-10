using System.ComponentModel.DataAnnotations;

namespace CafeWorkspaceBooking.Models
{
    public class AppImgDV
    {
        [Key]
        public int IdImgDV { get; set; }
        public string Path { get; set; }

        public int IdDichVu { get; set; }
        public AppDichVu? appDichVu { get; set; }
    }
}
