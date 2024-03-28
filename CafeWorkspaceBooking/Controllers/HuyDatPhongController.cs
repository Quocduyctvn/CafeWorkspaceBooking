using AutoMapper;
using CafeWorkspaceBooking.Models;
using CafeWorkspaceBooking.WebConfig.Contants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CafeWorkspaceBooking.Controllers
{
	public class HuyDatPhongController : ControllerBase
	{
		public HuyDatPhongController(CafeDbContext cafeDbContext, IMapper mapper) : base(cafeDbContext, mapper)
		{
		}

		public IActionResult HuyDatPhong(int id, string lido)
		{
			if(id== null)
			{
				SetErrorMesg("Lỗi trong quá trình xử lí- vui lòng kiểm tra lại");
				return View();
			}
			if (string.IsNullOrEmpty(lido))
			{
				SetErrorMesg("Vui lòng nhập lí do Hủy Phòng");
				return View();
			}


			// cập nhật trạng thái phòng tại thời điểm đó là huy
			var datphong = _CafeDbContext.appDatPhongs
						.Include(i => i.appKhachHang)
						.FirstOrDefault(i => i.IdDatPhong == id);
			datphong.TTDatPhong = TrangThaiDP.KHACH_HUY;


			// thêm dư liệu vào bảng HuyDatPhong
			var huyDP = new AppHuyDatPhong
			{
				TGHuy = DateTime.Now,
				LiDoHuy = lido,
				IdDatPhong = id,
				IdKhachHang = userId
			};
			


			_CafeDbContext.appDatPhongs.Update(datphong);               // update trạng thái đặt phòng 
			_CafeDbContext.AppHuyDatPhongs.Add(huyDP);                      // Thêm record Hủy đặt phòng
			int success = _CafeDbContext.SaveChanges();					// giá trị trả về của _CafeDbContext.SaveChanges();	 là sl record đc lưu 
            
			return RedirectToAction("DetailOfBooking", "DatPhong");
		}
	}
}
