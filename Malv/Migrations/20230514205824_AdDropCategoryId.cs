using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Malv.Migrations
{
    public partial class AdDropCategoryId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ad_Category_CategoryId",
                table: "Ad");

            migrationBuilder.DropIndex(
                name: "IX_Ad_CategoryId",
                table: "Ad");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Ad");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Ad",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Ad_CategoryId",
                table: "Ad",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ad_Category_CategoryId",
                table: "Ad",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
