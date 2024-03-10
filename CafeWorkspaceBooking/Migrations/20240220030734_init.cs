using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CafeWorkspaceBooking.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "appDichVus",
                columns: table => new
                {
                    IdDichVu = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LoaiDV = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_appDichVus", x => x.IdDichVu);
                });

            migrationBuilder.CreateTable(
                name: "AppKhachHangs",
                columns: table => new
                {
                    IdKhachHang = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoTen = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    GioiTinh = table.Column<bool>(type: "bit", nullable: false),
                    NgaySinh = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CCCD = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SDT = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImgKH = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrangThaiTK = table.Column<bool>(type: "bit", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppKhachHangs", x => x.IdKhachHang);
                });

            migrationBuilder.CreateTable(
                name: "AppPhongs",
                columns: table => new
                {
                    IdPhong = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenPhong = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SucChua = table.Column<int>(type: "int", nullable: false),
                    Gia = table.Column<double>(type: "float", nullable: false),
                    Img = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppPhongs", x => x.IdPhong);
                });

            migrationBuilder.CreateTable(
                name: "AppNhanViens",
                columns: table => new
                {
                    IdNhanVien = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChucVu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayVaoLam = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Luong = table.Column<double>(type: "float", nullable: false),
                    IdKhachHang = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppNhanViens", x => x.IdNhanVien);
                    table.ForeignKey(
                        name: "FK_AppNhanViens_AppKhachHangs_IdKhachHang",
                        column: x => x.IdKhachHang,
                        principalTable: "AppKhachHangs",
                        principalColumn: "IdKhachHang");
                });

            migrationBuilder.CreateTable(
                name: "AppDanhGias",
                columns: table => new
                {
                    IdDanhGia = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CapDo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NhanXet = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TGDanhGia = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdKhachHang = table.Column<int>(type: "int", nullable: false),
                    IdPhong = table.Column<int>(type: "int", nullable: false),
                    IdDichVu = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppDanhGias", x => x.IdDanhGia);
                    table.ForeignKey(
                        name: "FK_AppDanhGias_AppKhachHangs_IdKhachHang",
                        column: x => x.IdKhachHang,
                        principalTable: "AppKhachHangs",
                        principalColumn: "IdKhachHang",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppDanhGias_AppPhongs_IdPhong",
                        column: x => x.IdPhong,
                        principalTable: "AppPhongs",
                        principalColumn: "IdPhong",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppDanhGias_appDichVus_IdDichVu",
                        column: x => x.IdDichVu,
                        principalTable: "appDichVus",
                        principalColumn: "IdDichVu",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "appDatPhongs",
                columns: table => new
                {
                    IdDatPhong = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TongTien = table.Column<double>(type: "float", nullable: false),
                    SLKhach = table.Column<int>(type: "int", nullable: false),
                    TGDatOnline = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TGBatDau = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TGKetThuc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TongThoiLuong = table.Column<double>(type: "float", nullable: false),
                    TTDatPhong = table.Column<int>(type: "int", nullable: false),
                    IdPhong = table.Column<int>(type: "int", nullable: false),
                    IdKhachHang = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_appDatPhongs", x => x.IdDatPhong);
                    table.ForeignKey(
                        name: "FK_appDatPhongs_AppKhachHangs_IdKhachHang",
                        column: x => x.IdKhachHang,
                        principalTable: "AppKhachHangs",
                        principalColumn: "IdKhachHang",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_appDatPhongs_AppPhongs_IdPhong",
                        column: x => x.IdPhong,
                        principalTable: "AppPhongs",
                        principalColumn: "IdPhong",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppDVPhongs",
                columns: table => new
                {
                    IdDVPhong = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPhong = table.Column<int>(type: "int", nullable: false),
                    IdDichVu = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppDVPhongs", x => x.IdDVPhong);
                    table.ForeignKey(
                        name: "FK_AppDVPhongs_AppPhongs_IdPhong",
                        column: x => x.IdPhong,
                        principalTable: "AppPhongs",
                        principalColumn: "IdPhong",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppDVPhongs_appDichVus_IdDichVu",
                        column: x => x.IdDichVu,
                        principalTable: "appDichVus",
                        principalColumn: "IdDichVu",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppImgPhongs",
                columns: table => new
                {
                    IdImgPhong = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdPhong = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppImgPhongs", x => x.IdImgPhong);
                    table.ForeignKey(
                        name: "FK_AppImgPhongs_AppPhongs_IdPhong",
                        column: x => x.IdPhong,
                        principalTable: "AppPhongs",
                        principalColumn: "IdPhong",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppHoaDons",
                columns: table => new
                {
                    IdHoaDon = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TongThanhToan = table.Column<double>(type: "float", nullable: false),
                    TGLapHD = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdDatPhong = table.Column<int>(type: "int", nullable: false),
                    IdNhanVien = table.Column<int>(type: "int", nullable: false),
                    IdKhachHang = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppHoaDons", x => x.IdHoaDon);
                    table.ForeignKey(
                        name: "FK_AppHoaDons_AppKhachHangs_IdKhachHang",
                        column: x => x.IdKhachHang,
                        principalTable: "AppKhachHangs",
                        principalColumn: "IdKhachHang",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppHoaDons_AppNhanViens_IdNhanVien",
                        column: x => x.IdNhanVien,
                        principalTable: "AppNhanViens",
                        principalColumn: "IdNhanVien",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppHoaDons_appDatPhongs_IdDatPhong",
                        column: x => x.IdDatPhong,
                        principalTable: "appDatPhongs",
                        principalColumn: "IdDatPhong",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppHuyDatPhongs",
                columns: table => new
                {
                    IdHuyDatPhong = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TGHuy = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LiDoHuy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdKhachHang = table.Column<int>(type: "int", nullable: false),
                    IdDatPhong = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppHuyDatPhongs", x => x.IdHuyDatPhong);
                    table.ForeignKey(
                        name: "FK_AppHuyDatPhongs_AppKhachHangs_IdKhachHang",
                        column: x => x.IdKhachHang,
                        principalTable: "AppKhachHangs",
                        principalColumn: "IdKhachHang",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppHuyDatPhongs_appDatPhongs_IdDatPhong",
                        column: x => x.IdDatPhong,
                        principalTable: "appDatPhongs",
                        principalColumn: "IdDatPhong",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppThongBaos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenThongBao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NDThongbao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdPhong = table.Column<int>(type: "int", nullable: false),
                    IdDatPhong = table.Column<int>(type: "int", nullable: false),
                    IdHuyDatPhong = table.Column<int>(type: "int", nullable: false),
                    IdKhachHang = table.Column<int>(type: "int", nullable: false),
                    IdDanhGia = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppThongBaos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppThongBaos_AppDanhGias_IdDanhGia",
                        column: x => x.IdDanhGia,
                        principalTable: "AppDanhGias",
                        principalColumn: "IdDanhGia",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppThongBaos_AppHuyDatPhongs_IdHuyDatPhong",
                        column: x => x.IdHuyDatPhong,
                        principalTable: "AppHuyDatPhongs",
                        principalColumn: "IdHuyDatPhong",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppThongBaos_AppKhachHangs_IdKhachHang",
                        column: x => x.IdKhachHang,
                        principalTable: "AppKhachHangs",
                        principalColumn: "IdKhachHang",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppThongBaos_AppPhongs_IdPhong",
                        column: x => x.IdPhong,
                        principalTable: "AppPhongs",
                        principalColumn: "IdPhong");
                    table.ForeignKey(
                        name: "FK_AppThongBaos_appDatPhongs_IdDatPhong",
                        column: x => x.IdDatPhong,
                        principalTable: "appDatPhongs",
                        principalColumn: "IdDatPhong");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppDanhGias_IdDichVu",
                table: "AppDanhGias",
                column: "IdDichVu");

            migrationBuilder.CreateIndex(
                name: "IX_AppDanhGias_IdKhachHang",
                table: "AppDanhGias",
                column: "IdKhachHang");

            migrationBuilder.CreateIndex(
                name: "IX_AppDanhGias_IdPhong",
                table: "AppDanhGias",
                column: "IdPhong");

            migrationBuilder.CreateIndex(
                name: "IX_appDatPhongs_IdKhachHang",
                table: "appDatPhongs",
                column: "IdKhachHang");

            migrationBuilder.CreateIndex(
                name: "IX_appDatPhongs_IdPhong",
                table: "appDatPhongs",
                column: "IdPhong");

            migrationBuilder.CreateIndex(
                name: "IX_AppDVPhongs_IdDichVu",
                table: "AppDVPhongs",
                column: "IdDichVu");

            migrationBuilder.CreateIndex(
                name: "IX_AppDVPhongs_IdPhong",
                table: "AppDVPhongs",
                column: "IdPhong");

            migrationBuilder.CreateIndex(
                name: "IX_AppHoaDons_IdDatPhong",
                table: "AppHoaDons",
                column: "IdDatPhong",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppHoaDons_IdKhachHang",
                table: "AppHoaDons",
                column: "IdKhachHang");

            migrationBuilder.CreateIndex(
                name: "IX_AppHoaDons_IdNhanVien",
                table: "AppHoaDons",
                column: "IdNhanVien");

            migrationBuilder.CreateIndex(
                name: "IX_AppHuyDatPhongs_IdDatPhong",
                table: "AppHuyDatPhongs",
                column: "IdDatPhong",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppHuyDatPhongs_IdKhachHang",
                table: "AppHuyDatPhongs",
                column: "IdKhachHang");

            migrationBuilder.CreateIndex(
                name: "IX_AppImgPhongs_IdPhong",
                table: "AppImgPhongs",
                column: "IdPhong");

            migrationBuilder.CreateIndex(
                name: "IX_AppNhanViens_IdKhachHang",
                table: "AppNhanViens",
                column: "IdKhachHang",
                unique: true,
                filter: "[IdKhachHang] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AppThongBaos_IdDanhGia",
                table: "AppThongBaos",
                column: "IdDanhGia",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppThongBaos_IdDatPhong",
                table: "AppThongBaos",
                column: "IdDatPhong",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppThongBaos_IdHuyDatPhong",
                table: "AppThongBaos",
                column: "IdHuyDatPhong",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppThongBaos_IdKhachHang",
                table: "AppThongBaos",
                column: "IdKhachHang",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppThongBaos_IdPhong",
                table: "AppThongBaos",
                column: "IdPhong",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppDVPhongs");

            migrationBuilder.DropTable(
                name: "AppHoaDons");

            migrationBuilder.DropTable(
                name: "AppImgPhongs");

            migrationBuilder.DropTable(
                name: "AppThongBaos");

            migrationBuilder.DropTable(
                name: "AppNhanViens");

            migrationBuilder.DropTable(
                name: "AppDanhGias");

            migrationBuilder.DropTable(
                name: "AppHuyDatPhongs");

            migrationBuilder.DropTable(
                name: "appDichVus");

            migrationBuilder.DropTable(
                name: "appDatPhongs");

            migrationBuilder.DropTable(
                name: "AppKhachHangs");

            migrationBuilder.DropTable(
                name: "AppPhongs");
        }
    }
}
