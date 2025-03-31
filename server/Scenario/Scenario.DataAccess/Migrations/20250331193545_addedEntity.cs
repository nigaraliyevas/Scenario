using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Scenario.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addedEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlotAppUsers_AspNetUsers_AppUserId1",
                table: "PlotAppUsers");

            migrationBuilder.DropIndex(
                name: "IX_PlotAppUsers_AppUserId1",
                table: "PlotAppUsers");

            migrationBuilder.DropColumn(
                name: "AppUserId1",
                table: "PlotAppUsers");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "PlotAppUsers",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_PlotAppUsers_AppUserId",
                table: "PlotAppUsers",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlotAppUsers_AspNetUsers_AppUserId",
                table: "PlotAppUsers",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlotAppUsers_AspNetUsers_AppUserId",
                table: "PlotAppUsers");

            migrationBuilder.DropIndex(
                name: "IX_PlotAppUsers_AppUserId",
                table: "PlotAppUsers");

            migrationBuilder.AlterColumn<int>(
                name: "AppUserId",
                table: "PlotAppUsers",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId1",
                table: "PlotAppUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlotAppUsers_AppUserId1",
                table: "PlotAppUsers",
                column: "AppUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_PlotAppUsers_AspNetUsers_AppUserId1",
                table: "PlotAppUsers",
                column: "AppUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
