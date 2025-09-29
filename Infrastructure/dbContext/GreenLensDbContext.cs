using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ProjectGreenLens.Models.Entities;

namespace ProjectGreenLens.Infrastructure.dbContext
{
    public class GreenLensDbContext : DbContext
    {
        public GreenLensDbContext(DbContextOptions<GreenLensDbContext> options)
            : base(options)
        {
        }

        // DbSet cho tất cả các entity
        public DbSet<AIAdvicesLogs> AIAdvicesLogs { get; set; } = null!;
        public DbSet<ArModel> ArModels { get; set; } = null!;
        public DbSet<CareHistory> CareHistories { get; set; } = null!;
        public DbSet<CaresSchedules> CareSchedules { get; set; } = null!;
        public DbSet<Guide> Guides { get; set; } = null!;
        public DbSet<LogEntry> LogEntries { get; set; } = null!;
        public DbSet<NurseryProfile> NurseryProfiles { get; set; } = null!;
        public DbSet<Payment> Payments { get; set; } = null!;
        public DbSet<Permission> Permissions { get; set; } = null!;
        public DbSet<Plant> Plants { get; set; } = null!;
        public DbSet<PlantCategory> PlantCategories { get; set; } = null!;
        public DbSet<PlantDisease> PlantDiseases { get; set; } = null!;
        public DbSet<Photo> Photos { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<RolePermission> RolePermissions { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<UserMessage> UserMessages { get; set; } = null!;
        public DbSet<UserPlant> UserPlants { get; set; } = null!;
        public DbSet<UserProfile> UserProfiles { get; set; } = null!;
        public DbSet<UserToken> UserTokens { get; set; } = null!;
        public DbSet<PermissionQuota> PermissionQuotas { get; set; } = null!;
        public DbSet<UserPermissionUsage> UserPermissionUsages { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Cấu hình BaseEntity
            ConfigureBaseEntity(modelBuilder);

            // Cấu hình các chỉ mục
            ConfigureIndexes(modelBuilder);

            // Cấu hình kiểu dữ liệu cho AIAdvicesLogs
            modelBuilder.Entity<AIAdvicesLogs>(entity =>
            {
                entity.Property(e => e.userId).HasColumnType("int").IsRequired();
                entity.Property(e => e.userPlantId).HasColumnType("int"); // Không bắt buộc
                entity.Property(e => e.role).HasColumnType("varchar(20)").IsRequired();
                entity.Property(e => e.content).HasColumnType("varchar(4000)").IsRequired();
                entity.HasOne(e => e.user).WithMany(u => u.aiAdvicesLogs).HasForeignKey(e => e.userId).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(e => e.userPlant).WithMany(up => up.aiAdvicesLogs).HasForeignKey(e => e.userPlantId).OnDelete(DeleteBehavior.Restrict);
            });

            // Cấu hình kiểu dữ liệu cho ArModel
            modelBuilder.Entity<ArModel>(entity =>
            {
                entity.Property(e => e.plantId).HasColumnType("int").IsRequired();
                entity.Property(e => e.modelUrl).HasColumnType("nvarchar(max)").IsRequired();
                entity.Property(e => e.fileFormat).HasColumnType("varchar(10)"); // Không bắt buộc
                entity.HasOne(e => e.plant).WithOne(p => p.arModel).HasForeignKey<ArModel>(e => e.plantId);
            });

            // Cấu hình kiểu dữ liệu cho CareHistory
            modelBuilder.Entity<CareHistory>(entity =>
            {
                entity.Property(e => e.userPlantId).HasColumnType("int").IsRequired();
                entity.Property(e => e.careType).HasColumnType("int").IsRequired();
                entity.Property(e => e.careDate).HasColumnType("datetime2").IsRequired();
                entity.Property(e => e.notes).HasColumnType("nvarchar(500)"); // Không bắt buộc
                entity.Property(e => e.quantity).HasColumnType("nvarchar(100)"); // Không bắt buộc
                entity.Property(e => e.photoUrl).HasColumnType("nvarchar(max)"); // Không bắt buộc
                entity.Property(e => e.effectiveness).HasColumnType("int"); // Không bắt buộc
                entity.HasOne(e => e.userPlant).WithMany(up => up.careHistories).HasForeignKey(e => e.userPlantId);
            });

            // Cấu hình kiểu dữ liệu cho CaresSchedules
            modelBuilder.Entity<CaresSchedules>(entity =>
            {
                entity.Property(e => e.userPlantId).HasColumnType("int").IsRequired();
                entity.Property(e => e.taskName).HasColumnType("nvarchar(100)").IsRequired();
                entity.Property(e => e.scheduleTime).HasColumnType("datetime2").IsRequired();
                entity.Property(e => e.isCompleted).HasColumnType("bit").IsRequired();
                entity.Property(e => e.frequency).HasColumnType("int"); // Không bắt buộc
                entity.Property(e => e.nextScheduledDate).HasColumnType("datetime2"); // Không bắt buộc
                entity.Property(e => e.notes).HasColumnType("nvarchar(500)"); // Không bắt buộc
                entity.Property(e => e.completedAt).HasColumnType("datetime2"); // Không bắt buộc
                entity.HasOne(e => e.userPlant).WithMany(up => up.careSchedules).HasForeignKey(e => e.userPlantId);
            });

            // Cấu hình kiểu dữ liệu cho Guide
            modelBuilder.Entity<Guide>(entity =>
            {
                entity.Property(e => e.plantId).HasColumnType("int").IsRequired();
                entity.Property(e => e.title).HasColumnType("nvarchar(200)").IsRequired();
                entity.Property(e => e.content).HasColumnType("nvarchar(4000)"); // Không bắt buộc
                entity.HasOne(e => e.plant).WithMany(p => p.guides).HasForeignKey(e => e.plantId);
            });

            // Cấu hình kiểu dữ liệu cho LogEntry
            modelBuilder.Entity<LogEntry>(entity =>
            {
                entity.Property(e => e.userId).HasColumnType("int").IsRequired();
                entity.Property(e => e.action).HasColumnType("nvarchar(100)").IsRequired();
                entity.Property(e => e.metadata).HasColumnType("nvarchar(max)"); // Không bắt buộc
                entity.HasOne(e => e.user).WithMany(u => u.logEntries).HasForeignKey(e => e.userId);
            });

            // Cấu hình kiểu dữ liệu cho NurseryProfile
            modelBuilder.Entity<NurseryProfile>(entity =>
            {
                entity.Property(e => e.userId).HasColumnType("int").IsRequired();
                entity.Property(e => e.nurseryName).HasColumnType("nvarchar(100)").IsRequired();
                entity.Property(e => e.address).HasColumnType("nvarchar(200)"); // Không bắt buộc
                entity.Property(e => e.contactNumber).HasColumnType("nvarchar(20)"); // Không bắt buộc
                entity.Property(e => e.description).HasColumnType("nvarchar(1000)"); // Không bắt buộc
                entity.HasOne(e => e.user).WithOne(u => u.nurseryProfile).HasForeignKey<NurseryProfile>(e => e.userId);
            });


            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("Payments");

                entity.Property(p => p.userId)
                      .IsRequired();

                entity.HasOne(p => p.user)
                      .WithMany()
                      .HasForeignKey(p => p.userId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.Property(p => p.amount)
                      .IsRequired()
                      .HasColumnType("decimal(18,2)");

                entity.Property(p => p.currency)
                      .IsRequired()
                      .HasMaxLength(10);

                entity.Property(p => p.paymentMethod)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(p => p.status)
                      .IsRequired()
                      .HasConversion<string>()          // Enum lưu dưới dạng string
                      .HasColumnType("nvarchar(20)");

                entity.Property(p => p.transactionId)
                      .HasMaxLength(255);

                entity.Property(p => p.description)
                      .HasMaxLength(500);               // vẫn giữ description

                entity.Property(p => p.orderId)
                      .HasMaxLength(100);

                entity.Property(p => p.purchaseToken)
                      .HasMaxLength(255);

                entity.Property(p => p.productRefId);

                entity.HasOne(p => p.product)
                      .WithMany()
                      .HasForeignKey(p => p.productRefId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.Property(p => p.processedAt);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Products");
                entity.Property(p => p.ProductId)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(p => p.Name)
                      .IsRequired()
                      .HasMaxLength(255);

                entity.Property(p => p.Description)
                      .HasMaxLength(500);

                entity.Property(p => p.Price)
                      .HasColumnType("decimal(18,2)");

                entity.Property(p => p.Currency)
                      .IsRequired()
                      .HasMaxLength(10);

                entity.Property(p => p.Type)
                      .IsRequired()
                      .HasConversion<string>()          // Enum lưu string
                      .HasMaxLength(50);
            });
            // Cấu hình kiểu dữ liệu cho Permission
            modelBuilder.Entity<Permission>(entity =>
            {
                entity.Property(e => e.name).HasColumnType("nvarchar(50)").IsRequired();
                entity.Property(e => e.description).HasColumnType("nvarchar(500)").IsRequired();
            });

            // Cấu hình kiểu dữ liệu cho Plant
            modelBuilder.Entity<Plant>(entity =>
            {
                entity.Property(e => e.scientificName).HasColumnType("nvarchar(150)").IsRequired();
                entity.Property(e => e.commonName).HasColumnType("nvarchar(150)"); // Không bắt buộc
                entity.Property(e => e.description).HasColumnType("nvarchar(2000)"); // Không bắt buộc
                entity.Property(e => e.careInstructions).HasColumnType("nvarchar(2000)"); // Không bắt buộc
                entity.Property(e => e.plantCategoryId).HasColumnType("int"); // Không bắt buộc
                entity.Property(e => e.isIndoor).HasColumnType("bit"); // Không bắt buộc (mặc định false trong C#)
                entity.Property(e => e.wateringFrequency).HasColumnType("int").IsRequired();
                entity.Property(e => e.lightRequirement).HasColumnType("int").IsRequired();
                entity.Property(e => e.soilType).HasColumnType("nvarchar(100)"); // Không bắt buộc
                entity.Property(e => e.averagePrice).HasColumnType("decimal(18,2)"); // Không bắt buộc
                entity.HasOne(e => e.plantCategory).WithMany(pc => pc.plants).HasForeignKey(e => e.plantCategoryId);
            });

            // Cấu hình kiểu dữ liệu cho PlantCategory
            modelBuilder.Entity<PlantCategory>(entity =>
            {
                entity.Property(e => e.name).HasColumnType("nvarchar(100)").IsRequired();
                entity.Property(e => e.description).HasColumnType("nvarchar(500)"); // Không bắt buộc
            });

            // Cấu hình kiểu dữ liệu cho PlantDisease
            modelBuilder.Entity<PlantDisease>(entity =>
            {
                entity.Property(e => e.name).HasColumnType("nvarchar(100)").IsRequired();
                entity.Property(e => e.symptoms).HasColumnType("nvarchar(1000)"); // Không bắt buộc
                entity.Property(e => e.treatment).HasColumnType("nvarchar(1000)"); // Không bắt buộc
                entity.Property(e => e.prevention).HasColumnType("nvarchar(1000)"); // Không bắt buộc
            });

            // Cấu hình kiểu dữ liệu cho Photo
            modelBuilder.Entity<Photo>(entity =>
            {
                entity.Property(e => e.plantId).HasColumnType("int").IsRequired();
                entity.Property(e => e.photoUrl).HasColumnType("nvarchar(max)").IsRequired();
                entity.Property(e => e.caption).HasColumnType("nvarchar(250)"); // Không bắt buộc
                entity.HasOne(e => e.plant).WithMany(p => p.photos).HasForeignKey(e => e.plantId);
            });

            // Cấu hình kiểu dữ liệu cho Role
            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.name).HasColumnType("nvarchar(50)").IsRequired();
                entity.Property(e => e.description).HasColumnType("nvarchar(500)").IsRequired();
            });

            // Cấu hình kiểu dữ liệu cho RolePermission
            modelBuilder.Entity<RolePermission>(entity =>
            {
                entity.Property(e => e.roleId).HasColumnType("int").IsRequired();
                entity.Property(e => e.permissionId).HasColumnType("int").IsRequired();
                entity.HasKey(e => new { e.roleId, e.permissionId });
                entity.HasOne(e => e.role).WithMany(r => r.rolePermissions).HasForeignKey(e => e.roleId);
                entity.HasOne(e => e.permission).WithMany(p => p.rolePermissions).HasForeignKey(e => e.permissionId);
            });

            // Cấu hình kiểu dữ liệu cho User
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.username).HasColumnType("nvarchar(50)").IsRequired();
                entity.Property(e => e.email).HasColumnType("nvarchar(100)").IsRequired();
                entity.Property(e => e.passwordHash).HasColumnType("nvarchar(max)").IsRequired();
                entity.Property(e => e.roleId).HasColumnType("int").IsRequired();
                entity.HasOne(e => e.role).WithMany(r => r.users).HasForeignKey(e => e.roleId);
            });

            // Cấu hình kiểu dữ liệu cho UserMessage
            modelBuilder.Entity<UserMessage>(entity =>
            {
                entity.Property(e => e.senderId).HasColumnType("int").IsRequired();
                entity.Property(e => e.receiverId).HasColumnType("int").IsRequired();
                entity.Property(e => e.messageText).HasColumnType("nvarchar(2000)").IsRequired();
                entity.Property(e => e.isRead).HasColumnType("bit").IsRequired();
                entity.HasOne(e => e.sender).WithMany(u => u.sentMessages).HasForeignKey(e => e.senderId).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(e => e.receiver).WithMany(u => u.receivedMessages).HasForeignKey(e => e.receiverId).OnDelete(DeleteBehavior.Restrict);
            });

            // Cấu hình kiểu dữ liệu cho UserPlant
            modelBuilder.Entity<UserPlant>(entity =>
            {
                entity.Property(e => e.userId).HasColumnType("int").IsRequired();
                entity.Property(e => e.plantId).HasColumnType("int").IsRequired();
                entity.Property(e => e.nickname).HasColumnType("nvarchar(100)"); // Không bắt buộc
                entity.Property(e => e.acquiredDate).HasColumnType("datetime2").IsRequired();
                entity.Property(e => e.notes).HasColumnType("nvarchar(2000)"); // Không bắt buộc
                entity.Property(e => e.healthStatus).HasColumnType("int").IsRequired();
                entity.Property(e => e.currentLocation).HasColumnType("nvarchar(500)"); // Không bắt buộc
                entity.Property(e => e.isActive).HasColumnType("bit"); // Không bắt buộc (mặc định true trong C#)
                entity.HasOne(e => e.user).WithMany(u => u.userPlants).HasForeignKey(e => e.userId);
                entity.HasOne(e => e.plant).WithMany(p => p.userPlants).HasForeignKey(e => e.plantId);
            });

            // Cấu hình kiểu dữ liệu cho UserProfile
            modelBuilder.Entity<UserProfile>(entity =>
            {
                entity.Property(e => e.userId).HasColumnType("int").IsRequired();
                entity.Property(e => e.fullName).HasColumnType("nvarchar(100)"); // Không bắt buộc
                entity.Property(e => e.avatarUrl).HasColumnType("nvarchar(max)"); // Không bắt buộc
                entity.Property(e => e.bio).HasColumnType("nvarchar(1000)"); // Không bắt buộc
                entity.Property(e => e.location).HasColumnType("nvarchar(100)"); // Không bắt buộc
                entity.HasOne(e => e.user).WithOne(u => u.userProfile).HasForeignKey<UserProfile>(e => e.userId);
            });

            // Cấu hình kiểu dữ liệu cho UserToken
            modelBuilder.Entity<UserToken>(entity =>
            {
                entity.Property(e => e.token).HasColumnType("nvarchar(max)").IsRequired();
                entity.Property(e => e.expiresAt).HasColumnType("datetime2").IsRequired();
                entity.Property(e => e.isRevoked).HasColumnType("bit").IsRequired();
                entity.Property(e => e.type).HasColumnType("nvarchar(50)").IsRequired().HasConversion<string>();
                entity.Property(e => e.userId).HasColumnType("int").IsRequired();
                entity.HasOne(e => e.user).WithMany(u => u.userTokens).HasForeignKey(e => e.userId);
            });
            // RolePermission: composite key
            modelBuilder.Entity<RolePermission>(entity =>
            {
                entity.HasKey(rp => new { rp.roleId, rp.permissionId });

                entity.HasOne(rp => rp.role)
                      .WithMany(r => r.rolePermissions)
                      .HasForeignKey(rp => rp.roleId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(rp => rp.permission)
                      .WithMany(p => p.rolePermissions)
                      .HasForeignKey(rp => rp.permissionId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<RolePermission>()
                .HasKey(rp => new { rp.roleId, rp.permissionId });
            // PermissionQuota: composite key (roleId + permissionId)
            modelBuilder.Entity<PermissionQuota>(entity =>
            {
                entity.HasKey(pq => new { pq.roleId, pq.permissionId });

                entity.Property(pq => pq.usageLimit)
                      .IsRequired();

                entity.HasOne(pq => pq.role)
                      .WithMany()
                      .HasForeignKey(pq => pq.roleId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(pq => pq.permission)
                      .WithMany()
                      .HasForeignKey(pq => pq.permissionId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // UserPermissionUsage: composite key (userId + permissionId)
            modelBuilder.Entity<UserPermissionUsage>(entity =>
            {
                entity.HasKey(upu => new { upu.userId, upu.permissionId });

                entity.Property(upu => upu.usedCount)
                      .IsRequired();

                entity.Property(upu => upu.lastUsedAt)
                      .IsRequired();

                entity.HasOne(upu => upu.user)
                      .WithMany(u => u.permissionUsages)
                      .HasForeignKey(upu => upu.userId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(upu => upu.permission)
                      .WithMany(p => p.userPermissionUsages)
                      .HasForeignKey(upu => upu.permissionId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Seed dữ liệu cho Role
            modelBuilder.Entity<Role>().HasData(
                new Role { id = 1, name = "Quản Trị Viên", description = "Administrator with full permissions", uniqueGuid = new Guid("11111111-1111-1111-1111-111111111111"), createdAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), updatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Role { id = 2, name = "Người Dùng", description = "Regular user with limited permissions", uniqueGuid = new Guid("22222222-2222-2222-2222-222222222222"), createdAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), updatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Role { id = 3, name = "Vườn Ươm", description = "Nursery staff with specific permissions", uniqueGuid = new Guid("33333333-3333-3333-3333-333333333333"), createdAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), updatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Role { id = 4, name = "Người Dùng Bạc", description = "Entry-level user with basic feature access", uniqueGuid = new Guid("44444444-4444-4444-4444-444444444444"), createdAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), updatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Role { id = 5, name = "Người Dùng Vàng", description = "Intermediate user with extended feature access", uniqueGuid = new Guid("55555555-5555-5555-5555-555555555555"), createdAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), updatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Role { id = 6, name = "Người Dùng Kim Cương", description = "Advanced user with premium feature access", createdAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), updatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) }
                );

            // Seed dữ liệu cho Permission
            modelBuilder.Entity<Permission>().HasData(
                new Permission { id = 1, name = "createPlant", description = "Permission to create plants", uniqueGuid = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), createdAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), updatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Permission { id = 2, name = "editPlant", description = "Permission to edit plants", uniqueGuid = new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), createdAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), updatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Permission { id = 3, name = "deletePlant", description = "Permission to delete plants", uniqueGuid = new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), createdAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), updatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Permission { id = 4, name = "viewOrders", description = "Permission to view orders/payments", uniqueGuid = new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), createdAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), updatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Permission { id = 5, name = "manageUsers", description = "Permission to manage users", uniqueGuid = new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), createdAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), updatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Permission { id = 6, name = "sendMessages", description = "Permission to send messages", uniqueGuid = new Guid("ffffffff-ffff-ffff-ffff-ffffffffffff"), createdAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), updatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) }
            );

            // Seed dữ liệu cho RolePermission
            modelBuilder.Entity<RolePermission>().HasData(
                new RolePermission { roleId = 1, permissionId = 1 },
                new RolePermission { roleId = 1, permissionId = 2 },
                new RolePermission { roleId = 1, permissionId = 3 },
                new RolePermission { roleId = 1, permissionId = 4 },
                new RolePermission { roleId = 1, permissionId = 5 },
                new RolePermission { roleId = 1, permissionId = 6 },
                new RolePermission { roleId = 2, permissionId = 1 },
                new RolePermission { roleId = 2, permissionId = 4 },
                new RolePermission { roleId = 2, permissionId = 6 },
                new RolePermission { roleId = 3, permissionId = 1 },
                new RolePermission { roleId = 3, permissionId = 2 },
                new RolePermission { roleId = 3, permissionId = 4 }
            );

        }

        private static void ConfigureBaseEntity(ModelBuilder modelBuilder)
        {
            var entityTypes = modelBuilder.Model.GetEntityTypes()
                .Where(e => e.ClrType.IsSubclassOf(typeof(BaseEntity)));

            foreach (var entityType in entityTypes)
            {
                modelBuilder.Entity(entityType.ClrType, b =>
                {
                    b.Property(nameof(BaseEntity.id)).HasColumnType("int").IsRequired().ValueGeneratedOnAdd();
                    b.Property(nameof(BaseEntity.uniqueGuid)).HasColumnType("uniqueidentifier").IsRequired().HasDefaultValueSql("NEWID()");
                    b.Property(nameof(BaseEntity.createdAt)).HasColumnType("datetime2").IsRequired().HasDefaultValueSql("GETUTCDATE()");
                    b.Property(nameof(BaseEntity.updatedAt)).HasColumnType("datetime2").IsRequired().HasDefaultValueSql("GETUTCDATE()");
                    b.Property(nameof(BaseEntity.isDelete)).HasColumnType("bit").HasDefaultValue(false); // Không bắt buộc
                    b.Property(nameof(BaseEntity.deletedAt)).HasColumnType("datetime2"); // Không bắt buộc
                    b.HasIndex(nameof(BaseEntity.createdAt)).HasDatabaseName($"IX_{entityType.GetTableName()}_CreatedAt");
                });
            }
        }

        private static void ConfigureIndexes(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LogEntry>()
                .HasIndex(e => new { e.userId, e.createdAt })
                .HasDatabaseName("IX_LogEntry_UserCreated");

            modelBuilder.Entity<Payment>()
                .HasIndex(e => new { e.userId, e.status, e.createdAt })
                .HasDatabaseName("IX_Payment_UserStatusCreated");

            modelBuilder.Entity<UserMessage>()
                .HasIndex(e => new { e.receiverId, e.isRead, e.createdAt })
                .HasDatabaseName("IX_UserMessage_ReceiverReadCreated");

            modelBuilder.Entity<Plant>()
                .HasIndex(e => e.plantCategoryId)
                .HasDatabaseName("IX_Plant_CategoryId");

            modelBuilder.Entity<CareHistory>()
                .HasIndex(e => e.careDate)
                .HasDatabaseName("IX_CareHistory_CareDate");
        }

        public override int SaveChanges()
        {
            UpdateTimestamps();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateTimestamps();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateTimestamps()
        {
            var entries = ChangeTracker.Entries<BaseEntity>();

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.createdAt = DateTime.UtcNow;
                    entry.Entity.updatedAt = DateTime.UtcNow;
                    entry.Entity.isDelete = false;
                    entry.Entity.deletedAt = null;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.updatedAt = DateTime.UtcNow;
                    entry.Property(nameof(BaseEntity.createdAt)).IsModified = false;
                }
                else if (entry.State == EntityState.Deleted)
                {
                    entry.Entity.deletedAt = DateTime.UtcNow;
                    entry.Entity.isDelete = true;
                    entry.State = EntityState.Modified;
                }
            }
        }
    }
}