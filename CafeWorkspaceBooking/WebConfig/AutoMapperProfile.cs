using AutoMapper;
using CafeWorkspaceBooking.Areas.Admin.ViewModels;
using CafeWorkspaceBooking.Models;
using CafeWorkspaceBooking.ViewModels;

namespace CafeWorkspaceBooking.WebConfig
{
    public class AutoMapperProfile : Profile //profile là một class chứa thông tin ánh xạ cho object
    {
        public AutoMapperProfile() {
            CreateMap<RegisterVM, AppKhachHang>();
            CreateMap<PhongVM, AppPhong>().ReverseMap(); 


        }
    }
}
