using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using ProjectGreenLens.Models.Entities;

namespace ProjectGreenLens.Infrastructure.dbContext
{
    public class GreenLensDbContext : DbContext
    {
        public GreenLensDbContext(DbContextOptions<GreenLensDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Suppress the pending model changes warning
            optionsBuilder.ConfigureWarnings(warnings =>
                warnings.Ignore(RelationalEventId.PendingModelChangesWarning));

            base.OnConfiguring(optionsBuilder);
        }

        // DbSets
        public DbSet<User> users { get; set; }
        public DbSet<Role> roles { get; set; }
        public DbSet<Permission> permissions { get; set; }
        public DbSet<RolePermission> rolePermissions { get; set; }
        public DbSet<UserProfile> userProfiles { get; set; }
        public DbSet<UserMessage> userMessages { get; set; }
        public DbSet<LogEntry> logEntries { get; set; }
        public DbSet<Payment> payments { get; set; }
        public DbSet<NurseryProfile> nurseryProfiles { get; set; }
        public DbSet<UserPlant> userPlants { get; set; }
        public DbSet<Plant> plants { get; set; }
        public DbSet<PlantCategory> plantCategories { get; set; }
        public DbSet<ArModel> arModels { get; set; }
        public DbSet<Photo> photos { get; set; }
        public DbSet<Guide> guides { get; set; }
        public DbSet<AIAdvicesLogs> aiAdvicesLogs { get; set; }
        public DbSet<CaresSchedules> caresSchedules { get; set; }
        public DbSet<PlantDisease> plantDiseases { get; set; }
        public DbSet<CareHistory> careHistories { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure BaseEntity properties for all entities
            ConfigureBaseEntity(modelBuilder);

            // Configure User
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.username).IsUnique();
                entity.HasIndex(e => e.email).IsUnique();

                entity.Property(e => e.username)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.passwordHash)
                    .IsRequired();

                // Configure relationship with Role
                entity.HasOne(u => u.role)
                    .WithMany()
                    .HasForeignKey("roleId")
                    .OnDelete(DeleteBehavior.Restrict);

                // Configure one-to-one relationship with UserProfile
                entity.HasOne(u => u.userProfile)
                    .WithOne(up => up.user)
                    .HasForeignKey<UserProfile>(up => up.userId)
                    .OnDelete(DeleteBehavior.Cascade);

                // Configure one-to-one relationship with NurseryProfile
                entity.HasOne(u => u.nurseryProfile)
                    .WithOne(np => np.user)
                    .HasForeignKey<NurseryProfile>(np => np.userId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Cấu hình bảng Role
            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasIndex(r => r.name).IsUnique();

                entity.Property(r => r.name)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(r => r.description)
                      .IsRequired()
                      .HasMaxLength(500);
            });

            // Cấu hình bảng Permission
            modelBuilder.Entity<Permission>(entity =>
            {
                entity.HasIndex(p => p.name).IsUnique();

                entity.Property(p => p.name)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(p => p.description)
                      .IsRequired()
                      .HasMaxLength(500);
            });

            // Cấu hình bảng RolePermission (bảng trung gian)
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


            // Configure UserProfile
            modelBuilder.Entity<UserProfile>(entity =>
            {
                entity.Property(e => e.fullName)
                    .HasMaxLength(100);

                entity.Property(e => e.bio)
                    .HasMaxLength(1000);

                entity.Property(e => e.location)
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<UserMessage>(entity =>
            {
                entity.Property(e => e.messageText)
                    .IsRequired()
                    .HasMaxLength(2000);

                entity.Property(e => e.isRead)
                    .HasDefaultValue(false);

                // Configure relationships
                entity.HasOne(um => um.sender)
                    .WithMany(u => u.sentMessages)
                    .HasForeignKey(um => um.senderId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(um => um.receiver)
                    .WithMany(u => u.receivedMessages)
                    .HasForeignKey(um => um.receiverId)
                    .OnDelete(DeleteBehavior.Restrict);

                // SỬA THÀNH CÁCH MỚI - không deprecated
                entity.ToTable(tb => tb.HasCheckConstraint("CK_UserMessage_DifferentUsers", "\"senderId\" != \"receiverId\""));
            });

            // Configure LogEntry
            modelBuilder.Entity<LogEntry>(entity =>
            {
                entity.Property(e => e.action)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(le => le.user)
                    .WithMany(u => u.logEntries)
                    .HasForeignKey(le => le.userId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configure Payment
            modelBuilder.Entity<Payment>(entity =>
            {
                // ... các property configurations ...

                entity.HasOne(p => p.user)
                    .WithMany(u => u.payments)
                    .HasForeignKey(p => p.userId)
                    .OnDelete(DeleteBehavior.Cascade);

                // SỬA THÀNH CÁCH MỚI
                entity.ToTable(tb => tb.HasCheckConstraint("CK_Payment_PositiveAmount", "amount > 0"));
            });

            // Configure NurseryProfile
            modelBuilder.Entity<NurseryProfile>(entity =>
            {
                entity.Property(e => e.nurseryName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.address)
                    .HasMaxLength(200);

                entity.Property(e => e.description)
                    .HasMaxLength(1000);
            });

            // Configure Plant
            modelBuilder.Entity<Plant>(entity =>
            {
                entity.HasIndex(e => e.scientificName).IsUnique();

                entity.Property(e => e.scientificName)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.commonName)
                    .HasMaxLength(150);

                entity.Property(e => e.description)
                    .HasMaxLength(2000);

                entity.Property(e => e.careInstructions)
                    .HasMaxLength(2000);

                entity.Property(e => e.wateringFrequency)
                    .HasMaxLength(100);

                entity.Property(e => e.lightRequirement)
                    .HasMaxLength(100);

                entity.Property(e => e.soilType)
                    .HasMaxLength(100);

                entity.Property(e => e.averagePrice)
                    .HasColumnType("decimal(18,2)");

                entity.Property(e => e.isIndoor)
                    .HasDefaultValue(true);

                // Configure relationship with PlantCategory
                entity.HasOne(p => p.plantCategory)
                    .WithMany(pc => pc.plants)
                    .HasForeignKey(p => p.plantCategoryId)
                    .OnDelete(DeleteBehavior.SetNull);

                // Configure one-to-one relationship with ArModel
                entity.HasOne(p => p.arModel)
                    .WithOne(am => am.plant)
                    .HasForeignKey<ArModel>(am => am.plantId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configure PlantCategory
            modelBuilder.Entity<PlantCategory>(entity =>
            {
                entity.HasIndex(e => e.name).IsUnique();

                entity.Property(e => e.name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.description)
                    .HasMaxLength(500);
            });

            // Configure UserPlant
            modelBuilder.Entity<UserPlant>(entity =>
            {
                entity.Property(e => e.nickname)
                    .HasMaxLength(100);

                entity.Property(e => e.notes)
                    .HasMaxLength(2000);

                entity.Property(e => e.healthStatus)
                    .HasMaxLength(50)
                    .HasDefaultValue(1);

                entity.Property(e => e.currentLocation)
                    .HasMaxLength(500);

                entity.Property(e => e.isActive)
                    .HasDefaultValue(true);

                // Configure relationships
                entity.HasOne(up => up.user)
                    .WithMany(u => u.userPlants)
                    .HasForeignKey(up => up.userId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(up => up.plant)
                    .WithMany(p => p.userPlants)
                    .HasForeignKey(up => up.plantId)
                    .OnDelete(DeleteBehavior.Cascade);

                // Unique constraint for user-plant combination
                entity.HasIndex(e => new { e.userId, e.plantId, e.acquiredDate })
                    .HasDatabaseName("IX_UserPlant_Unique");
            });

            // Configure ArModel
            modelBuilder.Entity<ArModel>(entity =>
            {
                entity.Property(e => e.modelUrl)
                    .IsRequired();

                entity.Property(e => e.fileFormat)
                    .HasMaxLength(10);
            });

            // Configure Photo
            modelBuilder.Entity<Photo>(entity =>
            {
                entity.Property(e => e.photoUrl)
                    .IsRequired();

                entity.Property(e => e.caption)
                    .HasMaxLength(250);

                entity.HasOne(p => p.plant)
                    .WithMany(pl => pl.photos)
                    .HasForeignKey(p => p.plantId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configure Guide
            modelBuilder.Entity<Guide>(entity =>
            {
                entity.Property(e => e.title)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.content)
                    .HasMaxLength(5000);

                entity.HasOne(g => g.plant)
                    .WithMany(p => p.guides)
                    .HasForeignKey(g => g.plantId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configure AIAdvicesLogs
            modelBuilder.Entity<AIAdvicesLogs>(entity =>
            {
                entity.Property(e => e.adviceText)
                    .HasMaxLength(2000);

                entity.HasOne(aal => aal.userPlant)
                    .WithMany(up => up.aiAdvicesLogs)
                    .HasForeignKey(aal => aal.userPlantId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configure CaresSchedules
            modelBuilder.Entity<CaresSchedules>(entity =>
            {
                entity.Property(e => e.taskName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.frequency)
                    .HasMaxLength(50);

                entity.Property(e => e.notes)
                    .HasMaxLength(500);

                entity.Property(e => e.isCompleted)
                    .HasDefaultValue(false);

                entity.HasOne(cs => cs.userPlant)
                    .WithMany(up => up.careSchedules)
                    .HasForeignKey(cs => cs.userPlantId)
                    .OnDelete(DeleteBehavior.Cascade);

                // Index for efficient querying
                entity.HasIndex(e => new { e.userPlantId, e.scheduleTime, e.isCompleted })
                    .HasDatabaseName("IX_CaresSchedules_Query");
            });

            // Configure PlantDisease
            modelBuilder.Entity<PlantDisease>(entity =>
            {
                entity.HasIndex(e => e.name).IsUnique();

                entity.Property(e => e.name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.symptoms)
                    .HasMaxLength(1000);

                entity.Property(e => e.treatment)
                    .HasMaxLength(1000);

                entity.Property(e => e.prevention)
                    .HasMaxLength(1000);
            });

            // Configure CareHistory
            modelBuilder.Entity<CareHistory>(entity =>
            {
                entity.Property(e => e.careType)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.notes)
                    .HasMaxLength(500);

                entity.Property(e => e.quantity)
                    .HasMaxLength(100);

                entity.Property(e => e.effectiveness)
                    .HasDefaultValue(null);

                entity.HasOne(ch => ch.userPlant)
                    .WithMany(up => up.careHistories)
                    .HasForeignKey(ch => ch.userPlantId)
                    .OnDelete(DeleteBehavior.Cascade);

                // Index for efficient querying by date and type
                entity.HasIndex(e => new { e.userPlantId, e.careType, e.careDate })
                    .HasDatabaseName("IX_CareHistory_Query");
            });

            // Seeding initial data

            modelBuilder.Entity<Role>().HasData(
    new Role { id = 1, name = "admin", description = "Administrator with full permissions" },
    new Role { id = 2, name = "user", description = "Regular user with limited permissions" },
    new Role { id = 3, name = "nursery", description = "Nursery staff with specific permissions" }
);

            // Seed permissions
            modelBuilder.Entity<Permission>().HasData(
                new Permission { id = 1, name = "createPlant", description = "Permission to create plants" },
                new Permission { id = 2, name = "editPlant", description = "Permission to edit plants" },
                new Permission { id = 3, name = "deletePlant", description = "Permission to delete plants" },
                new Permission { id = 4, name = "viewOrders", description = "Permission to view orders/payments" },
                new Permission { id = 5, name = "manageUsers", description = "Permission to manage users" },
                new Permission { id = 6, name = "sendMessages", description = "Permission to send messages" }
            );

            // Seed rolePermissions (mapping roles to permissions)
            modelBuilder.Entity<RolePermission>().HasData(
                // admin has all permissions
                new RolePermission { roleId = 1, permissionId = 1 },
                new RolePermission { roleId = 1, permissionId = 2 },
                new RolePermission { roleId = 1, permissionId = 3 },
                new RolePermission { roleId = 1, permissionId = 4 },
                new RolePermission { roleId = 1, permissionId = 5 },
                new RolePermission { roleId = 1, permissionId = 6 },

                // user permissions (limited)
                new RolePermission { roleId = 2, permissionId = 1 },
                new RolePermission { roleId = 2, permissionId = 6 },
                new RolePermission { roleId = 2, permissionId = 4 },

                // nursery permissions
                new RolePermission { roleId = 3, permissionId = 1 },
                new RolePermission { roleId = 3, permissionId = 2 },
                new RolePermission { roleId = 3, permissionId = 4 }
            );

            // Configure indexes for better performance
            ConfigureIndexes(modelBuilder);
        }

        private static void ConfigureBaseEntity(ModelBuilder modelBuilder)
        {
            // Configure all entities inheriting from BaseEntity
            var entityTypes = modelBuilder.Model.GetEntityTypes()
                .Where(e => e.ClrType.IsSubclassOf(typeof(BaseEntity)));

            foreach (var entityType in entityTypes)
            {
                modelBuilder.Entity(entityType.ClrType, b =>
                {
                    b.Property(nameof(BaseEntity.id))
                        .IsRequired()
                        .ValueGeneratedOnAdd();
                    // PostgreSQL UUID generation
                    b.Property(nameof(BaseEntity.uniqueGuid))
                        .HasDefaultValueSql("gen_random_uuid()");

                    // PostgreSQL timestamp functions
                    b.Property(nameof(BaseEntity.createdAt))
                        .HasDefaultValueSql("NOW()");

                    b.Property(nameof(BaseEntity.updatedAt))
                        .HasDefaultValueSql("NOW()");

                    // Index for created and updated timestamps
                    b.HasIndex(nameof(BaseEntity.createdAt))
                        .HasDatabaseName($"IX_{entityType.GetTableName()}_CreatedAt");
                });
            }
        }

        private static void ConfigureIndexes(ModelBuilder modelBuilder)
        {
            // Additional performance indexes
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
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.updatedAt = DateTime.UtcNow;
                    // Prevent createdAt from being updated
                    entry.Property(nameof(BaseEntity.createdAt)).IsModified = false;
                }
            }
        }
    }
}
