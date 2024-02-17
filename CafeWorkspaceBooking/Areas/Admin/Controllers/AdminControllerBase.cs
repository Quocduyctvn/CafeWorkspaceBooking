using AutoMapper;
using CafeWorkspaceBooking.Models;
using CafeWorkspaceBooking.WebConfig.Contants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Data;

namespace CafeWorkspaceBooking.Areas.Admin.Controllers
{
    [Authorize(Roles = "ADMIN, NHANVIEN")]
    [Area("Admin")]
    public class AdminControllerBase : Controller
    {
        protected readonly CafeDbContext _CafeDbContext;  // khai báo protected quy định không cho phép truy cập ngoài lớp - trừ kế thừa 
        protected readonly IMapper _mapper;
        public AdminControllerBase(CafeDbContext cafeDbContext, IMapper mapper)
        {
            _CafeDbContext = cafeDbContext;
            _mapper = mapper;
        }

		public override void OnActionExecuting(ActionExecutingContext context)  // ham OnActionExecuting cho chay dau tien trong controller
		{
			var datphong = _CafeDbContext.appDatPhongs.ToList();
			foreach (var dp in datphong)
			{
				if (dp.TGKetThuc < DateTime.Now && dp.TTDatPhong == TrangThaiDP.CHODUYET)  // qúa hạn dặt phòng 
				{
					dp.TTDatPhong = TrangThaiDP.QUA_HAN;
				}
				_CafeDbContext.Update(dp);
				_CafeDbContext.SaveChanges();
			}
		}
		protected void SetSuccessMesg(string msg)
        {
            TempData["_SuccessMesg"] = msg;
        }
        protected void SetErrorMesg(string msg)
        {
            TempData["_ErrorMesg"] = msg;
        }
    }
}
