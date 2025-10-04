using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectGreenLens.Migrations
{
    /// <inheritdoc />
    public partial class GreenlensV22 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "conversationType",
                table: "AIAdvicesLogs",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "conversationType",
                table: "AIAdvicesLogs");
        }
    }
}
