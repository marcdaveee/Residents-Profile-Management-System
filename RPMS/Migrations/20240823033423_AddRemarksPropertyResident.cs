using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RPMS.Migrations
{
    /// <inheritdoc />
    public partial class AddRemarksPropertyResident : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "Residents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "Residents");
        }
    }
}
