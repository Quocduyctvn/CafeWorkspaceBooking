using AutoMapper;
using CafeWorkspaceBooking.Models;
using Microsoft.AspNetCore.Mvc;
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
			return View();
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


			var myAppConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
			var Username = myAppConfig.GetValue<string>("EmailConfig:Username");
			var Password = myAppConfig.GetValue<string>("EmailConfig:Password");
			var Host = myAppConfig.GetValue<string>("EmailConfig:Host");
			var Port = myAppConfig.GetValue<int>("EmailConfig:Port");
			var FromEmail = myAppConfig.GetValue<string>("EmailConfig:FromEmail");

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
