using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CafeWorkspaceBooking.Migrations
{
    /// <inheritdoc />
    public partial class version31 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Size",
                table: "appDichVus");

            migrationBuilder.AddColumn<int>(
                name: "Size",
                table: "AppDatDVs",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Size",
                table: "AppDatDVs");

            migrationBuilder.AddColumn<int>(
                name: "Size",
                table: "appDichVus",
                type: "int",
                nullable: true);
        }
    }
}
