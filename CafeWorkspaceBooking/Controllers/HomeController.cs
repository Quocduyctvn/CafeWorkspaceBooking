using AutoMapper;
using CafeWorkspaceBooking.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;

namespace CafeWorkspaceBooking.Controllers
{
	public class HomeController : ControllerBase
	{

		public HomeController(CafeDbContext cafeDbContext, IMapper mapper) : base(cafeDbContext, mapper)
		{
		}

		public IActionResult Index()
		{
			ViewBag.Path = "HomeIndex";   //  truyền viewbag để hiển thị slide 

			var phong = _CafeDbContext.AppPhongs.ToList();
			if(phong == null)
			{
				SetErrorMesg("Hiện tại phòng đang sửa chữa - Xin cảm ơn");
				return View();
			}

			return View(phong);
		}
		public IActionResult Detail(int? id)
		{
			if(id == null)
			{
				SetErrorMesg("Vui Lòng kiểm tra lại thông tin Phòng");
				return View(nameof(Index));
			}
			var phong = _CafeDbContext.AppPhongs
							.Include(p => p.appImgPhongs)
							.Where(p => p.IdPhong == id)
							.SingleOrDefault();
			if (phong == null)
			{
				SetErrorMesg("Vui Lòng kiểm tra lại thông tin Phòng");
				return View(nameof(Index));
			}
			return View(phong);
		}
		
		public IActionResult Contact()
		{
			return View();
		}
			//[Route("/Err/{statusCode}")]
			//public IActionResult Error(int statusCode) {
			//	//if ()
			//	//{

			//	//}
			//	return View();
			//}
		}
}
