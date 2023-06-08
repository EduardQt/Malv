using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace Malv.Migrations
{
    public partial class DropDistrict : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ad_District_DistrictId",
                table: "Ad");

            migrationBuilder.DropForeignKey(
                name: "FK_Municipality_District_DistrictId",
                table: "Municipality");

            migrationBuilder.DropTable(
                name: "District");

            migrationBuilder.DropIndex(
                name: "IX_Ad_DistrictId",
                table: "Ad");

            migrationBuilder.DropColumn(
                name: "DistrictId",
                table: "Ad");

            migrationBuilder.RenameColumn(
                name: "DistrictId",
                table: "Municipality",
                newName: "CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_Municipality_DistrictId",
                table: "Municipality",
                newName: "IX_Municipality_CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Municipality_Country_CountryId",
                table: "Municipality",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Municipality_Country_CountryId",
                table: "Municipality");

            migrationBuilder.RenameColumn(
                name: "CountryId",
                table: "Municipality",
                newName: "DistrictId");

            migrationBuilder.RenameIndex(
                name: "IX_Municipality_CountryId",
                table: "Municipality",
                newName: "IX_Municipality_DistrictId");

            migrationBuilder.AddColumn<int>(
                name: "DistrictId",
                table: "Ad",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "District",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_District", x => x.Id);
                    table.ForeignKey(
                        name: "FK_District_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Ad_DistrictId",
                table: "Ad",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_District_CountryId",
                table: "District",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ad_District_DistrictId",
                table: "Ad",
                column: "DistrictId",
                principalTable: "District",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Municipality_District_DistrictId",
                table: "Municipality",
                column: "DistrictId",
                principalTable: "District",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
