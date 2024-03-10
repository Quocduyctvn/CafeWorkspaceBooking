using System.ComponentModel.DataAnnotations;

namespace CafeWorkspaceBooking.Models
{
    public class AppHuyDatDV
    {
        [Key]
        public int IdHuyDatDV { get; set; }
        public DateTime TGHuy { get; set; }
        public string LiDoHuy { get; set; }


        public int IdDatDV { get; set; }   // lấy thông dặt hàng 
        public AppDatDV appDatDV { get; set; }

        public AppThongBao? appThongBao { get; set; }

    }
}
