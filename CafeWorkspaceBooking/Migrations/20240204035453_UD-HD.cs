using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CafeWorkspaceBooking.Migrations
{
    /// <inheritdoc />
    public partial class UDHD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppHoaDon_AppKhachHangs_IdKhachHang",
                table: "AppHoaDon");

            migrationBuilder.DropForeignKey(
                name: "FK_AppHoaDon_AppNhanViens_IdNhanVien",
                table: "AppHoaDon");

            migrationBuilder.DropForeignKey(
                name: "FK_AppHoaDon_appDatPhongs_IdDatPhong",
                table: "AppHoaDon");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppHoaDon",
                table: "AppHoaDon");

            migrationBuilder.RenameTable(
                name: "AppHoaDon",
                newName: "AppHoaDons");

            migrationBuilder.RenameIndex(
                name: "IX_AppHoaDon_IdNhanVien",
                table: "AppHoaDons",
                newName: "IX_AppHoaDons_IdNhanVien");

            migrationBuilder.RenameIndex(
                name: "IX_AppHoaDon_IdKhachHang",
                table: "AppHoaDons",
                newName: "IX_AppHoaDons_IdKhachHang");

            migrationBuilder.RenameIndex(
                name: "IX_AppHoaDon_IdDatPhong",
                table: "AppHoaDons",
                newName: "IX_AppHoaDons_IdDatPhong");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppHoaDons",
                table: "AppHoaDons",
                column: "IdHoaDon");

            migrationBuilder.AddForeignKey(
                name: "FK_AppHoaDons_AppKhachHangs_IdKhachHang",
                table: "AppHoaDons",
                column: "IdKhachHang",
                principalTable: "AppKhachHangs",
                principalColumn: "IdKhachHang",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppHoaDons_AppNhanViens_IdNhanVien",
                table: "AppHoaDons",
                column: "IdNhanVien",
                principalTable: "AppNhanViens",
                principalColumn: "IdNhanVien",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppHoaDons_appDatPhongs_IdDatPhong",
                table: "AppHoaDons",
                column: "IdDatPhong",
                principalTable: "appDatPhongs",
                principalColumn: "IdDatPhong",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppHoaDons_AppKhachHangs_IdKhachHang",
                table: "AppHoaDons");

            migrationBuilder.DropForeignKey(
                name: "FK_AppHoaDons_AppNhanViens_IdNhanVien",
                table: "AppHoaDons");

            migrationBuilder.DropForeignKey(
                name: "FK_AppHoaDons_appDatPhongs_IdDatPhong",
                table: "AppHoaDons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppHoaDons",
                table: "AppHoaDons");

            migrationBuilder.RenameTable(
                name: "AppHoaDons",
                newName: "AppHoaDon");

            migrationBuilder.RenameIndex(
                name: "IX_AppHoaDons_IdNhanVien",
                table: "AppHoaDon",
                newName: "IX_AppHoaDon_IdNhanVien");

            migrationBuilder.RenameIndex(
                name: "IX_AppHoaDons_IdKhachHang",
                table: "AppHoaDon",
                newName: "IX_AppHoaDon_IdKhachHang");

            migrationBuilder.RenameIndex(
                name: "IX_AppHoaDons_IdDatPhong",
                table: "AppHoaDon",
                newName: "IX_AppHoaDon_IdDatPhong");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppHoaDon",
                table: "AppHoaDon",
                column: "IdHoaDon");

            migrationBuilder.AddForeignKey(
                name: "FK_AppHoaDon_AppKhachHangs_IdKhachHang",
                table: "AppHoaDon",
                column: "IdKhachHang",
                principalTable: "AppKhachHangs",
                principalColumn: "IdKhachHang",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppHoaDon_AppNhanViens_IdNhanVien",
                table: "AppHoaDon",
                column: "IdNhanVien",
                principalTable: "AppNhanViens",
                principalColumn: "IdNhanVien",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppHoaDon_appDatPhongs_IdDatPhong",
                table: "AppHoaDon",
                column: "IdDatPhong",
                principalTable: "appDatPhongs",
                principalColumn: "IdDatPhong",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
