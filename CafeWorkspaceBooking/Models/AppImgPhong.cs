using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CafeWorkspaceBooking.Models
{
    public class AppImgPhong
    {
        [Key]
        public int IdImgPhong { get; set; }
        public string Path { get; set; }

        public int  IdPhong { get; set; }
        public AppPhong? AppPhong { get; set; }
    }
}
