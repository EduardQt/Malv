using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Malv.Migrations
{
    public partial class MoreCarData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AllWheelDrive",
                table: "CarAd",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "HorsePower",
                table: "CarAd",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AllWheelDrive",
                table: "CarAd");

            migrationBuilder.DropColumn(
                name: "HorsePower",
                table: "CarAd");
        }
    }
}
