using Microsoft.EntityFrameworkCore;
using ProjectGreenLens.Models.Entities;

namespace ProjectGreenLens.Infrastructure.dbContext
{
    public class GreenLensDbContext : DbContext
    {
        public GreenLensDbContext(DbContextOptions<GreenLensDbContext> options)
            : base(options)
        {
        }

        // DbSets
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<RolePermission> RolePermissions { get; set; } = null!;
        public DbSet<Permission> Permissions { get; set; } = null!;
        public DbSet<UserPermissionUsage> UserPermissionUsages { get; set; } = null!;
        public DbSet<UserProfile> UserProfiles { get; set; } = null!;
        public DbSet<UserToken> UserTokens { get; set; } = null!;
        public DbSet<AIAdvicesLogs> AIAdvicesLogs { get; set; } = null!;
        public DbSet<Payment> Payments { get; set; } = null!;
        public DbSet<UserPlant> UserPlants { get; set; } = null!;
        public DbSet<Plant> Plants { get; set; } = null!;
        public DbSet<PlantCategory> PlantCategories { get; set; } = null!;
        public DbSet<Photo> Photos { get; set; } = null!;
        public DbSet<Guide> Guides { get; set; } = null!;
        public DbSet<CareHistory> CareHistories { get; set; } = null!;
        public DbSet<CaresSchedules> CareSchedules { get; set; } = null!;
        public DbSet<Disease> Diseases { get; set; } = null!;
        public DbSet<UserPlantDisease> UserPlantDiseases { get; set; } = null!;
        public DbSet<Post> Posts { get; set; } = null!;
        public DbSet<Comment> Comments { get; set; } = null!;
        public DbSet<Like> Likes { get; set; } = null!;
        public DbSet<Notification> Notifications { get; set; } = null!;
        public DbSet<ContactMessage> ContactMessages { get; set; } = null!;
        public DbSet<SavedPlant> SavedPlants { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // BaseEntity config
            ConfigureBaseEntity(modelBuilder);

            // Composite Key for RolePermission
            modelBuilder.Entity<RolePermission>().HasKey(rp => new { rp.roleId, rp.permissionId });

            // Composite Key for UserPermissionUsage
            modelBuilder.Entity<UserPermissionUsage>().HasKey(upu => new { upu.userId, upu.permissionId });

            // Composite Key for UserPlantDisease
            modelBuilder.Entity<UserPlantDisease>().HasKey(upd => new { upd.userPlantId, upd.diseaseId });

            // Relationships & navigation properties

            // User - Role
            modelBuilder.Entity<User>()
                .HasOne(u => u.role)
                .WithMany(r => r.users)
                .HasForeignKey(u => u.roleId);

            // Role - RolePermission
            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.role)
                .WithMany(r => r.rolePermissions)
                .HasForeignKey(rp => rp.roleId);
            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.permission)
                .WithMany(p => p.rolePermissions)
                .HasForeignKey(rp => rp.permissionId);

            // Permission - UserPermissionUsage
            modelBuilder.Entity<UserPermissionUsage>()
                .HasOne(upu => upu.permission)
                .WithMany(p => p.userPermissionUsages)
                .HasForeignKey(upu => upu.permissionId);
            // User - UserPermissionUsage
            modelBuilder.Entity<UserPermissionUsage>()
                .HasOne(upu => upu.user)
                .WithMany(u => u.permissionUsages)
                .HasForeignKey(upu => upu.userId);

            // UserProfile
            modelBuilder.Entity<UserProfile>()
                .HasOne(up => up.user)
                .WithOne(u => u.userProfile)
                .HasForeignKey<UserProfile>(up => up.userId);

            // UserToken
            modelBuilder.Entity<UserToken>()
                .HasOne(ut => ut.user)
                .WithMany(u => u.userTokens)
                .HasForeignKey(ut => ut.userId);

            // AIAdvicesLogs
            modelBuilder.Entity<AIAdvicesLogs>()
                .HasOne(a => a.user)
                .WithMany(u => u.aiAdvicesLogs)
                .HasForeignKey(a => a.userId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<AIAdvicesLogs>()
                .HasOne(a => a.userPlant)
                .WithMany(up => up.aiAdvicesLogs)
                .HasForeignKey(a => a.userPlantId)
                .OnDelete(DeleteBehavior.Restrict);

            // Payment
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.user)
                .WithMany(u => u.payments)
                .HasForeignKey(p => p.userId);

            // UserPlant
            modelBuilder.Entity<UserPlant>()
                .HasOne(up => up.user)
                .WithMany(u => u.userPlants)
                .HasForeignKey(up => up.userId);
            modelBuilder.Entity<UserPlant>()
                .HasOne(up => up.plant)
                .WithMany(p => p.userPlants)
                .HasForeignKey(up => up.plantId);

            // PlantCategory - Plant
            modelBuilder.Entity<Plant>()
                .HasOne(p => p.plantCategory)
                .WithMany(pc => pc.plants)
                .HasForeignKey(p => p.plantCategoryId);

            // Photo
            modelBuilder.Entity<Photo>()
                .HasOne(ph => ph.plant)
                .WithMany(p => p.photos)
                .HasForeignKey(ph => ph.plantId);

            // Guide
            modelBuilder.Entity<Guide>()
                .HasOne(g => g.plant)
                .WithMany(p => p.guides)
                .HasForeignKey(g => g.plantId);

            // CareHistory
            modelBuilder.Entity<CareHistory>()
                .HasOne(ch => ch.userPlant)
                .WithMany(up => up.careHistories)
                .HasForeignKey(ch => ch.userPlantId);

            // CaresSchedules
            modelBuilder.Entity<CaresSchedules>()
                .HasOne(cs => cs.userPlant)
                .WithMany(up => up.careSchedules)
                .HasForeignKey(cs => cs.userPlantId);

            // Disease
            modelBuilder.Entity<UserPlantDisease>()
                .HasOne(upd => upd.userPlant)
                .WithMany(up => up.userPlantDiseases)
                .HasForeignKey(upd => upd.userPlantId);
            modelBuilder.Entity<UserPlantDisease>()
                .HasOne(upd => upd.disease)
                .WithMany(d => d.userPlantDiseases)
                .HasForeignKey(upd => upd.diseaseId);

            // Post
            modelBuilder.Entity<Post>()
                .HasOne(p => p.author)
                .WithMany(u => u.posts)
                .HasForeignKey(p => p.authorId);

            // Comment
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.post)
                .WithMany(p => p.comments)
                .HasForeignKey(c => c.postId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.user)
                .WithMany(u => u.comments)
                .HasForeignKey(c => c.userId)
                .OnDelete(DeleteBehavior.Restrict);
            // Like
            modelBuilder.Entity<Like>()
                .HasOne(l => l.user)
                .WithMany(u => u.likes)
                .HasForeignKey(l => l.userId);

            modelBuilder.Entity<Like>()
                .HasOne(l => l.post)
                .WithMany(p => p.likes)
                .HasForeignKey(l => l.postId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Like>()
                .HasOne(l => l.comment)
                .WithMany(c => c.likes)
                .HasForeignKey(l => l.commentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Notification
            modelBuilder.Entity<Notification>()
                .HasOne(n => n.user)
                .WithMany(u => u.notifications)
                .HasForeignKey(n => n.userId);

            // ContactMessage
            modelBuilder.Entity<ContactMessage>()
                .HasOne(cm => cm.user)
                .WithMany(u => u.contactMessages)
                .HasForeignKey(cm => cm.userId);

            // SavedPlant
            modelBuilder.Entity<SavedPlant>()
                .HasOne(sp => sp.user)
                .WithMany(u => u.savedPlants)
                .HasForeignKey(sp => sp.userId);
            modelBuilder.Entity<SavedPlant>()
                .HasOne(sp => sp.plant)
                .WithMany(p => p.savedPlants)
                .HasForeignKey(sp => sp.plantId);

            // Indexes
            ConfigureIndexes(modelBuilder);
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
                    b.Property(nameof(BaseEntity.isDelete)).HasColumnType("bit").HasDefaultValue(false);
                    b.Property(nameof(BaseEntity.deletedAt)).HasColumnType("datetime2");
                    b.HasIndex(nameof(BaseEntity.createdAt)).HasDatabaseName($"IX_{entityType.GetTableName()}_CreatedAt");
                });
            }
        }

        private static void ConfigureIndexes(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Payment>()
                .HasIndex(e => new { e.userId, e.status, e.createdAt })
                .HasDatabaseName("IX_Payment_UserStatusCreated");

            modelBuilder.Entity<UserPlant>()
                .HasIndex(e => e.plantId)
                .HasDatabaseName("IX_UserPlant_PlantId");

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