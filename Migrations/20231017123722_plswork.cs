using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project1.Migrations
{
    /// <inheritdoc />
    public partial class plswork : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Realestates_AspNetUsers_UserId",
                table: "Realestates");

            migrationBuilder.DropForeignKey(
                name: "FK_Rent_AspNetUsers_UserId",
                table: "Rent");

            migrationBuilder.AddForeignKey(
                name: "FK_Realestates_AspNetUsers_UserId",
                table: "Realestates",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Rent_AspNetUsers_UserId",
                table: "Rent",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Realestates_AspNetUsers_UserId",
                table: "Realestates");

            migrationBuilder.DropForeignKey(
                name: "FK_Rent_AspNetUsers_UserId",
                table: "Rent");

            migrationBuilder.AddForeignKey(
                name: "FK_Realestates_AspNetUsers_UserId",
                table: "Realestates",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rent_AspNetUsers_UserId",
                table: "Rent",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
