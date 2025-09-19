using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProjectGreenLens.Migrations
{
    /// <inheritdoc />
    public partial class GreenlensV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PermissionQuotas",
                columns: table => new
                {
                    roleId = table.Column<int>(type: "int", nullable: false),
                    permissionId = table.Column<int>(type: "int", nullable: false),
                    usageLimit = table.Column<int>(type: "int", nullable: false),
                    RolePermissionpermissionId = table.Column<int>(type: "int", nullable: true),
                    RolePermissionroleId = table.Column<int>(type: "int", nullable: true),
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
                    table.PrimaryKey("PK_PermissionQuotas", x => new { x.roleId, x.permissionId });
                    table.ForeignKey(
                        name: "FK_PermissionQuotas_Permissions_permissionId",
                        column: x => x.permissionId,
                        principalTable: "Permissions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PermissionQuotas_RolePermissions_RolePermissionroleId_RolePermissionpermissionId",
                        columns: x => new { x.RolePermissionroleId, x.RolePermissionpermissionId },
                        principalTable: "RolePermissions",
                        principalColumns: new[] { "roleId", "permissionId" });
                    table.ForeignKey(
                        name: "FK_PermissionQuotas_Roles_roleId",
                        column: x => x.roleId,
                        principalTable: "Roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserPermissionUsages",
                columns: table => new
                {
                    userId = table.Column<int>(type: "int", nullable: false),
                    permissionId = table.Column<int>(type: "int", nullable: false),
                    usedCount = table.Column<int>(type: "int", nullable: false),
                    lastUsedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    table.PrimaryKey("PK_UserPermissionUsages", x => new { x.userId, x.permissionId });
                    table.ForeignKey(
                        name: "FK_UserPermissionUsages_Permissions_permissionId",
                        column: x => x.permissionId,
                        principalTable: "Permissions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserPermissionUsages_Users_userId",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "id",
                keyValue: 1,
                column: "name",
                value: "Quản Trị Viên");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "id",
                keyValue: 2,
                column: "name",
                value: "Người Dùng");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "id",
                keyValue: 3,
                column: "name",
                value: "Vườn Ươm");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "id", "createdAt", "deletedAt", "description", "name", "uniqueGuid", "updatedAt" },
                values: new object[,]
                {
                    { 4, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Entry-level user with basic feature access", "Người Dùng Bạc", new Guid("44444444-4444-4444-4444-444444444444"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 5, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Intermediate user with extended feature access", "Người Dùng Vàng", new Guid("55555555-5555-5555-5555-555555555555"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "id", "createdAt", "deletedAt", "description", "name", "updatedAt" },
                values: new object[] { 6, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Advanced user with premium feature access", "Người Dùng Kim Cương", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.CreateIndex(
                name: "IX_PermissionQuotas_CreatedAt",
                table: "PermissionQuotas",
                column: "createdAt");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionQuotas_permissionId",
                table: "PermissionQuotas",
                column: "permissionId");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionQuotas_RolePermissionroleId_RolePermissionpermissionId",
                table: "PermissionQuotas",
                columns: new[] { "RolePermissionroleId", "RolePermissionpermissionId" });

            migrationBuilder.CreateIndex(
                name: "IX_UserPermissionUsages_CreatedAt",
                table: "UserPermissionUsages",
                column: "createdAt");

            migrationBuilder.CreateIndex(
                name: "IX_UserPermissionUsages_permissionId",
                table: "UserPermissionUsages",
                column: "permissionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PermissionQuotas");

            migrationBuilder.DropTable(
                name: "UserPermissionUsages");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "id",
                keyValue: 6);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "id",
                keyValue: 1,
                column: "name",
                value: "admin");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "id",
                keyValue: 2,
                column: "name",
                value: "user");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "id",
                keyValue: 3,
                column: "name",
                value: "nursery");
        }
    }
}
