using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Malv.Migrations
{
    public partial class Country_Ad : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DistrictId",
                table: "Ad",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MunicipalityId",
                table: "Ad",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Ad_DistrictId",
                table: "Ad",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_Ad_MunicipalityId",
                table: "Ad",
                column: "MunicipalityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ad_District_DistrictId",
                table: "Ad",
                column: "DistrictId",
                principalTable: "District",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ad_Municipality_MunicipalityId",
                table: "Ad",
                column: "MunicipalityId",
                principalTable: "Municipality",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ad_District_DistrictId",
                table: "Ad");

            migrationBuilder.DropForeignKey(
                name: "FK_Ad_Municipality_MunicipalityId",
                table: "Ad");

            migrationBuilder.DropIndex(
                name: "IX_Ad_DistrictId",
                table: "Ad");

            migrationBuilder.DropIndex(
                name: "IX_Ad_MunicipalityId",
                table: "Ad");

            migrationBuilder.DropColumn(
                name: "DistrictId",
                table: "Ad");

            migrationBuilder.DropColumn(
                name: "MunicipalityId",
                table: "Ad");
        }
    }
}
