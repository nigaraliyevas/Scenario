using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Scenario.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class newEntitiesAddedHello : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlotRatings_AspNetUsers_UserId",
                table: "PlotRatings");

            migrationBuilder.DropForeignKey(
                name: "FK_UserScenarioFavorite_AspNetUsers_UserId1",
                table: "UserScenarioFavorite");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserScenarioFavorite");

            migrationBuilder.RenameColumn(
                name: "UserId1",
                table: "UserScenarioFavorite",
                newName: "AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserScenarioFavorite_UserId1",
                table: "UserScenarioFavorite",
                newName: "IX_UserScenarioFavorite_AppUserId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "PlotRatings",
                newName: "AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_PlotRatings_UserId",
                table: "PlotRatings",
                newName: "IX_PlotRatings_AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlotRatings_AspNetUsers_AppUserId",
                table: "PlotRatings",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserScenarioFavorite_AspNetUsers_AppUserId",
                table: "UserScenarioFavorite",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlotRatings_AspNetUsers_AppUserId",
                table: "PlotRatings");

            migrationBuilder.DropForeignKey(
                name: "FK_UserScenarioFavorite_AspNetUsers_AppUserId",
                table: "UserScenarioFavorite");

            migrationBuilder.RenameColumn(
                name: "AppUserId",
                table: "UserScenarioFavorite",
                newName: "UserId1");

            migrationBuilder.RenameIndex(
                name: "IX_UserScenarioFavorite_AppUserId",
                table: "UserScenarioFavorite",
                newName: "IX_UserScenarioFavorite_UserId1");

            migrationBuilder.RenameColumn(
                name: "AppUserId",
                table: "PlotRatings",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_PlotRatings_AppUserId",
                table: "PlotRatings",
                newName: "IX_PlotRatings_UserId");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "UserScenarioFavorite",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "FK_PlotRatings_AspNetUsers_UserId",
                table: "PlotRatings",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserScenarioFavorite_AspNetUsers_UserId1",
                table: "UserScenarioFavorite",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
