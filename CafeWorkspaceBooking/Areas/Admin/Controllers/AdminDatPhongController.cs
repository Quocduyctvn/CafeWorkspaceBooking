using AutoMapper;
using CafeWorkspaceBooking.Areas.Admin.ViewModels;
using CafeWorkspaceBooking.Models;
using CafeWorkspaceBooking.ViewModels;
using CafeWorkspaceBooking.WebConfig.Contants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CafeWorkspaceBooking.Areas.Admin.Controllers
{
	public class AdminDatPhongController : AdminControllerBase
	{
		public AdminDatPhongController(CafeDbContext cafeDbContext, IMapper mapper) : base(cafeDbContext, mapper)
		{
		}

		public IActionResult Index()
		{
			var ListDP = _CafeDbContext.appDatPhongs
							.Include(p => p.appPhong)
							.Include(i => i.appKhachHang)
							.Include(i=> i.appHoaDon)
							.ToList();
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
					.Any(otherCustomer =>
						otherCustomer.appPhong.IdPhong == item.appPhong.IdPhong && 
						((item.TGBatDau > otherCustomer.TGBatDau && item.TGBatDau < otherCustomer.TGKetThuc) ||
						(item.TGKetThuc > otherCustomer.TGBatDau && item.TGKetThuc < otherCustomer.TGKetThuc))&&
						item.TTDatPhong == otherCustomer.TTDatPhong
					);
				if (check_Trung)
				{
					DP.TrangThaiPhong = TrangThaiPhong.TRUNG;
				}
				else if (item.TTDatPhong == TrangThaiDP.DADUYET)
				{
					DP.TrangThaiPhong = TrangThaiPhong.DADAT;
				}
				else
				{
					DP.TrangThaiPhong = TrangThaiPhong.TRONG;
				}


				if (item.TGBatDau <= DateTime.Now && item.TTDatPhong == TrangThaiDP.DADUYET)	// Đang sử Dụng 
				{
					if(item.TGKetThuc >= DateTime.Now)											// trong TG  sử dụng
					{
						DP.TrangThaiPhong = TrangThaiPhong.CHO_THANHTOAN;
					}
					else																		// hết giờ
					{
						DP.TrangThaiPhong = TrangThaiPhong.THANHTOAN;
					}
					if(item.appHoaDon != null)   // khi thanh  toán xong thì dữ liệu vào bảng Hóa Đơn
					{
						DP.TrangThaiPhong = TrangThaiPhong.DA_THANHTOAN;
					}
				}
				if (item.TGBatDau.Day >= DateTime.Now.Day && item.TTDatPhong == TrangThaiDP.DADUYET)        // đặt r nhưng ch tới ngày
				{
					DP.TrangThaiPhong = TrangThaiPhong.CHO_SUDUNG;
				}
				if (item.TTDatPhong == TrangThaiDP.CHODUYET && item.TGKetThuc < DateTime.Now)  // Chờ duyệt nhưng quá hạn  
				{
					item.TTDatPhong = TrangThaiDP.QUA_HAN;
				}
				datphong.Add(DP);
			}
			return View(datphong);
		}



		public IActionResult HuyDatPhong( int id, string lido) // id phong lấy từ form
		{
			if (id == null)
			{
				SetErrorMesg("Vui lòng kiểm tra lại hệ thống");
				return View(lido);
			}
			ClaimsIdentity identity = (ClaimsIdentity)User.Identity;  // lấy thông từ Claims xuống
			Claim userIdClaim = identity.FindFirst("UserId");
			int userId = int.Parse(userIdClaim?.Value);


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
			return RedirectToAction("Index", "AdminDatPhong");
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
		public IActionResult NhanPhong(int id)
		{
			var ChiTietDP = _CafeDbContext.appDatPhongs.Find(id);
			var checkTG = ChiTietDP.TGBatDau > DateTime.Now;  //nhận sớm => đặt giờ lại (nhận trễ lấy giờ cũ=))  ) 
			if (checkTG)
			{
				ChiTietDP.TGBatDau = DateTime.Now;
			}

			_CafeDbContext.Update(ChiTietDP);
			_CafeDbContext.SaveChanges();
			SetSuccessMesg($"Khách hàng {ChiTietDP.appKhachHang.HoTen} đã nhận phòng");
			return RedirectToAction("Index", "AdminDatPhong");
		}

		public IActionResult ThanhToan(int id)
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

			var tonggio =0.0; 
			if(ChiTietDP.TGKetThuc < DateTime.Now)     // thanh toán trể = TGBatDau => TGHienTai 
			{
				tonggio =  (float)(DateTime.Now - ChiTietDP.TGBatDau).TotalHours;
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


			SetSuccessMesg($"Thanh Toán thành công { HoaDon.TongThanhToan}");
			_CafeDbContext.Add(HoaDon);
			_CafeDbContext.SaveChanges();
			return RedirectToAction("Index", "AdminDatPhong");
		}
	}
}
// hien trang phong: Trùng giờ || Phòng trống 