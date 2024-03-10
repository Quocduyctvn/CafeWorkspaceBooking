using AutoMapper;
using CafeWorkspaceBooking.Areas.Admin.ViewModels;
using CafeWorkspaceBooking.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CafeWorkspaceBooking.Areas.Admin.Controllers
{
	public class AdminPhongController : AdminControllerBase
	{
		public AdminPhongController(CafeDbContext cafeDbContext, IMapper mapper) : base(cafeDbContext, mapper)
		{
		}

		public IActionResult Index()
		{
            var phong = _CafeDbContext.AppPhongs.ToList();
            if(phong== null)
            {
                SetErrorMesg("Hiện tại chưa có phòng");
                return RedirectToAction ("Index", "AdminHome");
            }

           


			return View(phong);
		}
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Create(PhongVM phongVM, [FromServices] IWebHostEnvironment envi)
		{
			if (!ModelState.IsValid)
			{
				return View(phongVM);
			}
			//===============================
			var phong = new AppPhong { };
			phong.TenPhong = phongVM.TenPhong;
			phong.SucChua = phongVM.SucChua;
			phong.Gia = phongVM.Gia;
			phong.MoTa = phongVM.MoTa;

            phong.Img = UploadFile(phongVM.Img, envi.WebRootPath);
            foreach (var img in phongVM.appImgPhongs) // lặp qua các danh sách img
            {
                if (img != null)
                {
                    var ImgPhong = new AppImgPhong();
                    ImgPhong.Path = UploadFile(img, envi.WebRootPath);
                    phong.appImgPhongs.Add(ImgPhong); // Thêm Ảnh vào bảng Imgphong
                }
            }

            try
            {
                _CafeDbContext.Add(phong);
                _CafeDbContext.SaveChanges();
                SetSuccessMesg("Thêm Phòng Thành Công");
            }
            catch (Exception err)
            {
                SetErrorMesg("Lỗi thêm " + err);
            }
            return RedirectToAction(nameof(Index));
		}
		private string UploadFile(IFormFile file, string dir)// kieu dữ liệu của file ảnh đc gửi lên server là IFormFile
		{
            var fName = file.FileName;
            fName = Path.GetFileNameWithoutExtension(fName)
                + DateTime.Now.Ticks
                + Path.GetExtension(fName);

            var res = "/Phong/" + fName;
            fName = Path.Combine(dir, "Phong", fName);    //Path.Conbine() dùng để kết hợp các đường dẫn thành 1 
            var stream = System.IO.File.Create(fName);
            file.CopyTo(stream);
            stream.Dispose();
			return res;
        }

        [Authorize(Roles = "NHANVIEN")]
        public  IActionResult Edit(int? id)
        {
            if (id== null)
            {
                return View();
            }
            var oldPhong = _CafeDbContext.AppPhongs
                .Where(i => i.IdPhong == id)
                .Select(i => new PhongVM
                {
                    TenPhong = i.TenPhong,
                    Gia = i.Gia,
                    SucChua = i.SucChua,
                    MoTa = i.MoTa,
                    Imgs = i.Img,
                    ImgPhong = i.appImgPhongs.Select(img => img.Path).ToList()
                })
                .AsEnumerable() 
                .SingleOrDefault();


            if (oldPhong ==  null)
            {
                SetErrorMesg("Vui lòng kiểm tra lại hệ thống - Phòng không tồn tại");
                return View();
            }
            ViewBag.id = id;
            return View(oldPhong);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int? id, PhongVM phong, [FromServices] IWebHostEnvironment env)  // lấy lại id từ phương thức Get
        {
            //check dữ liệu
            if(!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Vui lòng cập nhật thông tin chính xác!");
                return View();
            }
            var oldPhong = await _CafeDbContext.AppPhongs.FindAsync(id);
            if (oldPhong == null)
            {
                SetErrorMesg("Vui lòng kiểm tra lại hệ thống - Phòng không tồn tại");
                return View();
            }

            // Cập nhật Dữ liệu 
            // do update có hình ảnh nên không biết dùng Imapper dược 
            oldPhong.TenPhong = phong.TenPhong;
            oldPhong.SucChua = phong.SucChua;
            oldPhong.Gia = phong.Gia;
            oldPhong.MoTa = phong.MoTa;

            //Xóa Ảnh và cập nhật ảnh 
            if (phong.Img is not null)
            {
                System.IO.File.Delete(Path.Combine(env.WebRootPath + oldPhong.Img));    // xóa ảnh cũ 
                oldPhong.Img = UploadFile(phong.Img, env.WebRootPath);                  // lưu ảnh MỚI  vào ổ đĩa và csdl
            }

            if (phong.appImgPhongs is not null)                                         //Xóa Ảnh con và cập nhật ảnh con 
            {
                var listImg = _CafeDbContext.AppImgPhongs.Where(i => i.IdPhong == id).ToList();
                // xóa file 
                foreach (var img in listImg)
                {
                    System.IO.File.Delete(env.WebRootPath + img.Path);
                }
                _CafeDbContext.RemoveRange(listImg);                                    // xóa 1 dãy các ảnh trong database 
                // Upload ảnh sp ( nhieuuef ảnh)
                foreach (var img in phong.appImgPhongs)                                 // lập danh sách ảnh mới 
                {
                    if (img != null)
                    {
                        var imgPhongs = new AppImgPhong();
                        imgPhongs.Path = UploadFile(img, env.WebRootPath);
                        oldPhong.appImgPhongs.Add(imgPhongs);
                    }
                }
            }

            try
            {
                _CafeDbContext.AppPhongs.Update(oldPhong);
                _CafeDbContext.SaveChanges();
                SetSuccessMesg("Cập Nhập Phòng Thành Công");
            }
            catch (Exception ex)
            {
                SetErrorMesg("Đã xảy ra lỗi trong quá trình xử lí" + ex.Message);
            }

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int id, [FromServices] IWebHostEnvironment env)
        {
            if(id <= 0)
            {
                SetErrorMesg("Đã xảy ra lỗi trong quá trình xử lí");
                return RedirectToAction(nameof(Index));
            }

            // Lấy dữ liêuj 
            var phong = _CafeDbContext.AppPhongs.Find(id);
            if(phong == null)
            {
                SetErrorMesg("Đã xảy ra lỗi trong quá trình xử lí");  // không tìm thấy 
            }
            var ImgPhong = _CafeDbContext.AppImgPhongs.Where(i=> i.IdPhong == id).ToList();


            // Xóa Dữ liệu 
            try
            {
                _CafeDbContext.AppPhongs.Remove(phong);
                System.IO.File.Delete(Path.Combine(env.WebRootPath, phong.Img.TrimStart('/')));  // TrimStart loại bỏ kí tự / 

                foreach(var item in ImgPhong)
                {
                    _CafeDbContext.AppImgPhongs.Remove(item);
                    System.IO.File.Delete(Path.Combine(env.WebRootPath, item.Path.TrimStart('/')));
                }
                _CafeDbContext.SaveChanges();
                SetSuccessMesg($"Xóa Phòng [{phong.TenPhong}] thành công");
            }
            catch (Exception ex)
            {
                SetErrorMesg($"Không thể xóa - Xem chi tiết {ex.Message}");
            }


            return RedirectToAction(nameof(Index));
        }
    }
}
