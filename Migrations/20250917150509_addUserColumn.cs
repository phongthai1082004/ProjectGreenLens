using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectGreenLens.Migrations
{
    /// <inheritdoc />
    public partial class addUserColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isEmailVerified",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isEmailVerified",
                table: "Users");
        }
    }
}
