﻿// <auto-generated />
using System;
using CafeWorkspaceBooking.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CafeWorkspaceBooking.Migrations
{
    [DbContext(typeof(CafeDbContext))]
    [Migration("20240227165521_version-27-2")]
    partial class version272
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CafeWorkspaceBooking.Models.AppDanhGia", b =>
                {
                    b.Property<int>("IdDanhGia")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdDanhGia"));

                    b.Property<string>("CapDo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdDichVu")
                        .HasColumnType("int");

                    b.Property<int?>("IdKhachHang")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int>("IdPhong")
                        .HasColumnType("int");

                    b.Property<string>("NhanXet")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TGDanhGia")
                        .HasColumnType("datetime2");

                    b.HasKey("IdDanhGia");

                    b.HasIndex("IdKhachHang");

                    b.HasIndex("IdPhong");

                    b.ToTable("AppDanhGias");
                });

            modelBuilder.Entity("CafeWorkspaceBooking.Models.AppDatDV", b =>
                {
                    b.Property<int>("IdDatDV")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdDatDV"));

                    b.Property<int>("IdDatPhong")
                        .HasColumnType("int");

                    b.Property<int>("IdDichVu")
                        .HasColumnType("int");

                    b.Property<int>("IdKhachHang")
                        .HasColumnType("int");

                    b.HasKey("IdDatDV");

                    b.HasIndex("IdDatPhong");

                    b.HasIndex("IdDichVu");

                    b.HasIndex("IdKhachHang");

                    b.ToTable("AppDatDVs");
                });

            modelBuilder.Entity("CafeWorkspaceBooking.Models.AppDatPhong", b =>
                {
                    b.Property<int>("IdDatPhong")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdDatPhong"));

                    b.Property<int>("IdKhachHang")
                        .HasColumnType("int");

                    b.Property<int>("IdPhong")
                        .HasColumnType("int");

                    b.Property<int>("SLKhach")
                        .HasColumnType("int");

                    b.Property<DateTime>("TGBatDau")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("TGCheckIn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("TGCheckOut")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("TGDatOnline")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("TGKetThuc")
                        .HasColumnType("datetime2");

                    b.Property<int>("TTDatPhong")
                        .HasColumnType("int");

                    b.Property<double>("TongThoiLuong")
                        .HasColumnType("float");

                    b.Property<double>("TongTien")
                        .HasColumnType("float");

                    b.HasKey("IdDatPhong");

                    b.HasIndex("IdKhachHang");

                    b.HasIndex("IdPhong");

                    b.ToTable("appDatPhongs");
                });

            modelBuilder.Entity("CafeWorkspaceBooking.Models.AppDichVu", b =>
                {
                    b.Property<int>("IdDichVu")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdDichVu"));

                    b.Property<int>("IdDmDichVu")
                        .HasColumnType("int");

                    b.Property<string>("TenDv")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdDichVu");

                    b.HasIndex("IdDmDichVu");

                    b.ToTable("appDichVus");
                });

            modelBuilder.Entity("CafeWorkspaceBooking.Models.AppDmDichVu", b =>
                {
                    b.Property<int>("IdDmDichVu")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdDmDichVu"));

                    b.Property<string>("TenDmDv")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdDmDichVu");

                    b.ToTable("AppDmDichVus");
                });

            modelBuilder.Entity("CafeWorkspaceBooking.Models.AppHoaDon", b =>
                {
                    b.Property<int>("IdHoaDon")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdHoaDon"));

                    b.Property<int?>("IdDatDV")
                        .HasColumnType("int");

                    b.Property<int>("IdDatPhong")
                        .HasColumnType("int");

                    b.Property<int>("IdKhachHang")
                        .HasColumnType("int");

                    b.Property<int>("IdNhanVien")
                        .HasColumnType("int");

                    b.Property<DateTime>("TGLapHD")
                        .HasColumnType("datetime2");

                    b.Property<double>("TongThanhToan")
                        .HasColumnType("float");

                    b.HasKey("IdHoaDon");

                    b.HasIndex("IdDatDV")
                        .IsUnique()
                        .HasFilter("[IdDatDV] IS NOT NULL");

                    b.HasIndex("IdDatPhong")
                        .IsUnique();

                    b.HasIndex("IdKhachHang");

                    b.HasIndex("IdNhanVien");

                    b.ToTable("AppHoaDons");
                });

            modelBuilder.Entity("CafeWorkspaceBooking.Models.AppHuyDatDV", b =>
                {
                    b.Property<int>("IdHuyDatDV")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdHuyDatDV"));

                    b.Property<int>("IdDatDV")
                        .HasColumnType("int");

                    b.Property<string>("LiDoHuy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TGHuy")
                        .HasColumnType("datetime2");

                    b.Property<int?>("appThongBaoId")
                        .HasColumnType("int");

                    b.HasKey("IdHuyDatDV");

                    b.HasIndex("IdDatDV")
                        .IsUnique();

                    b.HasIndex("appThongBaoId");

                    b.ToTable("AppHuyDatDV");
                });

            modelBuilder.Entity("CafeWorkspaceBooking.Models.AppHuyDatPhong", b =>
                {
                    b.Property<int>("IdHuyDatPhong")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdHuyDatPhong"));

                    b.Property<int>("IdDatPhong")
                        .HasColumnType("int");

                    b.Property<int>("IdKhachHang")
                        .HasColumnType("int");

                    b.Property<string>("LiDoHuy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TGHuy")
                        .HasColumnType("datetime2");

                    b.HasKey("IdHuyDatPhong");

                    b.HasIndex("IdDatPhong")
                        .IsUnique();

                    b.HasIndex("IdKhachHang");

                    b.ToTable("AppHuyDatPhongs");
                });

            modelBuilder.Entity("CafeWorkspaceBooking.Models.AppImgPhong", b =>
                {
                    b.Property<int>("IdImgPhong")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdImgPhong"));

                    b.Property<int>("IdPhong")
                        .HasColumnType("int");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdImgPhong");

                    b.HasIndex("IdPhong");

                    b.ToTable("AppImgPhongs");
                });

            modelBuilder.Entity("CafeWorkspaceBooking.Models.AppKhachHang", b =>
                {
                    b.Property<int>("IdKhachHang")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdKhachHang"));

                    b.Property<string>("CCCD")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("GioiTinh")
                        .HasColumnType("bit");

                    b.Property<string>("HoTen")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("ImgKH")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("NgaySinh")
                        .HasColumnType("datetime2");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("SDT")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TrangThaiTK")
                        .HasColumnType("bit");

                    b.HasKey("IdKhachHang");

                    b.ToTable("AppKhachHangs");
                });

            modelBuilder.Entity("CafeWorkspaceBooking.Models.AppNhanVien", b =>
                {
                    b.Property<int>("IdNhanVien")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdNhanVien"));

                    b.Property<string>("ChucVu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("IdKhachHang")
                        .HasColumnType("int");

                    b.Property<double>("Luong")
                        .HasColumnType("float");

                    b.Property<DateTime>("NgayVaoLam")
                        .HasColumnType("datetime2");

                    b.HasKey("IdNhanVien");

                    b.HasIndex("IdKhachHang")
                        .IsUnique()
                        .HasFilter("[IdKhachHang] IS NOT NULL");

                    b.ToTable("AppNhanViens");
                });

            modelBuilder.Entity("CafeWorkspaceBooking.Models.AppPhong", b =>
                {
                    b.Property<int>("IdPhong")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdPhong"));

                    b.Property<double>("Gia")
                        .HasColumnType("float");

                    b.Property<string>("Img")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MoTa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SucChua")
                        .HasColumnType("int");

                    b.Property<string>("TenPhong")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdPhong");

                    b.ToTable("AppPhongs");
                });

            modelBuilder.Entity("CafeWorkspaceBooking.Models.AppThongBao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("CheckAll")
                        .HasColumnType("bit");

                    b.Property<bool>("Checked")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("IdDanhGia")
                        .HasColumnType("int");

                    b.Property<int?>("IdDatPhong")
                        .HasColumnType("int");

                    b.Property<int?>("IdHuyDatPhong")
                        .HasColumnType("int");

                    b.Property<int?>("IdKhachHang")
                        .HasColumnType("int");

                    b.Property<int?>("IdPhong")
                        .HasColumnType("int");

                    b.Property<string>("NDThongbao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenThongBao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IdDanhGia");

                    b.HasIndex("IdDatPhong");

                    b.HasIndex("IdHuyDatPhong")
                        .IsUnique()
                        .HasFilter("[IdHuyDatPhong] IS NOT NULL");

                    b.HasIndex("IdKhachHang");

                    b.HasIndex("IdPhong");

                    b.ToTable("AppThongBaos");
                });

            modelBuilder.Entity("CafeWorkspaceBooking.Models.AppDanhGia", b =>
                {
                    b.HasOne("CafeWorkspaceBooking.Models.AppKhachHang", "appKhachHang")
                        .WithMany("appDanhGias")
                        .HasForeignKey("IdKhachHang")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CafeWorkspaceBooking.Models.AppPhong", "appPhong")
                        .WithMany("appDanhGias")
                        .HasForeignKey("IdPhong")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("appKhachHang");

                    b.Navigation("appPhong");
                });

            modelBuilder.Entity("CafeWorkspaceBooking.Models.AppDatDV", b =>
                {
                    b.HasOne("CafeWorkspaceBooking.Models.AppDatPhong", "appDatPhong")
                        .WithMany("appDatDV")
                        .HasForeignKey("IdDatPhong")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CafeWorkspaceBooking.Models.AppDichVu", "appDichVus")
                        .WithMany("appDatDVs")
                        .HasForeignKey("IdDichVu")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CafeWorkspaceBooking.Models.AppKhachHang", "appKhachHang")
                        .WithMany("appDatDVs")
                        .HasForeignKey("IdKhachHang")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("appDatPhong");

                    b.Navigation("appDichVus");

                    b.Navigation("appKhachHang");
                });

            modelBuilder.Entity("CafeWorkspaceBooking.Models.AppDatPhong", b =>
                {
                    b.HasOne("CafeWorkspaceBooking.Models.AppKhachHang", "appKhachHang")
                        .WithMany("appDatPhongs")
                        .HasForeignKey("IdKhachHang")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CafeWorkspaceBooking.Models.AppPhong", "appPhong")
                        .WithMany("appDatPhongs")
                        .HasForeignKey("IdPhong")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("appKhachHang");

                    b.Navigation("appPhong");
                });

            modelBuilder.Entity("CafeWorkspaceBooking.Models.AppDichVu", b =>
                {
                    b.HasOne("CafeWorkspaceBooking.Models.AppDmDichVu", "appDmDichVu")
                        .WithMany("appDichVus")
                        .HasForeignKey("IdDmDichVu")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("appDmDichVu");
                });

            modelBuilder.Entity("CafeWorkspaceBooking.Models.AppHoaDon", b =>
                {
                    b.HasOne("CafeWorkspaceBooking.Models.AppDatDV", "appDatDV")
                        .WithOne("appHoaDon")
                        .HasForeignKey("CafeWorkspaceBooking.Models.AppHoaDon", "IdDatDV");

                    b.HasOne("CafeWorkspaceBooking.Models.AppDatPhong", "appDatPhong")
                        .WithOne("appHoaDon")
                        .HasForeignKey("CafeWorkspaceBooking.Models.AppHoaDon", "IdDatPhong")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CafeWorkspaceBooking.Models.AppKhachHang", "appkhachHang")
                        .WithMany("appHoaDon")
                        .HasForeignKey("IdKhachHang")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CafeWorkspaceBooking.Models.AppNhanVien", "appNhanVien")
                        .WithMany("appHoaDons")
                        .HasForeignKey("IdNhanVien")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("appDatDV");

                    b.Navigation("appDatPhong");

                    b.Navigation("appNhanVien");

                    b.Navigation("appkhachHang");
                });

            modelBuilder.Entity("CafeWorkspaceBooking.Models.AppHuyDatDV", b =>
                {
                    b.HasOne("CafeWorkspaceBooking.Models.AppDatDV", "appDatDV")
                        .WithOne("appHuyDatDV")
                        .HasForeignKey("CafeWorkspaceBooking.Models.AppHuyDatDV", "IdDatDV")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CafeWorkspaceBooking.Models.AppThongBao", "appThongBao")
                        .WithMany()
                        .HasForeignKey("appThongBaoId");

                    b.Navigation("appDatDV");

                    b.Navigation("appThongBao");
                });

            modelBuilder.Entity("CafeWorkspaceBooking.Models.AppHuyDatPhong", b =>
                {
                    b.HasOne("CafeWorkspaceBooking.Models.AppDatPhong", "appDatPhong")
                        .WithOne("appHuyDatPhong")
                        .HasForeignKey("CafeWorkspaceBooking.Models.AppHuyDatPhong", "IdDatPhong")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CafeWorkspaceBooking.Models.AppKhachHang", "appKhachHang")
                        .WithMany("appHuyDatPhongs")
                        .HasForeignKey("IdKhachHang")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("appDatPhong");

                    b.Navigation("appKhachHang");
                });

            modelBuilder.Entity("CafeWorkspaceBooking.Models.AppImgPhong", b =>
                {
                    b.HasOne("CafeWorkspaceBooking.Models.AppPhong", "AppPhong")
                        .WithMany("appImgPhongs")
                        .HasForeignKey("IdPhong")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppPhong");
                });

            modelBuilder.Entity("CafeWorkspaceBooking.Models.AppNhanVien", b =>
                {
                    b.HasOne("CafeWorkspaceBooking.Models.AppKhachHang", "appKhachHang")
                        .WithOne("appNhanVien")
                        .HasForeignKey("CafeWorkspaceBooking.Models.AppNhanVien", "IdKhachHang");

                    b.Navigation("appKhachHang");
                });

            modelBuilder.Entity("CafeWorkspaceBooking.Models.AppThongBao", b =>
                {
                    b.HasOne("CafeWorkspaceBooking.Models.AppDanhGia", "appDanhGia")
                        .WithMany("appThongBao")
                        .HasForeignKey("IdDanhGia");

                    b.HasOne("CafeWorkspaceBooking.Models.AppDatPhong", "appDatPhong")
                        .WithMany("appThongBao")
                        .HasForeignKey("IdDatPhong")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("CafeWorkspaceBooking.Models.AppHuyDatPhong", "appHuyDatPhong")
                        .WithOne("appThongBao")
                        .HasForeignKey("CafeWorkspaceBooking.Models.AppThongBao", "IdHuyDatPhong")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("CafeWorkspaceBooking.Models.AppKhachHang", "appKhachHang")
                        .WithMany("appThongBao")
                        .HasForeignKey("IdKhachHang");

                    b.HasOne("CafeWorkspaceBooking.Models.AppPhong", "appPhong")
                        .WithMany("appThongBao")
                        .HasForeignKey("IdPhong")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("appDanhGia");

                    b.Navigation("appDatPhong");

                    b.Navigation("appHuyDatPhong");

                    b.Navigation("appKhachHang");

                    b.Navigation("appPhong");
                });

            modelBuilder.Entity("CafeWorkspaceBooking.Models.AppDanhGia", b =>
                {
                    b.Navigation("appThongBao");
                });

            modelBuilder.Entity("CafeWorkspaceBooking.Models.AppDatDV", b =>
                {
                    b.Navigation("appHoaDon");

                    b.Navigation("appHuyDatDV")
                        .IsRequired();
                });

            modelBuilder.Entity("CafeWorkspaceBooking.Models.AppDatPhong", b =>
                {
                    b.Navigation("appDatDV");

                    b.Navigation("appHoaDon")
                        .IsRequired();

                    b.Navigation("appHuyDatPhong")
                        .IsRequired();

                    b.Navigation("appThongBao");
                });

            modelBuilder.Entity("CafeWorkspaceBooking.Models.AppDichVu", b =>
                {
                    b.Navigation("appDatDVs");
                });

            modelBuilder.Entity("CafeWorkspaceBooking.Models.AppDmDichVu", b =>
                {
                    b.Navigation("appDichVus");
                });

            modelBuilder.Entity("CafeWorkspaceBooking.Models.AppHuyDatPhong", b =>
                {
                    b.Navigation("appThongBao");
                });

            modelBuilder.Entity("CafeWorkspaceBooking.Models.AppKhachHang", b =>
                {
                    b.Navigation("appDanhGias");

                    b.Navigation("appDatDVs");

                    b.Navigation("appDatPhongs");

                    b.Navigation("appHoaDon");

                    b.Navigation("appHuyDatPhongs");

                    b.Navigation("appNhanVien");

                    b.Navigation("appThongBao");
                });

            modelBuilder.Entity("CafeWorkspaceBooking.Models.AppNhanVien", b =>
                {
                    b.Navigation("appHoaDons");
                });

            modelBuilder.Entity("CafeWorkspaceBooking.Models.AppPhong", b =>
                {
                    b.Navigation("appDanhGias");

                    b.Navigation("appDatPhongs");

                    b.Navigation("appImgPhongs");

                    b.Navigation("appThongBao");
                });
#pragma warning restore 612, 618
        }
    }
}
