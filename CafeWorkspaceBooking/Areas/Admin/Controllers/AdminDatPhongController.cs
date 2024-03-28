using AutoMapper;
using CafeWorkspaceBooking.Areas.Admin.ViewModels;
using CafeWorkspaceBooking.Models;
using CafeWorkspaceBooking.ViewModels;
using CafeWorkspaceBooking.WebConfig.Contants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CafeWorkspaceBooking.Areas.Admin.Controllers
{
	public class AdminDatPhongController : AdminControllerBase
	{
		public AdminDatPhongController(CafeDbContext cafeDbContext, IMapper mapper) : base(cafeDbContext, mapper)
		{
		}
		public override void OnActionExecuting(ActionExecutingContext context)  // ham OnActionExecuting cho chay dau tien trong controller
		{
			var tb = _CafeDbContext.AppThongBaos
							.Where(i => i.CheckAll == false)
							.ToList();
			ViewData["CheckAll"] = tb.Count;   // Số lượng thông báo moiứ nhất 
			var Tb = _CafeDbContext.AppThongBaos
					.OrderByDescending(i => i.CreateAt)
					.Take(15)
					.ToList();
			ViewBag.ThongBaoList = Tb;




			var datphong = _CafeDbContext.appDatPhongs.ToList();
			foreach (var dp in datphong)
			{
				TimeSpan Check15 = dp.TGBatDau - DateTime.Now;

				if (Check15.TotalMinutes == 15 && dp.TTDatPhong == TrangThaiDP.CHODUYET)            // thông báo còn 15' nx hết  duyệt 
				{
					var DP = _CafeDbContext.appDatPhongs
									.Include(i => i.appKhachHang)
									.Include(i => i.appPhong)
									.Where(i => i.TGBatDau == dp.TGBatDau && i.TGKetThuc == dp.TGKetThuc)
									.FirstOrDefault();
					var Tbao = new AppThongBao();                           // lưu bảng thông báo
					Tbao.TenThongBao = "Duyệt đặt phòng";
					Tbao.NDThongbao = $"Còn 15 phút nữa hết hạn duyệt Đặt Phòng cho khách {DP.appKhachHang.HoTen} tại phòng {DP.appPhong.TenPhong}";
					Tbao.CreateAt = DateTime.Now;
					Tbao.Checked = false;
					Tbao.CheckAll = false;

					Tbao.IdDanhGia = null;
					Tbao.IdDatPhong = DP.IdDatPhong;
					Tbao.IdHuyDatPhong = null;
					Tbao.IdKhachHang = DP.appKhachHang.IdKhachHang;
					Tbao.IdPhong = DP.appPhong.IdPhong;


					_CafeDbContext.Add(Tbao);
					_CafeDbContext.SaveChanges();
				}
				if (dp.TGBatDau < DateTime.Now && dp.TTDatPhong == TrangThaiDP.CHODUYET)  // qúa hạn duyet phòng ( hạn duyệt trước h ddatwj phòng 15') 
				{
					var DP = _CafeDbContext.appDatPhongs
									.Include(i => i.appKhachHang)
									.Include(i => i.appPhong)
									.Where(i => i.TGBatDau == dp.TGBatDau && i.TGKetThuc == dp.TGKetThuc)
									.FirstOrDefault();
					var Tbao = new AppThongBao();                           // lưu bảng thông báo
					Tbao.TenThongBao = "Hết hạn Duyệt đặt phòng";
					Tbao.NDThongbao = $"Đơn đặt phòng lúc {DP.TGBatDau} của khách {DP.appKhachHang.HoTen} đã bị Hủy - lí do quá hạn duyệt";
					Tbao.CreateAt = DateTime.Now;
					Tbao.Checked = false;
					Tbao.CheckAll = false;

					Tbao.IdDanhGia = null;
					Tbao.IdDatPhong = DP.IdDatPhong;
					Tbao.IdHuyDatPhong = null;
					Tbao.IdKhachHang = DP.appKhachHang.IdKhachHang;
					Tbao.IdPhong = DP.appPhong.IdPhong;


					_CafeDbContext.Update(Tbao);
					_CafeDbContext.SaveChanges();
					dp.TTDatPhong = TrangThaiDP.QUA_HAN;
				}


				if (Check15.TotalMinutes == 15 && dp.TTDatPhong == TrangThaiDP.DADUYET)  // thông báo chờ check in trước 15'
				{
					var DP = _CafeDbContext.appDatPhongs
									.Include(i => i.appKhachHang)
									.Include(i => i.appPhong)
									.Where(i => i.TGBatDau == dp.TGBatDau && i.TGKetThuc == dp.TGKetThuc)
									.FirstOrDefault();
					var Tbao = new AppThongBao();                           // lưu bảng thông báo
					Tbao.TenThongBao = "Chuẩn bị nhận Phòng";
					Tbao.NDThongbao = $"Còn 15 phút nữa khách {DP.appKhachHang.HoTen} Chuẩn bị nhận phòng {DP.appPhong.TenPhong}";
					Tbao.CreateAt = DateTime.Now;
					Tbao.Checked = false;
					Tbao.CheckAll = false;

					Tbao.IdDanhGia = null;
					Tbao.IdDatPhong = DP.IdDatPhong;
					Tbao.IdHuyDatPhong = null;
					Tbao.IdKhachHang = DP.appKhachHang.IdKhachHang;
					Tbao.IdPhong = DP.appPhong.IdPhong;


					_CafeDbContext.Add(Tbao);
					_CafeDbContext.SaveChanges();
				}
				//khách đã checkIN trong TG đătj phòng thì xóa thông báo "sắp nhận phòng kia" 
				if ((dp.TGBatDau < DateTime.Now && dp.TGKetThuc > DateTime.Now) && dp.TTDatPhong == TrangThaiDP.CHECKIN)
				{
					var check = _CafeDbContext.AppThongBaos
									 .Any(i => (dp != null && i.IdDatPhong == dp.IdDatPhong) &&
											   (dp.appKhachHang != null && i.IdKhachHang == dp.appKhachHang.IdKhachHang));
					AppThongBao TB = null;
					if (check)
					{
						TB = _CafeDbContext.AppThongBaos
									   .Include(i => i.appKhachHang)
									   .Include(i => i.appPhong)
									   .Where(i => i.IdDatPhong == dp.IdDatPhong && i.IdKhachHang == dp.appKhachHang.IdKhachHang)
									   .FirstOrDefault();
					}
					if (TB != null)
					{
						_CafeDbContext.Remove(TB);
						_CafeDbContext.SaveChanges();
					}
				}


				_CafeDbContext.Update(dp);
				_CafeDbContext.SaveChanges();

			}

		}
		public IActionResult Index(DateTime? datetime, string keyword, string phong)
		{
			if (datetime == DateTime.MinValue) // khi hạy lần đàu chưa lọc => datetime  luôn lấy giá trị {01/01/0001 00:00:00}
			{
				datetime = null;
			}
			var query = _CafeDbContext.appDatPhongs
							.Include(p => p.appPhong)
							.Include(i => i.appKhachHang)
							.Where(i => i.IdDatPhong != 0);  // thêm câu lệnh Whể vô tri vì có sử dụng query ở sau 
			if (datetime.HasValue)
			{
				query = query.Where(i => i.TGBatDau.Day == datetime.Value.Day);
				//query = query.Where(i => i.TGBatDau.Day == datetime.Value.Day);
			}
			if (!string.IsNullOrEmpty(phong))
			{
				query = query.Where(i => i.appPhong.IdPhong == Convert.ToInt32(phong));
			}

			if (!string.IsNullOrEmpty(keyword))
			{
				query = query.Where(i => i.appPhong.TenPhong.Contains(keyword) ||
									 i.appKhachHang.HoTen.Contains(keyword));
			}
			if (!string.IsNullOrEmpty(keyword) || !string.IsNullOrEmpty(phong) || datetime.HasValue)
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
				DP.TGBatDau = item.TGBatDau;
				DP.TGKetThuc = item.TGKetThuc;
				DP.TrangThaiDP = item.TTDatPhong;
				///DP.TrangThaiPhong = ;
				var check_Trung = _CafeDbContext.appDatPhongs
					.Include(p => p.appPhong)
					.Include(i => i.appKhachHang)
					.Where(i => i.IdDatPhong != item.IdDatPhong)
					.Any(otherCustomer =>
						((item.TGBatDau.TimeOfDay > otherCustomer.TGBatDau.TimeOfDay && item.TGBatDau.TimeOfDay < otherCustomer.TGKetThuc.TimeOfDay) ||
						(item.TGKetThuc.TimeOfDay > otherCustomer.TGBatDau.TimeOfDay && item.TGKetThuc.TimeOfDay < otherCustomer.TGKetThuc.TimeOfDay) ||
						(item.TGBatDau.TimeOfDay == otherCustomer.TGBatDau.TimeOfDay && item.TGKetThuc.TimeOfDay == otherCustomer.TGKetThuc.TimeOfDay))
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



		public IActionResult HuyDatPhong(int id, string lido, int? Idadmin) // id phong lấy từ form - Idadmin lấy từ hàm OnActionExecuting
		{
			if (id == null)
			{
				SetErrorMesg("Vui lòng kiểm tra lại hệ thống");
				return View(lido);
			}
			int userId;
			if (Idadmin != null) // nếu Idadmin có giá trị thì đc gọi từ action OnActionExecuting
			{
				userId = Idadmin.Value;
			}
			else
			{
				ClaimsIdentity identity = (ClaimsIdentity)User.Identity;  // lấy thông từ Claims xuống
				Claim userIdClaim = identity.FindFirst("UserId");
				userId = int.Parse(userIdClaim?.Value);
			}
			// ClaimsIdentity identity = (ClaimsIdentity)User.Identity;  // lấy thông từ Claims xuống
			//Claim userIdClaim = identity.FindFirst("UserId");
			//int userId = int.Parse(userIdClaim?.Value);

			//var userIdClaim = HttpContext.User.FindFirst("UserId");
			//int userId = int.Parse(userIdClaim?.Value);


			// cập nhật trạng thái phòng tại thời điểm đó là huy
			var datphong = _CafeDbContext.appDatPhongs.Find(id);
			datphong.TTDatPhong = TrangThaiDP.ADMIN_HUY;


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
			SetSuccessMesg("Hủy Đặt phòng thành công");
			return RedirectToAction("SendEmail", "AdminHome");
		}
		public IActionResult Duyet(int id)
		{
			var datphong = _CafeDbContext.appDatPhongs.Find(id);
			datphong.TTDatPhong = TrangThaiDP.DADUYET;


			_CafeDbContext.appDatPhongs.Update(datphong);
			_CafeDbContext.SaveChanges();
			SetSuccessMesg("Đặt phòng thành công");
			return RedirectToAction("Index", "AdminDatPhong");
		}
		public IActionResult CheckIn(int id)
		{
			var ChiTietDP = _CafeDbContext.appDatPhongs.Find(id);
			var checkTG = ChiTietDP.TGBatDau > DateTime.Now;  //nhận sớm => đặt giờ lại (nhận trễ lấy giờ cũ=))  ) 
			if (checkTG)
			{
				ChiTietDP.TGBatDau = DateTime.Now;
			}
			ChiTietDP.TGCheckIn = DateTime.Now;
			ChiTietDP.TTDatPhong = TrangThaiDP.CHECKIN;

			_CafeDbContext.Update(ChiTietDP);
			_CafeDbContext.SaveChanges();
			return RedirectToAction("Index", "AdminDatPhong");
		}

		//[HttpGet]
		//public IActionResult CheckOut(int id)
		//{
		//	return View(nameof(Index), "Admin");
		//}
		//[HttpPost]
		public IActionResult CheckOut(int id)
		{
			ClaimsIdentity identity = (ClaimsIdentity)User.Identity;  // lấy thông từ Claims xuống
			Claim userIdClaim = identity.FindFirst("UserId");
			int userId = int.Parse(userIdClaim?.Value);


			var ChiTietDP = _CafeDbContext.appDatPhongs
					.Include(i => i.appPhong)
					.Include(i => i.appKhachHang)
					.FirstOrDefault(dp => dp.IdDatPhong == id);



			var HoaDon = new AppHoaDon();
			HoaDon.TGLapHD = DateTime.Now;

			var tonggio = 0.0;
			ChiTietDP.TGCheckOut = DateTime.Now;

			if (ChiTietDP.TGKetThuc < DateTime.Now)     // thanh toán trể = TGBatDau => TGHienTai
			{
				tonggio = (float)(DateTime.Now - ChiTietDP.TGBatDau).TotalHours;
			}
			else
			{
				tonggio = (float)(ChiTietDP.TGKetThuc - ChiTietDP.TGBatDau).TotalHours;
			}
			HoaDon.TongThanhToan = (float)(tonggio * ChiTietDP.appPhong.Gia);

			var NhanVien = _CafeDbContext.AppNhanViens
								.Where(i => i.IdKhachHang == userId)
								.FirstOrDefault(i => i.IdKhachHang == userId);



			HoaDon.IdNhanVien = NhanVien.IdNhanVien;
			HoaDon.IdKhachHang = ChiTietDP.appKhachHang.IdKhachHang;
			HoaDon.IdDatPhong = id;


			ChiTietDP.TongThoiLuong = tonggio;
			ChiTietDP.TongTien = (float)(tonggio * ChiTietDP.appPhong.Gia);
			ChiTietDP.TTDatPhong = TrangThaiDP.CHECKOUT;
			_CafeDbContext.Update(ChiTietDP);

			SetSuccessMesg($"Thanh Toán thành công {HoaDon.TongThanhToan}");
			_CafeDbContext.Add(HoaDon);
			_CafeDbContext.SaveChanges();
			return RedirectToAction("Index", "AdminDatPhong");
		}


		[HttpGet]
		public IActionResult ChiTietDP(int? id, int? idDmDV)
		{
			if (id == null)
			{
				SetErrorMesg("Vui lòng kiểm tra lại thông tin đặt phòng");
				return RedirectToAction("Index", "AdminDatPhong");
			}
			var ttDP = _CafeDbContext.appDatPhongs
							.Where(i => i.IdDatPhong == id)
							.Include(i => i.appPhong)
							.Include(i => i.appHoaDon)
							.Include(i => i.appDatDV)
								.ThenInclude(i => i.appDichVus)
							.Include(i => i.appKhachHang)
							.FirstOrDefault();
			ViewBag.DmDV = _CafeDbContext.AppDmDichVus.ToList();

			var DV = new List<AppDichVu>();
			if (idDmDV == 0)
			{
				DV = _CafeDbContext.appDichVus.ToList();
			}
			if (idDmDV > 0)
			{
				DV = _CafeDbContext.appDichVus
						.Where(i => i.IdDmDichVu == idDmDV)
						.ToList();
			}
			ViewBag.DichVus = DV;
			return View(ttDP);
		}
		[HttpGet]
		public IActionResult AddDVDatPhong(int? idDP, int? idDV)
		{
			var DP = _CafeDbContext.appDatPhongs
							.Where(i => i.IdDatPhong == idDP)
							.Include(i => i.appPhong)
							.Include(i => i.appHoaDon)
							.Include(i => i.appDatDV)
								.ThenInclude(i => i.appDichVus)
							.Include(i => i.appKhachHang)
							.FirstOrDefault();
			var DV = _CafeDbContext.appDichVus
							.Where(i => i.IdDichVu == idDV)
							.FirstOrDefault();
			var DatDV = new AppDatDV();
			DatDV.TGNhan = DateTime.Now;
			DatDV.Size = SizeWater.L;
			DatDV.SoLuong = 1;
			DatDV.IdDatPhong = idDP.Value;
			DatDV.IdDichVu = idDV.Value;
			DatDV.IdKhachHang = DP.IdKhachHang;
			DatDV.TT_DatDV = TrangThaiDatDV.DANHAN;
			DatDV.TongTien = DV.Gia * DatDV.SoLuong;


			_CafeDbContext.Add(DatDV);
			_CafeDbContext.SaveChanges();
            return RedirectToAction("ChiTietDP", new { id = idDP });
        }
		[HttpGet]
		public IActionResult DeleteDVDatPhong(int? id, int? idDP)
		{
            var DV = _CafeDbContext.AppDatDVs
                            .Where(i => i.IdDatDV == id)
                            .FirstOrDefault();
			_CafeDbContext.Remove(DV);
			_CafeDbContext.SaveChanges();
            return RedirectToAction("ChiTietDP", new { id = idDP });

        }
		[HttpPost]
		public IActionResult UpdateDP(int? idDP, AppDatPhong appDP, List<UpdateInfo> appDatDVs)
		{
			
			return RedirectToAction("ChiTietDP", new { id = idDP });
        }
		public class UpdateInfo
		{
			public string SoLuong { get; set; }
			public string Size { get; set; }
		}
    }
}
// hien trang phong: Trùng giờ || Phòng trống 