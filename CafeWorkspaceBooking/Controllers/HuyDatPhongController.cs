using AutoMapper;
using CafeWorkspaceBooking.Models;
using CafeWorkspaceBooking.WebConfig.Contants;
using Microsoft.AspNetCore.Mvc;
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
			ClaimsIdentity identity = (ClaimsIdentity)User.Identity;  // lấy thông từ Claims xuống
			Claim userIdClaim = identity.FindFirst("UserId");
			int userId = int.Parse(userIdClaim?.Value);



			// cập nhật trạng thái phòng tại thời điểm đó là huy
			var datphong = _CafeDbContext.appDatPhongs.Find(id);
			datphong.TTDatPhong = TrangThaiDP.KHACH_HUY;


			// thêm dư liệu vào bảng HuyDatPhong
			var huyDP = new AppHuyDatPhong
			{
				TGHuy = DateTime.Now,
				LiDoHuy = lido,
				IdDatPhong = id,
				IdKhachHang = userId
			};
			_CafeDbContext.AppHuyDatPhongs.Add(huyDP);


			_CafeDbContext.appDatPhongs.Update(datphong);
			_CafeDbContext.SaveChanges();
			SetSuccessMesg("Hủy Đặt phòng thành công - hẹn gặp lại quý khách");
			return RedirectToAction("DetailOfBooking", "DatPhong");
		}
	}
}
