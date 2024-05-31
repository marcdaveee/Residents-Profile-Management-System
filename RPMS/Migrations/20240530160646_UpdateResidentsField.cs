using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RPMS.Migrations
{
    /// <inheritdoc />
    public partial class UpdateResidentsField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Residents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Residents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Residents");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Residents");
        }
    }
}
