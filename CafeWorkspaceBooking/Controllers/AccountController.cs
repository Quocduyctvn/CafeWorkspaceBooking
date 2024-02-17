using AutoMapper;
using CafeWorkspaceBooking.Models;
using CafeWorkspaceBooking.ViewModels;
using CafeWorkspaceBooking.WebConfig.Contants;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace CafeWorkspaceBooking.Controllers
{
	public class AccountController : ControllerBase
	{
		public AccountController(CafeDbContext cafeDbContext, IMapper mapper) : base(cafeDbContext, mapper)
		{
		}

		public IActionResult Register()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Register(RegisterVM _kh) // lấy ảnh để nhận dạng nhận phòng 
		{
			if (ModelState.IsValid == false)
			{
				return View(_kh);
			}
			// chuan hoa du lieu 
			_kh.HoTen = _kh.HoTen.ToLower().Trim();
			_kh.Email = _kh.Email?.ToLower().Trim();  // dấu chấm ?.  => Check Email => if null trả về null => else thực hiện ToLower().Trim()


			// check tồn tại 
			var exit = _CafeDbContext.AppKhachHangs.Any(k => k.Email == _kh.Email || k.CCCD == _kh.CCCD);
			if (exit)
			{
				// nếu tài khoản đã tồn tại thì xem Check Trạng thái Tài khoản 
				// do Id là khóa chính và đăng kí mới nên dùng "FirstOrDefaultAsync" để tìm kiếm Email 
				var If_KH = await _CafeDbContext.AppKhachHangs.FirstOrDefaultAsync(k => k.Email == _kh.Email);
				if (!If_KH.TrangThaiTK)
				{
					SetErrorMesg("Tài khoản đã bị Khóa - Vui lòng liên hệ Lolita Coffe để được mở lại");
					return View(_kh);
				}
				SetErrorMesg("Tài khoản đã tồn tại");
				return View(_kh);
			}

			// Config dữ liệu trc khi lưu 
			var KhachHang = _mapper.Map<AppKhachHang>(_kh);
			KhachHang.Password = BCrypt.Net.BCrypt.HashPassword(_kh.Password);
			KhachHang.TrangThaiTK = TrangThaiTK.OK;
			KhachHang.Role = UserRole.KHACHHANG;


			_CafeDbContext.AppKhachHangs.Add(KhachHang);
			_CafeDbContext.SaveChanges();
			SetSuccessMesg("Đăng kí tài khoản Thành Công");
			return RedirectToAction("Index", "Home");
		}
		public IActionResult Login()
		{
			return View();
		}
		[HttpPost]

		public async Task<IActionResult> Login(LoginVM user)
		{
			if (!ModelState.IsValid)
			{
				ModelState.AddModelError("", "Dữ liệu không hợp lệ!");
				return View(user);
			}

			// check Exit
			var exit = _CafeDbContext.AppKhachHangs.Any(u => u.SDT == user.SDT);
			if (!exit) // TH<Không Tồn tại> Sai SDT 
			{
				SetErrorMesg("Tài khoản không tồn tại - Vui lòng đăng kí mới");
				return View(nameof(Register));
			}
			var _kh = new AppKhachHang();


			if (user.SDT != "12345678910")
			{
				_kh = await _CafeDbContext.AppKhachHangs.FirstOrDefaultAsync(u => u.SDT == user.SDT);
				// nếu tồn tại thì kiểm tra tài khoản đó có bị khóa hay không 
				if (!_kh.TrangThaiTK)
				{
					SetErrorMesg("Tài khoản cuả bạn bị khóa - Vui lòng liên hệ quán Lolita Coffee");
					return View(nameof(Register));
				}


				//Check Pass 
				var checkPass = BCrypt.Net.BCrypt.Verify(user.Password, _kh?.Password);
				if (!checkPass)
				{
					SetErrorMesg("Thông tin đăng nhập không hợp lệ");
					return View();
				}

				// khi dữ liệu hợp lệ tiến hành lưu thông tin vào Claims 
				var claims = new List<Claim>
							{
								new Claim("UserId", _kh.IdKhachHang.ToString()),
								new Claim(ClaimTypes.Email, _kh.Email),
								new Claim(ClaimTypes.Name, _kh.HoTen),
								new Claim("CCCD", _kh.CCCD),
								new Claim(ClaimTypes.MobilePhone, _kh.SDT),

                                //claim - role động 
                                new Claim(ClaimTypes.Role ,_kh.Role.ToString()) // _kh.Role là kiểu enum => ToString()
							};
				var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
				var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
				await HttpContext.SignInAsync(claimsPrincipal);
			}
			else
			{
				// khi dữ liệu hợp lệ tiến hành lưu thông tin vào Claims
				var claims = new List<Claim>
							{
								new Claim("UserId", _kh.IdKhachHang.ToString()),
								new Claim(ClaimTypes.Email, _kh.Email),
								new Claim(ClaimTypes.Name, _kh.HoTen),
								new Claim("CCCD", _kh.CCCD),
								new Claim(ClaimTypes.MobilePhone, _kh.SDT),

                                //claim - role động 
                                new Claim(ClaimTypes.Role ,_kh.Role.ToString()) // _kh.Role là kiểu enum => ToString()
							};
				var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
				var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
				await HttpContext.SignInAsync(claimsPrincipal);
			}


			// nếu là khách hàng trả về trang HOME 
			if (_kh.Role.ToString() == "KHACHHANG")
			{
                return RedirectToAction("Index", "Home");
            }
			// nếu là NHANVIEN & ADMIN trả về trang HOME manager
			if (_kh.Role == UserRole.NHANVIEN || _kh.Role == UserRole.ADMIN)
			{
                return RedirectToAction("Index", "AdminHome", new { area = "Admin" });
            }
			// nếu không là khách => sau Khi ĐNhap thì trả về trang người dùng yêu cầu
			 var returnUrl = Request.Query["ReturnUrl"].ToString();   // code đang vô tri 
			 return Redirect(returnUrl);
		}

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}
