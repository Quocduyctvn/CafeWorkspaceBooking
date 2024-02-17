using System.ComponentModel.DataAnnotations;

namespace CafeWorkspaceBooking.Models
{
    public class AppDVPhong   // bang đc sinh ra do quan hệ nhìu - nhìu <PHONG & DICH VU>
    {
        [Key]
        public int IdDVPhong { get; set; }


        public int IdPhong { get; set; }
        public int IdDichVu { get; set; }
        public AppPhong appPhongs{ get; set; }
        public AppDichVu appDichVus { get; set; }

    }
}
