using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectGreenLens.Migrations
{
    /// <inheritdoc />
    public partial class GreenlensV21 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "plantId",
                table: "AIAdvicesLogs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AIAdvicesLogs_plantId",
                table: "AIAdvicesLogs",
                column: "plantId");

            migrationBuilder.AddForeignKey(
                name: "FK_AIAdvicesLogs_Plants_plantId",
                table: "AIAdvicesLogs",
                column: "plantId",
                principalTable: "Plants",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AIAdvicesLogs_Plants_plantId",
                table: "AIAdvicesLogs");

            migrationBuilder.DropIndex(
                name: "IX_AIAdvicesLogs_plantId",
                table: "AIAdvicesLogs");

            migrationBuilder.DropColumn(
                name: "plantId",
                table: "AIAdvicesLogs");
        }
    }
}
