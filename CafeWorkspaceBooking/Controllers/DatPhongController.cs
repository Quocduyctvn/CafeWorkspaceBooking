using AutoMapper;
using CafeWorkspaceBooking.Areas.Admin.ViewModels;
using CafeWorkspaceBooking.Models;
using CafeWorkspaceBooking.ViewModels;
using CafeWorkspaceBooking.WebConfig.Contants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CafeWorkspaceBooking.Controllers
{
	public class DatPhongController : ControllerBase
	{
		public DatPhongController(CafeDbContext cafeDbContext, IMapper mapper) : base(cafeDbContext, mapper)
		{
		}

		[HttpGet]
		public IActionResult BookingRoom(int? id)
		{

			var user = HttpContext.User;
			if (!user.Identity.IsAuthenticated) // check đã đăng nhập hay ch
			{
				 SetErrorMesg("Vui lòng Đăng nhập trước khi tiến hành Đặt phòng");
				
				return RedirectToAction("Login", "Account");
			}

			ClaimsIdentity identity = (ClaimsIdentity)User.Identity;  // lấy thông từ Claims xuống
			Claim userIdClaim = identity.FindFirst("UserId");
			int userId = int.Parse(userIdClaim?.Value);
			var _kh = _CafeDbContext.AppKhachHangs.Find(userId);


			// lấy thông tin phòng
			var phong = _CafeDbContext.AppPhongs
				.FirstOrDefault(i => i.IdPhong == id);

			if (phong == null)
			{
				SetErrorMesg("Không tìm thấy Phòng");
				return RedirectToAction("Index", "Home");
			}
			ViewBag.Phong = new
			{
				Name = phong.TenPhong,
				Gia = phong.Gia,
				SucChua = phong.SucChua,
				Img = phong.Img,
				Mota = phong.MoTa
			};
			ViewBag._kh = new
			{
				HoTen = _kh.HoTen ?? "",
				Email = _kh.Email ?? "",
				SDT = _kh.SDT ?? "",
				CCCD = _kh.CCCD ?? ""
			};

			return View();
		}
		[HttpPost]
		public IActionResult BookingRoom(int? id, DatPhongVM datphongVM)
		{
			if (!ModelState.IsValid)
			{
				ModelState.AddModelError("", "Dữ liệu không hợp lệ - vui lòng nhập chính xác!");
				return View(datphongVM);
			}

			ClaimsIdentity identity = (ClaimsIdentity)User.Identity;  // lấy thông từ Claims xuống
			Claim userIdClaim = identity.FindFirst("UserId");
			int userId = int.Parse(userIdClaim?.Value);
			var _kh = _CafeDbContext.AppKhachHangs.Find(userId);



			var phong = _CafeDbContext.AppPhongs.Find(id);
			if(datphongVM.SLKhach > phong.SucChua)
			{
				ModelState.AddModelError("", $"Số khách tối đa là: {phong.SucChua}");

				return View(datphongVM);
			}
			var tonggio = (float)(datphongVM.TGKetThuc - datphongVM.TGBatDau).TotalHours;
			var dp = new AppDatPhong
			{
				TongTien = tonggio * phong.Gia,
				SLKhach = datphongVM.SLKhach,
				TGBatDau = datphongVM.TGBatDau,
				TGKetThuc = datphongVM.TGKetThuc,
				TGDatOnline = DateTime.Now,
				TGCheckIn = null,
				TGCheckOut = null,
				TongThoiLuong = tonggio,
				TTDatPhong = TrangThaiDP.CHODUYET,
				IdPhong = phong.IdPhong,
				IdKhachHang = _kh.IdKhachHang,
			};

			_CafeDbContext.Add(dp);
            int success = _CafeDbContext.SaveChanges();					// giá trị trả về của _CafeDbContext.SaveChanges();	 là sl record đc lưu 
            if (success > 0)
            {
                var DP = _CafeDbContext.appDatPhongs
                                    .Where(i => i.TGBatDau == dp.TGBatDau && i.TGKetThuc == dp.TGKetThuc)
                                    .FirstOrDefault();
                var Tbao = new AppThongBao();                           // lưu bảng thông báo
                Tbao.TenThongBao = "Đặt phòng";
                Tbao.NDThongbao = $"Khách hàng {_kh.HoTen} đã đặt phòng vào lúc {dp.TGBatDau}";
                Tbao.CreateAt = DateTime.Now;
                Tbao.Checked = false;
                Tbao.CheckAll = false;

                Tbao.IdDanhGia = null;
                Tbao.IdDatPhong = DP.IdDatPhong;
				Tbao.IdHuyDatPhong = null;
                Tbao.IdKhachHang = userId;
				Tbao.IdPhong = dp.IdPhong;


                _CafeDbContext.Add(Tbao);
                _CafeDbContext.SaveChanges();

                SetSuccessMesg("Đặt phòng thành công - Cảm ơn quý khách");
            }
            else
            {

                SetErrorMesg("Đặt phòng KHÔNg thành công");
            }
            return RedirectToAction("Index", "Home");
		}

		public IActionResult DetailOfBooking(DateTime? datetime, string keyword, string phong)
		{
			if (datetime == DateTime.MinValue) // khi hạy lần đàu chưa lọc => datetime  luôn lấy giá trị {01/01/0001 00:00:00}
			{
				datetime = null;
			}
			var user = HttpContext.User;
			var _kh = _CafeDbContext.AppKhachHangs.Find(userId);

			if (_kh == null)
			{
				SetErrorMesg("Tài khoản không tồn tại");
				return RedirectToAction("Index", "Home");
			}

			var query = _CafeDbContext.appDatPhongs
					.Include(p => p.appPhong)
					.Include(i => i.appKhachHang)
					.Include(i => i.appHoaDon)
					.Where(i => i.IdKhachHang == _kh.IdKhachHang);

			if (datetime.HasValue)
			{
				query = query.Where(i => i.TGBatDau.Day == datetime.Value.Day);
			}

			if (!string.IsNullOrEmpty(phong))
			{
				query = query.Where(i => i.appPhong.IdPhong == Convert.ToInt32(phong));
			}

			if (!string.IsNullOrEmpty(keyword))
			{
				query = query.Where(i => i.appPhong.TenPhong.Contains(keyword));
			}
			if(!string.IsNullOrEmpty(keyword) || !string.IsNullOrEmpty(phong) || datetime.HasValue)
			{
				ViewBag.searched = true;
			}
			var ListDP = query.ToList();

			var datphong = new List<AdminDatPhongVM>();
			foreach (var item in ListDP)
			{
				var DP = new AdminDatPhongVM { };
				DP.IdKhachHang = item.appKhachHang.IdKhachHang;
				DP.IdDatPhong = item.IdDatPhong;
				DP.TenKhachHang = item.appKhachHang.HoTen;
				DP.TenPhong = item.appPhong.TenPhong;
				DP.TrangThaiDP = item.TTDatPhong;
				///DP.TrangThaiPhong = ;
				var check_Trung = _CafeDbContext.appDatPhongs
					.Include(p => p.appPhong)
					.Include(i => i.appKhachHang)
					.Any(otherCustomer =>
						otherCustomer.appPhong.IdPhong == item.appPhong.IdPhong &&
						((item.TGBatDau > otherCustomer.TGBatDau && item.TGBatDau < otherCustomer.TGKetThuc) ||
						(item.TGKetThuc > otherCustomer.TGBatDau && item.TGKetThuc < otherCustomer.TGKetThuc)) &&
						item.TTDatPhong == otherCustomer.TTDatPhong
					);
				if (check_Trung)
				{
					DP.TrangThaiPhong = TrangThaiPhong.TRUNG;
				}
				datphong.Add(DP);
			}

			var p = _CafeDbContext.AppPhongs
				.Select(i => new { Id = i.IdPhong, Name = i.TenPhong })
				.ToList();

			ViewBag.Phong = new SelectList(p, "Id", "Name");

			return View(datphong);
		}
	}
}
