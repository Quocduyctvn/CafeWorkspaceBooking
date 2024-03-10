using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CafeWorkspaceBooking.Migrations
{
    /// <inheritdoc />
    public partial class addCL_TgianThongbao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppThongBaos_AppDanhGias_IdDanhGia",
                table: "AppThongBaos");

            migrationBuilder.DropForeignKey(
                name: "FK_AppThongBaos_AppKhachHangs_IdKhachHang",
                table: "AppThongBaos");

            migrationBuilder.DropIndex(
                name: "IX_AppThongBaos_IdDanhGia",
                table: "AppThongBaos");

            migrationBuilder.DropIndex(
                name: "IX_AppThongBaos_IdDatPhong",
                table: "AppThongBaos");

            migrationBuilder.DropIndex(
                name: "IX_AppThongBaos_IdHuyDatPhong",
                table: "AppThongBaos");

            migrationBuilder.DropIndex(
                name: "IX_AppThongBaos_IdKhachHang",
                table: "AppThongBaos");

            migrationBuilder.DropIndex(
                name: "IX_AppThongBaos_IdPhong",
                table: "AppThongBaos");

            migrationBuilder.AlterColumn<int>(
                name: "IdPhong",
                table: "AppThongBaos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "IdKhachHang",
                table: "AppThongBaos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "IdHuyDatPhong",
                table: "AppThongBaos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "IdDatPhong",
                table: "AppThongBaos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "IdDanhGia",
                table: "AppThongBaos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateAt",
                table: "AppThongBaos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_AppThongBaos_IdDanhGia",
                table: "AppThongBaos",
                column: "IdDanhGia",
                unique: true,
                filter: "[IdDanhGia] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AppThongBaos_IdDatPhong",
                table: "AppThongBaos",
                column: "IdDatPhong",
                unique: true,
                filter: "[IdDatPhong] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AppThongBaos_IdHuyDatPhong",
                table: "AppThongBaos",
                column: "IdHuyDatPhong",
                unique: true,
                filter: "[IdHuyDatPhong] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AppThongBaos_IdKhachHang",
                table: "AppThongBaos",
                column: "IdKhachHang",
                unique: true,
                filter: "[IdKhachHang] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AppThongBaos_IdPhong",
                table: "AppThongBaos",
                column: "IdPhong",
                unique: true,
                filter: "[IdPhong] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AppThongBaos_AppDanhGias_IdDanhGia",
                table: "AppThongBaos",
                column: "IdDanhGia",
                principalTable: "AppDanhGias",
                principalColumn: "IdDanhGia");

            migrationBuilder.AddForeignKey(
                name: "FK_AppThongBaos_AppKhachHangs_IdKhachHang",
                table: "AppThongBaos",
                column: "IdKhachHang",
                principalTable: "AppKhachHangs",
                principalColumn: "IdKhachHang");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppThongBaos_AppDanhGias_IdDanhGia",
                table: "AppThongBaos");

            migrationBuilder.DropForeignKey(
                name: "FK_AppThongBaos_AppKhachHangs_IdKhachHang",
                table: "AppThongBaos");

            migrationBuilder.DropIndex(
                name: "IX_AppThongBaos_IdDanhGia",
                table: "AppThongBaos");

            migrationBuilder.DropIndex(
                name: "IX_AppThongBaos_IdDatPhong",
                table: "AppThongBaos");

            migrationBuilder.DropIndex(
                name: "IX_AppThongBaos_IdHuyDatPhong",
                table: "AppThongBaos");

            migrationBuilder.DropIndex(
                name: "IX_AppThongBaos_IdKhachHang",
                table: "AppThongBaos");

            migrationBuilder.DropIndex(
                name: "IX_AppThongBaos_IdPhong",
                table: "AppThongBaos");

            migrationBuilder.DropColumn(
                name: "CreateAt",
                table: "AppThongBaos");

            migrationBuilder.AlterColumn<int>(
                name: "IdPhong",
                table: "AppThongBaos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IdKhachHang",
                table: "AppThongBaos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IdHuyDatPhong",
                table: "AppThongBaos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IdDatPhong",
                table: "AppThongBaos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IdDanhGia",
                table: "AppThongBaos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_AppThongBaos_AppDanhGias_IdDanhGia",
                table: "AppThongBaos",
                column: "IdDanhGia",
                principalTable: "AppDanhGias",
                principalColumn: "IdDanhGia",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppThongBaos_AppKhachHangs_IdKhachHang",
                table: "AppThongBaos",
                column: "IdKhachHang",
                principalTable: "AppKhachHangs",
                principalColumn: "IdKhachHang",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
