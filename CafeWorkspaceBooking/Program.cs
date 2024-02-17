using CafeWorkspaceBooking.Models;
using CafeWorkspaceBooking.WebConfig;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllersWithViews();

//DATABASE 
builder.Services.AddDbContext<CafeDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});
//AUTOMAPPER
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));   //  đăng kí dịch vụ automapper 
//AUTHEN
builder.Services.AddAuthentication()
	.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options => {
		options.LoginPath = "/Account/Login";   // action nào y/c đăng nhập- mà ch đ/n thì đá về trang đăng kí ;; vd: trang đang kí phong
		options.ExpireTimeSpan = TimeSpan.FromMinutes(10); 
		options.AccessDeniedPath = "/Home/Index"; // trả về trang mà y/c Cần xác thực danh tính

		//options.Events = new CookieAuthenticationEvents 
		//{
		//	OnRedirectToAccessDenied = context =>
		//	{
		//		var user = context.HttpContext.User;
		//		if (user.Identity.IsAuthenticated)
		//		{
		//			if (user.IsInRole("KhachHang"))
		//			{
		//				context.RedirectUri = "/Home/Index";
		//			}
		//			else if (user.IsInRole("Admin") || user.IsInRole("NhanVien"))
		//			{
		//				context.RedirectUri = "/Admin/Home/Index";
		//			}
		//		}

		//		return Task.CompletedTask;
		//	}
		//};
	});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}

// app.UseStatusCodePagesWithRedirects("/Err/{0}");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();


//Thêm rounter cho admin
app.MapAreaControllerRoute(
  name: "Azdmin",
  areaName: "Admin",
  pattern: "Admin/{controller=AdminHome}/{action=Index}/{id?}"
);
app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
