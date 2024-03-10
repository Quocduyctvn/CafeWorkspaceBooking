using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CafeWorkspaceBooking.Migrations
{
    /// <inheritdoc />
    public partial class Add_relationship_1NThongbao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AppThongBaos_IdDanhGia",
                table: "AppThongBaos");

            migrationBuilder.DropIndex(
                name: "IX_AppThongBaos_IdDatPhong",
                table: "AppThongBaos");

            migrationBuilder.DropIndex(
                name: "IX_AppThongBaos_IdKhachHang",
                table: "AppThongBaos");

            migrationBuilder.DropIndex(
                name: "IX_AppThongBaos_IdPhong",
                table: "AppThongBaos");

            migrationBuilder.CreateIndex(
                name: "IX_AppThongBaos_IdDanhGia",
                table: "AppThongBaos",
                column: "IdDanhGia");

            migrationBuilder.CreateIndex(
                name: "IX_AppThongBaos_IdDatPhong",
                table: "AppThongBaos",
                column: "IdDatPhong");

            migrationBuilder.CreateIndex(
                name: "IX_AppThongBaos_IdKhachHang",
                table: "AppThongBaos",
                column: "IdKhachHang");

            migrationBuilder.CreateIndex(
                name: "IX_AppThongBaos_IdPhong",
                table: "AppThongBaos",
                column: "IdPhong");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AppThongBaos_IdDanhGia",
                table: "AppThongBaos");

            migrationBuilder.DropIndex(
                name: "IX_AppThongBaos_IdDatPhong",
                table: "AppThongBaos");

            migrationBuilder.DropIndex(
                name: "IX_AppThongBaos_IdKhachHang",
                table: "AppThongBaos");

            migrationBuilder.DropIndex(
                name: "IX_AppThongBaos_IdPhong",
                table: "AppThongBaos");

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
        }
    }
}
