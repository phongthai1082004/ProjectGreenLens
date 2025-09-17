using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProjectGreenLens.Migrations
{
    /// <inheritdoc />
    public partial class GreenLensV1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    uniqueGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    isDelete = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    deletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "PlantCategories",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    uniqueGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    isDelete = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    deletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlantCategories", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "PlantDiseases",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    symptoms = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    treatment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    prevention = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    uniqueGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    isDelete = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    deletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlantDiseases", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    uniqueGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    isDelete = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    deletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Plants",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    scientificName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    commonName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    careInstructions = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    plantCategoryId = table.Column<int>(type: "int", nullable: true),
                    isIndoor = table.Column<bool>(type: "bit", nullable: false),
                    wateringFrequency = table.Column<int>(type: "int", maxLength: 100, nullable: false),
                    lightRequirement = table.Column<int>(type: "int", maxLength: 100, nullable: false),
                    soilType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    averagePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    uniqueGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    isDelete = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    deletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plants", x => x.id);
                    table.ForeignKey(
                        name: "FK_Plants_PlantCategories_plantCategoryId",
                        column: x => x.plantCategoryId,
                        principalTable: "PlantCategories",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "RolePermissions",
                columns: table => new
                {
                    roleId = table.Column<int>(type: "int", nullable: false),
                    permissionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissions", x => new { x.roleId, x.permissionId });
                    table.ForeignKey(
                        name: "FK_RolePermissions_Permissions_permissionId",
                        column: x => x.permissionId,
                        principalTable: "Permissions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermissions_Roles_roleId",
                        column: x => x.roleId,
                        principalTable: "Roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    passwordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    roleId = table.Column<int>(type: "int", nullable: false),
                    uniqueGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    isDelete = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    deletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_roleId",
                        column: x => x.roleId,
                        principalTable: "Roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArModels",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    plantId = table.Column<int>(type: "int", nullable: false),
                    modelUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    fileFormat = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    uniqueGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    isDelete = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    deletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArModels", x => x.id);
                    table.ForeignKey(
                        name: "FK_ArModels_Plants_plantId",
                        column: x => x.plantId,
                        principalTable: "Plants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Guides",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    plantId = table.Column<int>(type: "int", nullable: false),
                    title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    content = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: true),
                    uniqueGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    isDelete = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    deletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guides", x => x.id);
                    table.ForeignKey(
                        name: "FK_Guides_Plants_plantId",
                        column: x => x.plantId,
                        principalTable: "Plants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    plantId = table.Column<int>(type: "int", nullable: false),
                    photoUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    caption = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    uniqueGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    isDelete = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    deletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.id);
                    table.ForeignKey(
                        name: "FK_Photos_Plants_plantId",
                        column: x => x.plantId,
                        principalTable: "Plants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LogEntries",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(type: "int", nullable: false),
                    action = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    metadata = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    uniqueGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    isDelete = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    deletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogEntries", x => x.id);
                    table.ForeignKey(
                        name: "FK_LogEntries_Users_userId",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NurseryProfiles",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(type: "int", nullable: false),
                    nurseryName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    contactNumber = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    uniqueGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    isDelete = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    deletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NurseryProfiles", x => x.id);
                    table.ForeignKey(
                        name: "FK_NurseryProfiles_Users_userId",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(type: "int", nullable: false),
                    amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    currency = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    paymentMethod = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    transactionId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    processedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    uniqueGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    isDelete = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    deletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.id);
                    table.ForeignKey(
                        name: "FK_Payments_Users_userId",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserMessages",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    senderId = table.Column<int>(type: "int", nullable: false),
                    receiverId = table.Column<int>(type: "int", nullable: false),
                    messageText = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    isRead = table.Column<bool>(type: "bit", nullable: false),
                    uniqueGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    isDelete = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    deletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMessages", x => x.id);
                    table.ForeignKey(
                        name: "FK_UserMessages_Users_receiverId",
                        column: x => x.receiverId,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserMessages_Users_senderId",
                        column: x => x.senderId,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserPlants",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(type: "int", nullable: false),
                    plantId = table.Column<int>(type: "int", nullable: false),
                    nickname = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    acquiredDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    notes = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    healthStatus = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    currentLocation = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    uniqueGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    isDelete = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    deletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPlants", x => x.id);
                    table.ForeignKey(
                        name: "FK_UserPlants_Plants_plantId",
                        column: x => x.plantId,
                        principalTable: "Plants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserPlants_Users_userId",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserProfiles",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(type: "int", nullable: false),
                    fullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    avatarUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    bio = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    location = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    uniqueGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    isDelete = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    deletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfiles", x => x.id);
                    table.ForeignKey(
                        name: "FK_UserProfiles_Users_userId",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    expiresAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    isRevoked = table.Column<bool>(type: "bit", nullable: false),
                    type = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    userId = table.Column<int>(type: "int", nullable: false),
                    uniqueGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    isDelete = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    deletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => x.id);
                    table.ForeignKey(
                        name: "FK_UserTokens_Users_userId",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AIAdvicesLogs",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(type: "int", nullable: false),
                    userPlantId = table.Column<int>(type: "int", nullable: true),
                    role = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    content = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: false),
                    uniqueGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    isDelete = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    deletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AIAdvicesLogs", x => x.id);
                    table.ForeignKey(
                        name: "FK_AIAdvicesLogs_UserPlants_userPlantId",
                        column: x => x.userPlantId,
                        principalTable: "UserPlants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AIAdvicesLogs_Users_userId",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CareHistories",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userPlantId = table.Column<int>(type: "int", nullable: false),
                    careType = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    careDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    quantity = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    photoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    effectiveness = table.Column<int>(type: "int", nullable: true),
                    uniqueGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    isDelete = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    deletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CareHistories", x => x.id);
                    table.ForeignKey(
                        name: "FK_CareHistories_UserPlants_userPlantId",
                        column: x => x.userPlantId,
                        principalTable: "UserPlants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CareSchedules",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userPlantId = table.Column<int>(type: "int", nullable: false),
                    taskName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    scheduleTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    isCompleted = table.Column<bool>(type: "bit", nullable: false),
                    frequency = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    nextScheduledDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    completedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    uniqueGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    isDelete = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    deletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CareSchedules", x => x.id);
                    table.ForeignKey(
                        name: "FK_CareSchedules_UserPlants_userPlantId",
                        column: x => x.userPlantId,
                        principalTable: "UserPlants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "id", "createdAt", "deletedAt", "description", "name", "uniqueGuid", "updatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Permission to create plants", "createPlant", new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Permission to edit plants", "editPlant", new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 3, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Permission to delete plants", "deletePlant", new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 4, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Permission to view orders/payments", "viewOrders", new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 5, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Permission to manage users", "manageUsers", new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 6, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Permission to send messages", "sendMessages", new Guid("ffffffff-ffff-ffff-ffff-ffffffffffff"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "id", "createdAt", "deletedAt", "description", "name", "uniqueGuid", "updatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Administrator with full permissions", "admin", new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Regular user with limited permissions", "user", new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 3, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Nursery staff with specific permissions", "nursery", new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "permissionId", "roleId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 1 },
                    { 4, 1 },
                    { 5, 1 },
                    { 6, 1 },
                    { 1, 2 },
                    { 4, 2 },
                    { 6, 2 },
                    { 1, 3 },
                    { 2, 3 },
                    { 4, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AIAdvicesLogs_CreatedAt",
                table: "AIAdvicesLogs",
                column: "createdAt");

            migrationBuilder.CreateIndex(
                name: "IX_AIAdvicesLogs_userId",
                table: "AIAdvicesLogs",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_AIAdvicesLogs_userPlantId",
                table: "AIAdvicesLogs",
                column: "userPlantId");

            migrationBuilder.CreateIndex(
                name: "IX_ArModels_CreatedAt",
                table: "ArModels",
                column: "createdAt");

            migrationBuilder.CreateIndex(
                name: "IX_ArModels_plantId",
                table: "ArModels",
                column: "plantId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CareHistories_CreatedAt",
                table: "CareHistories",
                column: "createdAt");

            migrationBuilder.CreateIndex(
                name: "IX_CareHistories_userPlantId",
                table: "CareHistories",
                column: "userPlantId");

            migrationBuilder.CreateIndex(
                name: "IX_CareHistory_CareDate",
                table: "CareHistories",
                column: "careDate");

            migrationBuilder.CreateIndex(
                name: "IX_CareSchedules_CreatedAt",
                table: "CareSchedules",
                column: "createdAt");

            migrationBuilder.CreateIndex(
                name: "IX_CareSchedules_userPlantId",
                table: "CareSchedules",
                column: "userPlantId");

            migrationBuilder.CreateIndex(
                name: "IX_Guides_CreatedAt",
                table: "Guides",
                column: "createdAt");

            migrationBuilder.CreateIndex(
                name: "IX_Guides_plantId",
                table: "Guides",
                column: "plantId");

            migrationBuilder.CreateIndex(
                name: "IX_LogEntries_CreatedAt",
                table: "LogEntries",
                column: "createdAt");

            migrationBuilder.CreateIndex(
                name: "IX_LogEntry_UserCreated",
                table: "LogEntries",
                columns: new[] { "userId", "createdAt" });

            migrationBuilder.CreateIndex(
                name: "IX_NurseryProfiles_CreatedAt",
                table: "NurseryProfiles",
                column: "createdAt");

            migrationBuilder.CreateIndex(
                name: "IX_NurseryProfiles_userId",
                table: "NurseryProfiles",
                column: "userId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payment_UserStatusCreated",
                table: "Payments",
                columns: new[] { "userId", "status", "createdAt" });

            migrationBuilder.CreateIndex(
                name: "IX_Payments_CreatedAt",
                table: "Payments",
                column: "createdAt");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_CreatedAt",
                table: "Permissions",
                column: "createdAt");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_CreatedAt",
                table: "Photos",
                column: "createdAt");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_plantId",
                table: "Photos",
                column: "plantId");

            migrationBuilder.CreateIndex(
                name: "IX_PlantCategories_CreatedAt",
                table: "PlantCategories",
                column: "createdAt");

            migrationBuilder.CreateIndex(
                name: "IX_PlantDiseases_CreatedAt",
                table: "PlantDiseases",
                column: "createdAt");

            migrationBuilder.CreateIndex(
                name: "IX_Plant_CategoryId",
                table: "Plants",
                column: "plantCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Plants_CreatedAt",
                table: "Plants",
                column: "createdAt");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_permissionId",
                table: "RolePermissions",
                column: "permissionId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_CreatedAt",
                table: "Roles",
                column: "createdAt");

            migrationBuilder.CreateIndex(
                name: "IX_UserMessage_ReceiverReadCreated",
                table: "UserMessages",
                columns: new[] { "receiverId", "isRead", "createdAt" });

            migrationBuilder.CreateIndex(
                name: "IX_UserMessages_CreatedAt",
                table: "UserMessages",
                column: "createdAt");

            migrationBuilder.CreateIndex(
                name: "IX_UserMessages_senderId",
                table: "UserMessages",
                column: "senderId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPlants_CreatedAt",
                table: "UserPlants",
                column: "createdAt");

            migrationBuilder.CreateIndex(
                name: "IX_UserPlants_plantId",
                table: "UserPlants",
                column: "plantId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPlants_userId",
                table: "UserPlants",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_CreatedAt",
                table: "UserProfiles",
                column: "createdAt");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_userId",
                table: "UserProfiles",
                column: "userId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_CreatedAt",
                table: "Users",
                column: "createdAt");

            migrationBuilder.CreateIndex(
                name: "IX_Users_roleId",
                table: "Users",
                column: "roleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTokens_CreatedAt",
                table: "UserTokens",
                column: "createdAt");

            migrationBuilder.CreateIndex(
                name: "IX_UserTokens_userId",
                table: "UserTokens",
                column: "userId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AIAdvicesLogs");

            migrationBuilder.DropTable(
                name: "ArModels");

            migrationBuilder.DropTable(
                name: "CareHistories");

            migrationBuilder.DropTable(
                name: "CareSchedules");

            migrationBuilder.DropTable(
                name: "Guides");

            migrationBuilder.DropTable(
                name: "LogEntries");

            migrationBuilder.DropTable(
                name: "NurseryProfiles");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.DropTable(
                name: "PlantDiseases");

            migrationBuilder.DropTable(
                name: "RolePermissions");

            migrationBuilder.DropTable(
                name: "UserMessages");

            migrationBuilder.DropTable(
                name: "UserProfiles");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "UserPlants");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "Plants");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "PlantCategories");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
