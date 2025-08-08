using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

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
                name: "permissions",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    uniqueGuid = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    updatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_permissions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "plantCategories",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    uniqueGuid = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    updatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_plantCategories", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "plantDiseases",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    symptoms = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    treatment = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    prevention = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    uniqueGuid = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    updatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_plantDiseases", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    uniqueGuid = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    updatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "plants",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    scientificName = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    commonName = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    careInstructions = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    plantCategoryId = table.Column<int>(type: "integer", nullable: true),
                    isIndoor = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    wateringFrequency = table.Column<int>(type: "integer", maxLength: 100, nullable: false),
                    lightRequirement = table.Column<int>(type: "integer", maxLength: 100, nullable: false),
                    soilType = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    averagePrice = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    uniqueGuid = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    updatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_plants", x => x.id);
                    table.ForeignKey(
                        name: "FK_plants_plantCategories_plantCategoryId",
                        column: x => x.plantCategoryId,
                        principalTable: "plantCategories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "rolePermissions",
                columns: table => new
                {
                    roleId = table.Column<int>(type: "integer", nullable: false),
                    permissionId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rolePermissions", x => new { x.roleId, x.permissionId });
                    table.ForeignKey(
                        name: "FK_rolePermissions_permissions_permissionId",
                        column: x => x.permissionId,
                        principalTable: "permissions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_rolePermissions_roles_roleId",
                        column: x => x.roleId,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    username = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    passwordHash = table.Column<string>(type: "text", nullable: false),
                    roleId = table.Column<int>(type: "integer", nullable: false),
                    uniqueGuid = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    updatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                    table.ForeignKey(
                        name: "FK_users_roles_roleId",
                        column: x => x.roleId,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "arModels",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    plantId = table.Column<int>(type: "integer", nullable: false),
                    modelUrl = table.Column<string>(type: "text", nullable: false),
                    fileFormat = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    uniqueGuid = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    updatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_arModels", x => x.id);
                    table.ForeignKey(
                        name: "FK_arModels_plants_plantId",
                        column: x => x.plantId,
                        principalTable: "plants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "guides",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    plantId = table.Column<int>(type: "integer", nullable: false),
                    title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    content = table.Column<string>(type: "character varying(5000)", maxLength: 5000, nullable: true),
                    uniqueGuid = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    updatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_guides", x => x.id);
                    table.ForeignKey(
                        name: "FK_guides_plants_plantId",
                        column: x => x.plantId,
                        principalTable: "plants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "photos",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    plantId = table.Column<int>(type: "integer", nullable: false),
                    photoUrl = table.Column<string>(type: "text", nullable: false),
                    caption = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    uniqueGuid = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    updatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_photos", x => x.id);
                    table.ForeignKey(
                        name: "FK_photos_plants_plantId",
                        column: x => x.plantId,
                        principalTable: "plants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "logEntries",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    userId = table.Column<int>(type: "integer", nullable: false),
                    action = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    metadata = table.Column<string>(type: "text", nullable: true),
                    uniqueGuid = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    updatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_logEntries", x => x.id);
                    table.ForeignKey(
                        name: "FK_logEntries_users_userId",
                        column: x => x.userId,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "nurseryProfiles",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    userId = table.Column<int>(type: "integer", nullable: false),
                    nurseryName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    address = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    contactNumber = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    uniqueGuid = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    updatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_nurseryProfiles", x => x.id);
                    table.ForeignKey(
                        name: "FK_nurseryProfiles_users_userId",
                        column: x => x.userId,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "payments",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    userId = table.Column<int>(type: "integer", nullable: false),
                    amount = table.Column<decimal>(type: "numeric", nullable: false),
                    currency = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    paymentMethod = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    transactionId = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    processedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    uniqueGuid = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    updatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payments", x => x.id);
                    table.CheckConstraint("CK_Payment_PositiveAmount", "amount > 0");
                    table.ForeignKey(
                        name: "FK_payments_users_userId",
                        column: x => x.userId,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "userMessages",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    senderId = table.Column<int>(type: "integer", nullable: false),
                    receiverId = table.Column<int>(type: "integer", nullable: false),
                    messageText = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    isRead = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    uniqueGuid = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    updatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userMessages", x => x.id);
                    table.CheckConstraint("CK_UserMessage_DifferentUsers", "\"senderId\" != \"receiverId\"");
                    table.ForeignKey(
                        name: "FK_userMessages_users_receiverId",
                        column: x => x.receiverId,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_userMessages_users_senderId",
                        column: x => x.senderId,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "userPlants",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    userId = table.Column<int>(type: "integer", nullable: false),
                    plantId = table.Column<int>(type: "integer", nullable: false),
                    nickname = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    acquiredDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    notes = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    healthStatus = table.Column<int>(type: "integer", maxLength: 50, nullable: false, defaultValue: 1),
                    currentLocation = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    isActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    uniqueGuid = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    updatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userPlants", x => x.id);
                    table.ForeignKey(
                        name: "FK_userPlants_plants_plantId",
                        column: x => x.plantId,
                        principalTable: "plants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_userPlants_users_userId",
                        column: x => x.userId,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "userProfiles",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    userId = table.Column<int>(type: "integer", nullable: false),
                    fullName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    avatarUrl = table.Column<string>(type: "text", nullable: true),
                    bio = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    location = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    uniqueGuid = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    updatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userProfiles", x => x.id);
                    table.ForeignKey(
                        name: "FK_userProfiles_users_userId",
                        column: x => x.userId,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "aiAdvicesLogs",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    userPlantId = table.Column<int>(type: "integer", nullable: false),
                    adviceText = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    uniqueGuid = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    updatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_aiAdvicesLogs", x => x.id);
                    table.ForeignKey(
                        name: "FK_aiAdvicesLogs_userPlants_userPlantId",
                        column: x => x.userPlantId,
                        principalTable: "userPlants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "careHistories",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    userPlantId = table.Column<int>(type: "integer", nullable: false),
                    careType = table.Column<int>(type: "integer", maxLength: 50, nullable: false),
                    careDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    notes = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    quantity = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    photoUrl = table.Column<string>(type: "text", nullable: true),
                    effectiveness = table.Column<int>(type: "integer", nullable: true),
                    uniqueGuid = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    updatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_careHistories", x => x.id);
                    table.ForeignKey(
                        name: "FK_careHistories_userPlants_userPlantId",
                        column: x => x.userPlantId,
                        principalTable: "userPlants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "caresSchedules",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    userPlantId = table.Column<int>(type: "integer", nullable: false),
                    taskName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    scheduleTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    isCompleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    frequency = table.Column<int>(type: "integer", maxLength: 50, nullable: false),
                    nextScheduledDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    notes = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    completedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    uniqueGuid = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    updatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_caresSchedules", x => x.id);
                    table.ForeignKey(
                        name: "FK_caresSchedules_userPlants_userPlantId",
                        column: x => x.userPlantId,
                        principalTable: "userPlants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "permissions",
                columns: new[] { "id", "createdAt", "description", "name", "updatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 8, 8, 17, 57, 22, 840, DateTimeKind.Utc).AddTicks(3413), "Permission to create plants", "createPlant", new DateTime(2025, 8, 8, 17, 57, 22, 840, DateTimeKind.Utc).AddTicks(3414) },
                    { 2, new DateTime(2025, 8, 8, 17, 57, 22, 840, DateTimeKind.Utc).AddTicks(3925), "Permission to edit plants", "editPlant", new DateTime(2025, 8, 8, 17, 57, 22, 840, DateTimeKind.Utc).AddTicks(3926) },
                    { 3, new DateTime(2025, 8, 8, 17, 57, 22, 840, DateTimeKind.Utc).AddTicks(3927), "Permission to delete plants", "deletePlant", new DateTime(2025, 8, 8, 17, 57, 22, 840, DateTimeKind.Utc).AddTicks(3927) },
                    { 4, new DateTime(2025, 8, 8, 17, 57, 22, 840, DateTimeKind.Utc).AddTicks(3928), "Permission to view orders/payments", "viewOrders", new DateTime(2025, 8, 8, 17, 57, 22, 840, DateTimeKind.Utc).AddTicks(3929) },
                    { 5, new DateTime(2025, 8, 8, 17, 57, 22, 840, DateTimeKind.Utc).AddTicks(3929), "Permission to manage users", "manageUsers", new DateTime(2025, 8, 8, 17, 57, 22, 840, DateTimeKind.Utc).AddTicks(3930) },
                    { 6, new DateTime(2025, 8, 8, 17, 57, 22, 840, DateTimeKind.Utc).AddTicks(3931), "Permission to send messages", "sendMessages", new DateTime(2025, 8, 8, 17, 57, 22, 840, DateTimeKind.Utc).AddTicks(3931) }
                });

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "createdAt", "description", "name", "updatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 8, 8, 17, 57, 22, 839, DateTimeKind.Utc).AddTicks(7581), "Administrator with full permissions", "admin", new DateTime(2025, 8, 8, 17, 57, 22, 839, DateTimeKind.Utc).AddTicks(7583) },
                    { 2, new DateTime(2025, 8, 8, 17, 57, 22, 839, DateTimeKind.Utc).AddTicks(8314), "Regular user with limited permissions", "user", new DateTime(2025, 8, 8, 17, 57, 22, 839, DateTimeKind.Utc).AddTicks(8314) },
                    { 3, new DateTime(2025, 8, 8, 17, 57, 22, 839, DateTimeKind.Utc).AddTicks(8316), "Nursery staff with specific permissions", "nursery", new DateTime(2025, 8, 8, 17, 57, 22, 839, DateTimeKind.Utc).AddTicks(8316) }
                });

            migrationBuilder.InsertData(
                table: "rolePermissions",
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
                name: "IX_aiAdvicesLogs_CreatedAt",
                table: "aiAdvicesLogs",
                column: "createdAt");

            migrationBuilder.CreateIndex(
                name: "IX_aiAdvicesLogs_userPlantId",
                table: "aiAdvicesLogs",
                column: "userPlantId");

            migrationBuilder.CreateIndex(
                name: "IX_arModels_CreatedAt",
                table: "arModels",
                column: "createdAt");

            migrationBuilder.CreateIndex(
                name: "IX_arModels_plantId",
                table: "arModels",
                column: "plantId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_careHistories_CreatedAt",
                table: "careHistories",
                column: "createdAt");

            migrationBuilder.CreateIndex(
                name: "IX_CareHistory_CareDate",
                table: "careHistories",
                column: "careDate");

            migrationBuilder.CreateIndex(
                name: "IX_CareHistory_Query",
                table: "careHistories",
                columns: new[] { "userPlantId", "careType", "careDate" });

            migrationBuilder.CreateIndex(
                name: "IX_caresSchedules_CreatedAt",
                table: "caresSchedules",
                column: "createdAt");

            migrationBuilder.CreateIndex(
                name: "IX_CaresSchedules_Query",
                table: "caresSchedules",
                columns: new[] { "userPlantId", "scheduleTime", "isCompleted" });

            migrationBuilder.CreateIndex(
                name: "IX_guides_CreatedAt",
                table: "guides",
                column: "createdAt");

            migrationBuilder.CreateIndex(
                name: "IX_guides_plantId",
                table: "guides",
                column: "plantId");

            migrationBuilder.CreateIndex(
                name: "IX_logEntries_CreatedAt",
                table: "logEntries",
                column: "createdAt");

            migrationBuilder.CreateIndex(
                name: "IX_LogEntry_UserCreated",
                table: "logEntries",
                columns: new[] { "userId", "createdAt" });

            migrationBuilder.CreateIndex(
                name: "IX_nurseryProfiles_CreatedAt",
                table: "nurseryProfiles",
                column: "createdAt");

            migrationBuilder.CreateIndex(
                name: "IX_nurseryProfiles_userId",
                table: "nurseryProfiles",
                column: "userId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payment_UserStatusCreated",
                table: "payments",
                columns: new[] { "userId", "status", "createdAt" });

            migrationBuilder.CreateIndex(
                name: "IX_payments_CreatedAt",
                table: "payments",
                column: "createdAt");

            migrationBuilder.CreateIndex(
                name: "IX_permissions_CreatedAt",
                table: "permissions",
                column: "createdAt");

            migrationBuilder.CreateIndex(
                name: "IX_permissions_name",
                table: "permissions",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_photos_CreatedAt",
                table: "photos",
                column: "createdAt");

            migrationBuilder.CreateIndex(
                name: "IX_photos_plantId",
                table: "photos",
                column: "plantId");

            migrationBuilder.CreateIndex(
                name: "IX_plantCategories_CreatedAt",
                table: "plantCategories",
                column: "createdAt");

            migrationBuilder.CreateIndex(
                name: "IX_plantCategories_name",
                table: "plantCategories",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_plantDiseases_CreatedAt",
                table: "plantDiseases",
                column: "createdAt");

            migrationBuilder.CreateIndex(
                name: "IX_plantDiseases_name",
                table: "plantDiseases",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Plant_CategoryId",
                table: "plants",
                column: "plantCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_plants_CreatedAt",
                table: "plants",
                column: "createdAt");

            migrationBuilder.CreateIndex(
                name: "IX_plants_scientificName",
                table: "plants",
                column: "scientificName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_rolePermissions_permissionId",
                table: "rolePermissions",
                column: "permissionId");

            migrationBuilder.CreateIndex(
                name: "IX_roles_CreatedAt",
                table: "roles",
                column: "createdAt");

            migrationBuilder.CreateIndex(
                name: "IX_roles_name",
                table: "roles",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserMessage_ReceiverReadCreated",
                table: "userMessages",
                columns: new[] { "receiverId", "isRead", "createdAt" });

            migrationBuilder.CreateIndex(
                name: "IX_userMessages_CreatedAt",
                table: "userMessages",
                column: "createdAt");

            migrationBuilder.CreateIndex(
                name: "IX_userMessages_senderId",
                table: "userMessages",
                column: "senderId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPlant_Unique",
                table: "userPlants",
                columns: new[] { "userId", "plantId", "acquiredDate" });

            migrationBuilder.CreateIndex(
                name: "IX_userPlants_CreatedAt",
                table: "userPlants",
                column: "createdAt");

            migrationBuilder.CreateIndex(
                name: "IX_userPlants_plantId",
                table: "userPlants",
                column: "plantId");

            migrationBuilder.CreateIndex(
                name: "IX_userProfiles_CreatedAt",
                table: "userProfiles",
                column: "createdAt");

            migrationBuilder.CreateIndex(
                name: "IX_userProfiles_userId",
                table: "userProfiles",
                column: "userId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_CreatedAt",
                table: "users",
                column: "createdAt");

            migrationBuilder.CreateIndex(
                name: "IX_users_email",
                table: "users",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_roleId",
                table: "users",
                column: "roleId");

            migrationBuilder.CreateIndex(
                name: "IX_users_username",
                table: "users",
                column: "username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "aiAdvicesLogs");

            migrationBuilder.DropTable(
                name: "arModels");

            migrationBuilder.DropTable(
                name: "careHistories");

            migrationBuilder.DropTable(
                name: "caresSchedules");

            migrationBuilder.DropTable(
                name: "guides");

            migrationBuilder.DropTable(
                name: "logEntries");

            migrationBuilder.DropTable(
                name: "nurseryProfiles");

            migrationBuilder.DropTable(
                name: "payments");

            migrationBuilder.DropTable(
                name: "photos");

            migrationBuilder.DropTable(
                name: "plantDiseases");

            migrationBuilder.DropTable(
                name: "rolePermissions");

            migrationBuilder.DropTable(
                name: "userMessages");

            migrationBuilder.DropTable(
                name: "userProfiles");

            migrationBuilder.DropTable(
                name: "userPlants");

            migrationBuilder.DropTable(
                name: "permissions");

            migrationBuilder.DropTable(
                name: "plants");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "plantCategories");

            migrationBuilder.DropTable(
                name: "roles");
        }
    }
}
