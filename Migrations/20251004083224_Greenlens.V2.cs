using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectGreenLens.Migrations
{
    /// <inheritdoc />
    public partial class GreenlensV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "lightRequirement",
                table: "Plants",
                newName: "LightRequirement");

            migrationBuilder.CreateTable(
                name: "GuestAIAdvicesLogs",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GuestToken = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserPlantId = table.Column<int>(type: "int", nullable: true),
                    uniqueGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    isDelete = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    deletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuestAIAdvicesLogs", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "GuestQuotas",
                columns: table => new
                {
                    GuestToken = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UsedCount = table.Column<int>(type: "int", nullable: false),
                    LastUsedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    uniqueGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    isDelete = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    deletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuestQuotas", x => x.GuestToken);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GuestAIAdvicesLog_GuestToken",
                table: "GuestAIAdvicesLogs",
                column: "GuestToken");

            migrationBuilder.CreateIndex(
                name: "IX_GuestAIAdvicesLogs_CreatedAt",
                table: "GuestAIAdvicesLogs",
                column: "createdAt");

            migrationBuilder.CreateIndex(
                name: "IX_GuestQuotas_CreatedAt",
                table: "GuestQuotas",
                column: "createdAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GuestAIAdvicesLogs");

            migrationBuilder.DropTable(
                name: "GuestQuotas");

            migrationBuilder.RenameColumn(
                name: "LightRequirement",
                table: "Plants",
                newName: "lightRequirement");
        }
    }
}
