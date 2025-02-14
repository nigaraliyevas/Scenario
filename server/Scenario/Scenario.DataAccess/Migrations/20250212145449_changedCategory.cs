using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Scenario.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class changedCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Fullname",
                table: "Scriptwriters",
                newName: "Surname");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Scriptwriters",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Scriptwriters",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Scriptwriters",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PlotCount",
                table: "Scriptwriters",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Scriptwriters");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Scriptwriters");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Scriptwriters");

            migrationBuilder.DropColumn(
                name: "PlotCount",
                table: "Scriptwriters");

            migrationBuilder.RenameColumn(
                name: "Surname",
                table: "Scriptwriters",
                newName: "Fullname");
        }
    }
}
