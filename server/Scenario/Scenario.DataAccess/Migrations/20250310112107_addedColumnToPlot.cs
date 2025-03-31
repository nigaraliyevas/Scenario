using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Scenario.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addedColumnToPlot : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CommentedCount",
                table: "Plots",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsFavorite",
                table: "PlotAppUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommentedCount",
                table: "Plots");

            migrationBuilder.DropColumn(
                name: "IsFavorite",
                table: "PlotAppUsers");
        }
    }
}
