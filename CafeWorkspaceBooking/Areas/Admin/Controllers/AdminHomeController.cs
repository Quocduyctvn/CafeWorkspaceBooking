using AutoMapper;
using CafeWorkspaceBooking.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Security.Claims;

namespace CafeWorkspaceBooking.Areas.Admin.Controllers
{
	public class AdminHomeController : AdminControllerBase
	{
		public AdminHomeController(CafeDbContext cafeDbContext, IMapper mapper) : base(cafeDbContext, mapper)
		{
		}

		public IActionResult Index()
		{

            var ChartPhong = _CafeDbContext.AppPhongs
                           .Include(i => i.appDatPhongs)
                           .ThenInclude(i => i.appHoaDon)
                           .ToList();
            // Tạo một Dictionary để lưu trữ số lượng hóa đơn theo ngày trong tuần
            var hoaDonTheoNgay = new Dictionary<DayOfWeek, int>();

            // Khởi tạo giá trị mặc định cho Dictionary
            foreach (DayOfWeek ngayTrongTuan in Enum.GetValues(typeof(DayOfWeek)))
            {
                hoaDonTheoNgay[ngayTrongTuan] = 0;
            }

            // Lặp qua từng danh mục dịch vụ
            foreach (var p in ChartPhong)
            {
                // Lấy số lượng hóa đơn của danh mục dịch vụ
                int soLuongHoaDon = p.appDatPhongs
                    .Select(datDV => datDV.appHoaDon)
                    .Count();


                // Lấy ngày trong tuần của hóa đơn và cập nhật giá trị trong Dictionary
                foreach (var dv in p.appDatPhongs)
                {
                    if (dv.appHoaDon != null)
                    {
                        DayOfWeek ngayTrongTuan = dv.appHoaDon.TGLapHD.DayOfWeek;
                        hoaDonTheoNgay[ngayTrongTuan] += 1; // Assuming each hoaDon contributes 1 to the count
                    }
                }

            }

            var mangSoLuongHoaDonTheoNgay = hoaDonTheoNgay.Values.ToArray();

            ViewBag.MangSoLuongHoaDonTheoNgay = mangSoLuongHoaDonTheoNgay;

			ViewBag.NotificationType = "success";
			ViewBag.NotificationMessage = "Thực hiện thành công!";
			return View();
		}

		[HttpGet]
		public async Task<IActionResult> CheckedMessage()
		{


			string refererUrl = Request.Headers["Referer"].ToString();
			// Chuyển hướng về trang trước đó
			return Redirect(refererUrl);
		}



		public IActionResult SendEmail()
		{

			ClaimsIdentity identity = (ClaimsIdentity)User.Identity;  // lấy thông từ Claims xuống
			Claim userIdClaim = identity.FindFirst("UserId");
			int userId = int.Parse(userIdClaim?.Value);

			var khachhang = _CafeDbContext.AppKhachHangs.Find(userId);

			var mail = new SendMail()
			{
				ToEmailAddress = khachhang.Email,
				Subject = "Thư cảm ơn",
				Content = "Lolita cảm ơn quý khách đã đặt phòng"
			};


			//var myAppConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
			//var Username = myAppConfig.GetValue<string>("EmailConfig:Username");
			//var Password = myAppConfig.GetValue<string>("EmailConfig:Password");
			//var Host = myAppConfig.GetValue<string>("EmailConfig:Host");
			//var Port = myAppConfig.GetValue<int>("EmailConfig:Port");
			//var FromEmail = myAppConfig.GetValue<string>("EmailConfig:FromEmail");
			var Username = "quocduyctvn@gmail.com";
			var Password = "fcdl pezn rchn apcp";
			var Host = "smtp.gmail.com";
			var Port = 587;
			var FromEmail = "quocduyctvn@gmail.com";


			MailMessage message = new MailMessage();
			message.From = new MailAddress(FromEmail);						// nguoi gui 
			message.To.Add(mail.ToEmailAddress.ToString());                 // nguoi nhan 
			message.Subject = mail.Subject;
			message.IsBodyHtml = true;
			message.Body = mail.Content;

			//message.Attachments.Add(new Attachment(PostedFile.OpenReadStream(), PostedReadFile.File.Name)); // PostedFile à biến file nếu cần gửi file 


			SmtpClient mailClient = new SmtpClient();

            try
			{
                mailClient.EnableSsl = true;

                mailClient.UseDefaultCredentials = false;
				mailClient.Credentials = new System.Net.NetworkCredential(Username, Password);
				mailClient.Host = Host;
				mailClient.Port = Port;

				mailClient.Send(message);
			}
			catch(Exception ex)
			{

			}
			return View(nameof(Index), "Home");
		}
	}
}
