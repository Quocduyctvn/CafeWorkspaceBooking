using AutoMapper;
using CafeWorkspaceBooking.Areas.Admin.ViewModels;
using CafeWorkspaceBooking.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CafeWorkspaceBooking.Areas.Admin.Controllers
{
	public class AdminDichVuController : AdminControllerBase
	{
		public AdminDichVuController(CafeDbContext cafeDbContext, IMapper mapper) : base(cafeDbContext, mapper)
		{
		}

		public IActionResult Index(string? search_Dm, string? search_DV)
		{
			var dmDV = _CafeDbContext.AppDmDichVus
						.ToList();
			if (!string.IsNullOrEmpty(search_Dm))
			{
				dmDV = _CafeDbContext.AppDmDichVus
					.Where(d => d.TenDmDv.Contains(search_Dm))
					.ToList();
			}
			ViewBag.dmDV = dmDV;







            var DV = new AdminConfigDV_VM();
			DV.DisplayDichVuVM = _CafeDbContext.appDichVus.ToList();

			if (!string.IsNullOrEmpty(search_DV))
			{
				// Thực hiện tìm kiếm không phân biệt chữ hoa chữ thường trong thuộc tính 'TenDv'
				DV.DisplayDichVuVM = DV.DisplayDichVuVM
					.Where(i => i.TenDv.IndexOf(search_DV, StringComparison.OrdinalIgnoreCase) >= 0)
					.ToList();
			}

			return View(DV);
		}

		[HttpPost]
		public IActionResult AddDanhMucDichvu(string name)
		{
			var DMDV = new AppDmDichVu();
			DMDV.TenDmDv = name;

			_CafeDbContext.Add(DMDV);
			_CafeDbContext.SaveChanges();

			return RedirectToAction("Index");
		}


		[HttpPost]
		public IActionResult UpdateDanhMucDichvu(string name, string id)
		{
			int Id = int.Parse(id);
			var CTDmDV = _CafeDbContext.AppDmDichVus
						.Find(Id);
			CTDmDV.TenDmDv = name;

			_CafeDbContext.Update(CTDmDV);
			_CafeDbContext.SaveChanges();
			ViewBag.CtDmDV = CTDmDV;
			return RedirectToAction("Index");
		}
		[HttpPost]
		public IActionResult DeleteDanhMucDichvu(string id, [FromServices] IWebHostEnvironment envi)
		{
			int Id = int.Parse(id);
			var CTDmDV = _CafeDbContext.AppDmDichVus                    // cha
						.Find(Id);
			var ListDvu = _CafeDbContext.appDichVus
							.Where(i => i.IdDmDichVu == Id)
							.ToList();
			foreach (var item in ListDvu)                               // lặp con 
			{
				var imgdv = _CafeDbContext.AppImgDV
							.Where(i => i.IdDichVu == item.IdDichVu)
							.ToList();
				if (imgdv != null)
				{
					foreach (var itemChild in imgdv)                        // lặp cháu
					{
						_CafeDbContext.AppImgDV.Remove(itemChild);
						System.IO.File.Delete(Path.Combine(envi.WebRootPath, itemChild.Path.TrimStart('/')));
					}
				}
				System.IO.File.Delete(Path.Combine(envi.WebRootPath, item.ImageUrl.TrimStart('/')));
				_CafeDbContext.appDichVus.Remove(item);

			}
			_CafeDbContext.Remove(CTDmDV);
			_CafeDbContext.SaveChanges();
			return RedirectToAction("Index");
		}


		[HttpPost]
		public IActionResult AddDichVu(AdminConfigDV_VM dvVM, [FromServices] IWebHostEnvironment envi)
		{
			var newDV = dvVM.CreateDichVuVM;
			AppDichVu dv = new AppDichVu();
			dv.IdDmDichVu = newDV.IdDmDichVu;
			dv.TenDv = newDV.TenDv;
			dv.Gia = newDV.Gia;
			dv.ThanhPhan = newDV.ThanhPhan;
			dv.Mota = newDV.Mota;
			dv.ImageUrl = UploadFile(newDV.ImageUrlFormFile, envi.WebRootPath);

			dv.appImgDV = new List<AppImgDV>();
			foreach (var item in newDV.appImgDVFormFile)
			{
				if (item != null)
				{
					AppImgDV imgdv = new AppImgDV();
					imgdv.Path = UploadFile(item, envi.WebRootPath);
					dv.appImgDV.Add(imgdv);  // add từ Cha xuống nên không gán id cha

				}
			}
			_CafeDbContext.Add(dv);
			_CafeDbContext.SaveChanges();
			SetSuccessMesg("Thêm Sản Phẫm Thành Công");
			return RedirectToAction("Index");
		}
		private string UploadFile(IFormFile file, string dir)
		{
			// Lấy tên gốc của tệp
			var fName = file.FileName;

			// Thay đổi tên tệp để đảm bảo tính duy nhất bằng cách thêm timestamp vào tên
			fName = Path.GetFileNameWithoutExtension(fName)
				+ DateTime.Now.Ticks
				+ Path.GetExtension(fName);

			// Xác định đường dẫn trên server để lưu trữ tệp
			var res = "/DichVu/" + fName;  // Đường dẫn này có thể được sử dụng sau này để truy cập tệp từ client

			// Đường dẫn tuyệt đối để lưu trữ tệp trên server
			fName = Path.Combine(dir, "DichVu", fName);

			// Tạo và ghi tệp trên server từ dữ liệu của tệp được gửi lên
			var stream = System.IO.File.Create(fName);
			file.CopyTo(stream);
			stream.Dispose();

			// Trả về đường dẫn tương đối của tệp, có thể được sử dụng để hiển thị hình ảnh sau này
			return res;
		}
		[HttpGet]
		public IActionResult DetailDV(int id)
		{
			var dv = _CafeDbContext.appDichVus
							.Include(i => i.appImgDV)
							.Where(i => i.IdDichVu == id)
							.FirstOrDefault();
			if (dv == null)
			{
				SetErrorMesg("Không tìm thấy sản phẫm - vui lòng lh admin");
				return RedirectToAction("Index");
			}

			return View(dv);
		}
		[HttpGet]
		public IActionResult EditDV(int id)
		{
			var dmDV = _CafeDbContext.AppDmDichVus
						.ToList();

			ViewBag.dmDV = dmDV;
			var DV = _CafeDbContext.appDichVus
						.Where(i => i.IdDichVu == id)
						.Select(i => new AdminDichVuVM
						{
							IdDmDichVu = i.IdDmDichVu,
							TenDv = i.TenDv,
							Gia = i.Gia,
							ThanhPhan = i.ThanhPhan,
							Mota = i.Mota,
							ImageUrl = i.ImageUrl,
							appImgDV = i.appImgDV.Select(img => img.Path).ToList()
						})
						.AsEnumerable()
						.SingleOrDefault();
			return View(DV);
		}

		[HttpPost]
		public IActionResult EditDV(int id, AdminDichVuVM DV, [FromServices] IWebHostEnvironment env)
		{
			if(id== null)
			{
				SetErrorMesg("Vui lòng kiểm tra lại id truyền");
				return RedirectToAction("Index");
			}
			var oldDV = _CafeDbContext.appDichVus
						.Find(id);
			oldDV.IdDmDichVu = DV.IdDmDichVu;
			oldDV.TenDv = DV.TenDv;
			oldDV.Gia = DV.Gia;
			oldDV.ThanhPhan = DV.ThanhPhan;
			oldDV.Mota = DV.Mota;

			if (DV.ImageUrlFormFile is not null)
			{
				System.IO.File.Delete(Path.Combine(env.WebRootPath + oldDV.ImageUrl));    // xóa ảnh cũ trong ổ đĩa 
				oldDV.ImageUrl = UploadFile(DV.ImageUrlFormFile, env.WebRootPath);                  // lưu ảnh MỚI  vào ổ đĩa và csdl
			}

			if (DV.appImgDVFormFile is not null)                                         //Xóa Ảnh con và cập nhật ảnh con 
			{
				var listImg = _CafeDbContext.AppImgDV.Where(i => i.IdDichVu == id).ToList();
				// xóa file cũ trong ổ đĩa 
				foreach (var img in listImg)
				{
					System.IO.File.Delete(env.WebRootPath + img.Path);
				}
				_CafeDbContext.RemoveRange(listImg);                                    // xóa 1 dãy các ảnh trong database 
																						// Upload ảnh sp ( nhieuuef ảnh)
				foreach (var img in DV.appImgDVFormFile)                                 // lập danh sách ảnh mới 
				{
					if (img != null)
					{
						var imgDV = new AppImgDV();
						imgDV.Path = UploadFile(img, env.WebRootPath);
						oldDV.appImgDV.Add(imgDV);
					}
				}
			}
			try
			{
				_CafeDbContext.appDichVus.Update(oldDV);
				_CafeDbContext.SaveChanges();
				SetSuccessMesg("Cập Nhập Phòng Thành Công");
			}
			catch (DbUpdateConcurrencyException ex)
			{
				var innerException = ex.InnerException;
				while (innerException != null)
				{
					Console.WriteLine("Inner Exception: " + innerException.Message);
					innerException = innerException.InnerException;
				}
				SetErrorMesg("Lỗi cập nhật cơ sở dữ liệu: " + ex.Message);
			}
			catch (DbUpdateException ex)
			{
				// Xử lý các lỗi cập nhật cơ sở dữ liệu khác
				SetErrorMesg("Lỗi cập nhật cơ sở dữ liệu: " + ex.Message);
			}
			catch (Exception ex)
			{
				// Xử lý bất kỳ ngoại lệ không mong muốn nào khác
				SetErrorMesg("Đã xảy ra lỗi trong quá trình xử lí" + ex.Message);
			}

			return RedirectToAction(nameof(Index));
		}
		[HttpGet]
		public IActionResult DeleteDV(int id, [FromServices] IWebHostEnvironment env)
		{
			if (id <= 0)
			{
				SetErrorMesg("Đã xảy ra lỗi trong quá trình xử lí");
				return RedirectToAction(nameof(Index));
			}

			// Lấy dữ liêu
			var DV = _CafeDbContext.appDichVus.Find(id);
			if (DV == null)
			{
				SetErrorMesg("Đã xảy ra lỗi trong quá trình xử lí");  // không tìm thấy
			}
			var ImgDVs = _CafeDbContext.AppImgDV.Where(i => i.IdDichVu == id).ToList();

			// Xóa Dữ liệu
			try
			{
				
				foreach (var item in ImgDVs)
				{
					_CafeDbContext.AppImgDV.Remove(item);
					System.IO.File.Delete(Path.Combine(env.WebRootPath, item.Path.TrimStart('/')));
				}
				_CafeDbContext.appDichVus.Remove(DV);
				System.IO.File.Delete(Path.Combine(env.WebRootPath, DV.ImageUrl.TrimStart('/')));  // TrimStart loại bỏ kí tự

				_CafeDbContext.SaveChanges();
				SetSuccessMesg($"Xóa Sản phẫm [{DV.TenDv}] thành công");
			}
			catch (Exception ex)
			{
				SetErrorMesg($"Không thể xóa - Xem chi tiết {ex.Message}");
			}

			return RedirectToAction(nameof(Index));
		}


		

    }
}