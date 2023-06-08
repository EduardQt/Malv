using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Malv.Migrations
{
    public partial class ChatMessage_SentDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "SentDate",
                table: "ChatMessage",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessage_UserId",
                table: "ChatMessage",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMessage_User_UserId",
                table: "ChatMessage",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatMessage_User_UserId",
                table: "ChatMessage");

            migrationBuilder.DropIndex(
                name: "IX_ChatMessage_UserId",
                table: "ChatMessage");

            migrationBuilder.DropColumn(
                name: "SentDate",
                table: "ChatMessage");
        }
    }
}
