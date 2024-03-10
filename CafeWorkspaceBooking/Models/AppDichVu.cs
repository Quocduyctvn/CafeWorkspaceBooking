using CafeWorkspaceBooking.WebConfig.Contants;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace CafeWorkspaceBooking.Models
{
    public class AppDichVu
    {
        [Key]
        public int  IdDichVu { get; set; }
        public string TenDv { get; set; }               // Bạc xĩu - cafe đá - liton - trà Chanh 
        public double Gia { get; set; }
        public string ImageUrl {  get; set; }   
        public string ThanhPhan {  get; set; }
        public string Mota { get; set; }



        public ICollection<AppDatDV> appDatDVs { get; set; }   // 1 dicch vu  có nhìu DICH VU PHONG 
        public int IdDmDichVu { get; set; }
        public AppDmDichVu appDmDichVu { get; set; }
        public ICollection<AppImgDV> appImgDV { get; set; }   // 1 dicch vu  có nhìu Image DICH VU

    }
}
