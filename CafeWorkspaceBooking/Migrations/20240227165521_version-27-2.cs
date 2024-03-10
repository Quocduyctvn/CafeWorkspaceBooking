using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CafeWorkspaceBooking.Migrations
{
    /// <inheritdoc />
    public partial class version272 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppDanhGias_appDichVus_IdDichVu",
                table: "AppDanhGias");

            migrationBuilder.DropTable(
                name: "AppDVPhongs");

            migrationBuilder.DropIndex(
                name: "IX_AppDanhGias_IdDichVu",
                table: "AppDanhGias");

            migrationBuilder.DropColumn(
                name: "LoaiDV",
                table: "appDichVus");

            migrationBuilder.AddColumn<int>(
                name: "IdDatDV",
                table: "AppHoaDons",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdDmDichVu",
                table: "appDichVus",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AppDatDVs",
                columns: table => new
                {
                    IdDatDV = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdDichVu = table.Column<int>(type: "int", nullable: false),
                    IdKhachHang = table.Column<int>(type: "int", nullable: false),
                    IdDatPhong = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppDatDVs", x => x.IdDatDV);
                    table.ForeignKey(
                        name: "FK_AppDatDVs_AppKhachHangs_IdKhachHang",
                        column: x => x.IdKhachHang,
                        principalTable: "AppKhachHangs",
                        principalColumn: "IdKhachHang",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppDatDVs_appDatPhongs_IdDatPhong",
                        column: x => x.IdDatPhong,
                        principalTable: "appDatPhongs",
                        principalColumn: "IdDatPhong",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppDatDVs_appDichVus_IdDichVu",
                        column: x => x.IdDichVu,
                        principalTable: "appDichVus",
                        principalColumn: "IdDichVu",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppDmDichVus",
                columns: table => new
                {
                    IdDmDichVu = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDmDv = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppDmDichVus", x => x.IdDmDichVu);
                });

            migrationBuilder.CreateTable(
                name: "AppHuyDatDV",
                columns: table => new
                {
                    IdHuyDatDV = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TGHuy = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LiDoHuy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdDatDV = table.Column<int>(type: "int", nullable: false),
                    appThongBaoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppHuyDatDV", x => x.IdHuyDatDV);
                    table.ForeignKey(
                        name: "FK_AppHuyDatDV_AppDatDVs_IdDatDV",
                        column: x => x.IdDatDV,
                        principalTable: "AppDatDVs",
                        principalColumn: "IdDatDV",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppHuyDatDV_AppThongBaos_appThongBaoId",
                        column: x => x.appThongBaoId,
                        principalTable: "AppThongBaos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppHoaDons_IdDatDV",
                table: "AppHoaDons",
                column: "IdDatDV",
                unique: true,
                filter: "[IdDatDV] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_appDichVus_IdDmDichVu",
                table: "appDichVus",
                column: "IdDmDichVu");

            migrationBuilder.CreateIndex(
                name: "IX_AppDatDVs_IdDatPhong",
                table: "AppDatDVs",
                column: "IdDatPhong");

            migrationBuilder.CreateIndex(
                name: "IX_AppDatDVs_IdDichVu",
                table: "AppDatDVs",
                column: "IdDichVu");

            migrationBuilder.CreateIndex(
                name: "IX_AppDatDVs_IdKhachHang",
                table: "AppDatDVs",
                column: "IdKhachHang");

            migrationBuilder.CreateIndex(
                name: "IX_AppHuyDatDV_appThongBaoId",
                table: "AppHuyDatDV",
                column: "appThongBaoId");

            migrationBuilder.CreateIndex(
                name: "IX_AppHuyDatDV_IdDatDV",
                table: "AppHuyDatDV",
                column: "IdDatDV",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_appDichVus_AppDmDichVus_IdDmDichVu",
                table: "appDichVus",
                column: "IdDmDichVu",
                principalTable: "AppDmDichVus",
                principalColumn: "IdDmDichVu",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppHoaDons_AppDatDVs_IdDatDV",
                table: "AppHoaDons",
                column: "IdDatDV",
                principalTable: "AppDatDVs",
                principalColumn: "IdDatDV");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_appDichVus_AppDmDichVus_IdDmDichVu",
                table: "appDichVus");

            migrationBuilder.DropForeignKey(
                name: "FK_AppHoaDons_AppDatDVs_IdDatDV",
                table: "AppHoaDons");

            migrationBuilder.DropTable(
                name: "AppDmDichVus");

            migrationBuilder.DropTable(
                name: "AppHuyDatDV");

            migrationBuilder.DropTable(
                name: "AppDatDVs");

            migrationBuilder.DropIndex(
                name: "IX_AppHoaDons_IdDatDV",
                table: "AppHoaDons");

            migrationBuilder.DropIndex(
                name: "IX_appDichVus_IdDmDichVu",
                table: "appDichVus");

            migrationBuilder.DropColumn(
                name: "IdDatDV",
                table: "AppHoaDons");

            migrationBuilder.DropColumn(
                name: "IdDmDichVu",
                table: "appDichVus");

            migrationBuilder.AddColumn<string>(
                name: "LoaiDV",
                table: "appDichVus",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "AppDVPhongs",
                columns: table => new
                {
                    IdDVPhong = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdDichVu = table.Column<int>(type: "int", nullable: false),
                    IdPhong = table.Column<int>(type: "int", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_AppDanhGias_IdDichVu",
                table: "AppDanhGias",
                column: "IdDichVu");

            migrationBuilder.CreateIndex(
                name: "IX_AppDVPhongs_IdDichVu",
                table: "AppDVPhongs",
                column: "IdDichVu");

            migrationBuilder.CreateIndex(
                name: "IX_AppDVPhongs_IdPhong",
                table: "AppDVPhongs",
                column: "IdPhong");

            migrationBuilder.AddForeignKey(
                name: "FK_AppDanhGias_appDichVus_IdDichVu",
                table: "AppDanhGias",
                column: "IdDichVu",
                principalTable: "appDichVus",
                principalColumn: "IdDichVu",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
