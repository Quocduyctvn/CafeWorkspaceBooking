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
			ClaimsIdentity identity = (ClaimsIdentity)User.Identity;  // lấy thông từ Claims xuống
			Claim userIdClaim = identity.FindFirst("UserId");
			int userId = int.Parse(userIdClaim?.Value);



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
            if (success > 0)
			{
				var TTHDP = _CafeDbContext.AppHuyDatPhongs									
									.Where(i => i.TGHuy == huyDP.TGHuy && i.IdDatPhong == huyDP.IdDatPhong)
									.FirstOrDefault();
				var Tbao = new AppThongBao();							// lưu bảng thông báo
				Tbao.TenThongBao = "Hủy đặt phòng";
				Tbao.NDThongbao = $"Khách hàng {huyDP.appKhachHang.HoTen} đã hủy đặt phòng vào lúc {huyDP.appDatPhong.TGBatDau}";
				Tbao.CreateAt = DateTime.Now;
				Tbao.Checked = false;
				Tbao.CheckAll = false;

				Tbao.IdDanhGia = null;
				Tbao.IdDatPhong = huyDP.IdDatPhong;
				Tbao.IdHuyDatPhong = TTHDP.IdHuyDatPhong;
				Tbao.IdKhachHang = userId;
				Tbao.IdPhong = huyDP.appDatPhong.IdPhong;
				

				_CafeDbContext.Add(Tbao);
				_CafeDbContext.SaveChanges();

                SetSuccessMesg("Hủy Đặt phòng thành công - hẹn gặp lại quý khách");
            }
			else
			{

                SetErrorMesg("Hủy Đặt phòng KHÔNg thành công");
            }
			return RedirectToAction("DetailOfBooking", "DatPhong");
		}
	}
}
