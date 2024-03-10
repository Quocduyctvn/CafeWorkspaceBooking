using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CafeWorkspaceBooking.Migrations
{
    /// <inheritdoc />
    public partial class addCL_Checked : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CheckAll",
                table: "AppThongBaos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Checked",
                table: "AppThongBaos",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CheckAll",
                table: "AppThongBaos");

            migrationBuilder.DropColumn(
                name: "Checked",
                table: "AppThongBaos");
        }
    }
}
