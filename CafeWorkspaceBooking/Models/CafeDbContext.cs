using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace CafeWorkspaceBooking.Models
{
	public class CafeDbContext : DbContext
	{
		public CafeDbContext(DbContextOptions options) : base(options)
		{

		}
		public DbSet<AppDanhGia> AppDanhGias { get; set; }
		public DbSet<AppDatPhong> appDatPhongs { get; set; }

		public DbSet<AppHuyDatPhong> AppHuyDatPhongs { get; set; }
		public DbSet<AppImgPhong> AppImgPhongs { get; set; }
		public DbSet<AppKhachHang> AppKhachHangs { get; set; }
		public DbSet<AppHoaDon> AppHoaDons { get; set; }

		// public DbSet<AppLoaiPhong> AppLoaiPhongs { get; set; }
		public DbSet<AppNhanVien> AppNhanViens { get; set; }
		public DbSet<AppPhong> AppPhongs { get; set; }
        public DbSet<AppThongBao> AppThongBaos { get; set; }


        public DbSet<AppDichVu> appDichVus { get; set; }
        public DbSet<AppDatDV> AppDatDVs { get; set; }
        public DbSet<AppDmDichVu> AppDmDichVus { get; set; }
        public DbSet<AppHuyDatDV> AppHuyDatDV { get; set; }
        public DbSet<AppImgDV> AppImgDV { get; set; }


        //public DbSet<AppTGDatPhong> AppTGDatPhongs { get; set; }

        // public DbSet<AppRole> AppRoles { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
            modelBuilder.Entity<AppDanhGia>()
				.HasOne(d => d.appKhachHang)
				.WithMany(_kh => _kh.appDanhGias)
				.HasForeignKey(d => d.IdKhachHang)
				.IsRequired()
				.OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<AppDanhGia>()
				.HasOne(dp => dp.appPhong)
				.WithMany(dp => dp.appDanhGias)
				.HasForeignKey(dp => dp.IdPhong);



            modelBuilder.Entity<AppDatPhong>()
			   .HasOne(dp => dp.appHoaDon)
			   .WithOne(hd => hd.appDatPhong)
			   .HasForeignKey<AppHoaDon>(hd => hd.IdDatPhong);
            modelBuilder.Entity<AppDatPhong>()
				.HasOne(dp => dp.appHuyDatPhong)
				.WithOne(hd => hd.appDatPhong)
				.HasForeignKey<AppHuyDatPhong>(hd => hd.IdDatPhong);
			modelBuilder.Entity<AppDatPhong>()
				.HasOne(_kh => _kh.appKhachHang)
				.WithMany(dp => dp.appDatPhongs)
				.HasForeignKey(dp => dp.IdKhachHang)
				.OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<AppDatPhong>()
				.HasOne(dp => dp.appPhong)
				.WithMany(dp => dp.appDatPhongs)
				.HasForeignKey(dp => dp.IdPhong);

			modelBuilder.Entity<AppHuyDatPhong>()
				.HasOne(dp => dp.appKhachHang)
				.WithMany(dp => dp.appHuyDatPhongs)
				.HasForeignKey(dp => dp.IdKhachHang);

            modelBuilder.Entity<AppDichVu>()
				.HasOne(dp => dp.appDmDichVu)
				.WithMany(dp => dp.appDichVus)
				.HasForeignKey(dp => dp.IdDmDichVu);

            modelBuilder.Entity<AppDatDV>()
				.HasOne(dp => dp.appDichVus)
				.WithMany(dp => dp.appDatDVs)
				.HasForeignKey(dp => dp.IdDichVu);
            modelBuilder.Entity<AppDatDV>()
				.HasOne(dp => dp.appKhachHang)
				.WithMany(dp => dp.appDatDVs)
				.HasForeignKey(dp => dp.IdKhachHang);
            modelBuilder.Entity<AppDatDV>()
				.HasOne(dp => dp.appDatPhong)
				.WithMany(dp => dp.appDatDV)
				.HasForeignKey(dp => dp.IdDatPhong);
            modelBuilder.Entity<AppDatDV>()
				.HasOne(dp => dp.appHoaDon)
			   .WithOne(hd => hd.appDatDV)
			   .HasForeignKey<AppHoaDon>(hd => hd.IdDatDV);



            modelBuilder.Entity<AppHoaDon>()
				.HasOne(dp => dp.appNhanVien)
				.WithMany(dp => dp.appHoaDons)
				.HasForeignKey(dp => dp.IdNhanVien);
            modelBuilder.Entity<AppHoaDon>()
				.HasOne(dp => dp.appkhachHang)
				.WithMany(dp => dp.appHoaDon)
				.HasForeignKey(dp => dp.IdKhachHang);




            modelBuilder.Entity<AppNhanVien>()
				.HasOne(nv => nv.appKhachHang)
				.WithOne(_kh => _kh.appNhanVien)
				.HasForeignKey<AppNhanVien>(nv => nv.IdKhachHang);


            modelBuilder.Entity<AppImgPhong>()
                .HasOne(p => p.AppPhong)
                .WithMany(dp => dp.appImgPhongs)
                .HasForeignKey(dp => dp.IdPhong);

            modelBuilder.Entity<AppNhanVien>()
				.HasOne(p => p.appKhachHang)
				.WithOne(dp => dp.appNhanVien)
				.HasForeignKey< AppNhanVien>(dp => dp.IdKhachHang);

            modelBuilder.Entity<AppHuyDatDV>()
				.HasOne(p => p.appDatDV)
				.WithOne(dp => dp.appHuyDatDV)
				.HasForeignKey<AppHuyDatDV>(dp => dp.IdDatDV);
            //        modelBuilder.Entity<AppTGDatPhong>()
            //.HasOne(p => p.appPhong)
            //.WithMany(dp => dp.appTGDatPhongs)
            //.HasForeignKey(dp => dp.IdPhong);




            modelBuilder.Entity<AppThongBao>()
                .HasOne(t => t.appPhong)
				.WithMany(t => t.appThongBao)
                .HasForeignKey(t => t.IdPhong)
                .OnDelete(DeleteBehavior.NoAction); // Chỉ định NO ACTION


            modelBuilder.Entity<AppThongBao>()
				.HasOne(t => t.appDatPhong)
				.WithMany(t => t.appThongBao)
				.HasForeignKey(t => t.IdDatPhong)
                .OnDelete(DeleteBehavior.NoAction); // Chỉ định NO ACTION

            modelBuilder.Entity<AppThongBao>()
				.HasOne(t => t.appHuyDatPhong)
				.WithOne(t => t.appThongBao)
				.HasForeignKey<AppThongBao>(t => t.IdHuyDatPhong)
				.OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AppThongBao>()
				.HasOne(t => t.appKhachHang)
				.WithMany(t => t.appThongBao)
				.HasForeignKey(t => t.IdKhachHang);

			modelBuilder.Entity<AppThongBao>()
				.HasOne(t => t.appDanhGia)
				.WithMany(t => t.appThongBao)
				.HasForeignKey(t => t.IdDanhGia);

            modelBuilder.Entity<AppThongBao>()
				.HasOne(t => t.appHuyDatDV)
                .WithOne(t => t.appThongBao)
                .HasForeignKey<AppThongBao>(t => t.IdHuyDatDV)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<AppImgDV>()
				.HasOne(t => t.appDichVu)
				.WithMany(t => t.appImgDV)
				.HasForeignKey(t => t.IdDichVu)
				.OnDelete(DeleteBehavior.Restrict);
        }
    }
}
