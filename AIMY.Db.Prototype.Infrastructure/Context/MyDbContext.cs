using System;
using System.Collections.Generic;
using AIMY.Db.Prototype.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace AIMY.Db.Prototype.Infrastructure.Context;

public partial class MyDbContext : DbContext
{
    public MyDbContext()
    {
    }

    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<App> Apps { get; set; }

    public virtual DbSet<AppPermission> AppPermissions { get; set; }

    public virtual DbSet<AppRolesPermission> AppRolesPermissions { get; set; }

    public virtual DbSet<Entity> Entities { get; set; }

    public virtual DbSet<EntityValue> EntityValues { get; set; }

    public virtual DbSet<FlywaySchemaHistory> FlywaySchemaHistories { get; set; }

    public virtual DbSet<PermissionModule> PermissionModules { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserApp> UserApps { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=aimyqa-postgresql-db.cjcuyweio8o4.us-east-1.rds.amazonaws.com;Database=aimy.user.management.rd;Username=postgres;Password=dsnWhRsVcaSXZR4DSHmGbaLn6;Port=5432").UseSnakeCaseNamingConvention();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<App>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("apps_pkey");

            entity.ToTable("apps", "app");

            entity.HasIndex(e => e.Key, "apps_key_key").IsUnique();

            entity.HasIndex(e => e.Name, "apps_name_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.Key)
                .HasMaxLength(100)
                .HasColumnName("key");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
        });

        modelBuilder.Entity<AppPermission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("app_permissions_pkey");

            entity.ToTable("app_permissions", "permission");

            entity.HasIndex(e => e.AppId, "idx_app_permissions_app_id");

            entity.HasIndex(e => e.PermissionModuleId, "idx_app_permissions_module_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Action)
                .HasMaxLength(50)
                .HasColumnName("action");
            entity.Property(e => e.AppId).HasColumnName("app_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.PermissionModuleId).HasColumnName("permission_module_id");
            entity.Property(e => e.PermissionName)
                .HasMaxLength(100)
                .HasColumnName("permission_name");
            entity.Property(e => e.Resource)
                .HasMaxLength(100)
                .HasColumnName("resource");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

            entity.HasOne(d => d.App).WithMany(p => p.AppPermissions)
                .HasForeignKey(d => d.AppId)
                .HasConstraintName("app_permissions_app_id_fkey");

            entity.HasOne(d => d.PermissionModule).WithMany(p => p.AppPermissions)
                .HasForeignKey(d => d.PermissionModuleId)
                .HasConstraintName("app_permissions_permission_module_id_fkey");
        });

        modelBuilder.Entity<AppRolesPermission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("app_roles_permissions_pkey");

            entity.ToTable("app_roles_permissions", "role");

            entity.HasIndex(e => e.AppPermissionId, "idx_app_roles_permissions_permission_id");

            entity.HasIndex(e => e.AppRoleId, "idx_app_roles_permissions_role_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AppPermissionId).HasColumnName("app_permission_id");
            entity.Property(e => e.AppRoleId).HasColumnName("app_role_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

            entity.HasOne(d => d.AppPermission).WithMany(p => p.AppRolesPermissions)
                .HasForeignKey(d => d.AppPermissionId)
                .HasConstraintName("app_roles_permissions_app_permission_id_fkey");

            entity.HasOne(d => d.AppRole).WithMany(p => p.AppRolesPermissions)
                .HasForeignKey(d => d.AppRoleId)
                .HasConstraintName("app_roles_permissions_app_role_id_fkey");
        });

        modelBuilder.Entity<Entity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("entities_pkey");

            entity.ToTable("entities", "app");

            entity.HasIndex(e => e.AppId, "idx_entities_app_id");

            entity.HasIndex(e => e.ParentId, "idx_entities_parent_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AppId).HasColumnName("app_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.ParentId).HasColumnName("parent_id");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

            entity.HasOne(d => d.App).WithMany(p => p.Entities)
                .HasForeignKey(d => d.AppId)
                .HasConstraintName("entities_app_id_fkey");

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent)
                .HasForeignKey(d => d.ParentId)
                .HasConstraintName("entities_parent_id_fkey");
        });

        modelBuilder.Entity<EntityValue>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("entity_values_pkey");

            entity.ToTable("entity_values", "app");

            entity.HasIndex(e => e.EntityId, "idx_entity_values_entity_id");

            entity.HasIndex(e => e.ParentId, "idx_entity_values_parent_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.EntityId).HasColumnName("entity_id");
            entity.Property(e => e.ParentId).HasColumnName("parent_id");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.Value)
                .HasMaxLength(255)
                .HasColumnName("value");

            entity.HasOne(d => d.Entity).WithMany(p => p.EntityValues)
                .HasForeignKey(d => d.EntityId)
                .HasConstraintName("entity_values_entity_id_fkey");

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent)
                .HasForeignKey(d => d.ParentId)
                .HasConstraintName("entity_values_parent_id_fkey");
        });

        modelBuilder.Entity<FlywaySchemaHistory>(entity =>
        {
            entity.HasKey(e => e.InstalledRank).HasName("flyway_schema_history_pk");

            entity.ToTable("flyway_schema_history", "auth");

            entity.HasIndex(e => e.Success, "flyway_schema_history_s_idx");

            entity.Property(e => e.InstalledRank)
                .ValueGeneratedNever()
                .HasColumnName("installed_rank");
            entity.Property(e => e.Checksum).HasColumnName("checksum");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .HasColumnName("description");
            entity.Property(e => e.ExecutionTime).HasColumnName("execution_time");
            entity.Property(e => e.InstalledBy)
                .HasMaxLength(100)
                .HasColumnName("installed_by");
            entity.Property(e => e.InstalledOn)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("installed_on");
            entity.Property(e => e.Script)
                .HasMaxLength(1000)
                .HasColumnName("script");
            entity.Property(e => e.Success).HasColumnName("success");
            entity.Property(e => e.Type)
                .HasMaxLength(20)
                .HasColumnName("type");
            entity.Property(e => e.Version)
                .HasMaxLength(50)
                .HasColumnName("version");
        });

        modelBuilder.Entity<PermissionModule>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("permission_modules_pkey");

            entity.ToTable("permission_modules", "permission");

            entity.HasIndex(e => e.AppId, "idx_permission_modules_app_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AppId).HasColumnName("app_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

            entity.HasOne(d => d.App).WithMany(p => p.PermissionModules)
                .HasForeignKey(d => d.AppId)
                .HasConstraintName("permission_modules_app_id_fkey");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("roles_pkey");

            entity.ToTable("roles", "role");

            entity.HasIndex(e => e.AppId, "idx_roles_app_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AppId).HasColumnName("app_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.Key)
                .HasMaxLength(100)
                .HasColumnName("key");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

            entity.HasOne(d => d.App).WithMany(p => p.Roles)
                .HasForeignKey(d => d.AppId)
                .HasConstraintName("roles_app_id_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.ToTable("users", "auth");

            entity.HasIndex(e => e.Email, "idx_users_email");

            entity.HasIndex(e => e.Email, "users_email_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.JobTitle)
                .HasMaxLength(255)
                .HasColumnName("job_title");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
        });

        modelBuilder.Entity<UserApp>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_apps_pkey");

            entity.ToTable("user_apps", "auth");

            entity.HasIndex(e => e.AppId, "idx_user_apps_app_id");

            entity.HasIndex(e => e.UserId, "idx_user_apps_user_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AppId).HasColumnName("app_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.App).WithMany(p => p.UserApps)
                .HasForeignKey(d => d.AppId)
                .HasConstraintName("user_apps_app_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.UserApps)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("user_apps_user_id_fkey");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_roles_pkey");

            entity.ToTable("user_roles", "auth");

            entity.HasIndex(e => e.AppId, "idx_user_roles_app_id");

            entity.HasIndex(e => e.EntityValueId, "idx_user_roles_entity_value_id");

            entity.HasIndex(e => e.AppRoleId, "idx_user_roles_role_id");

            entity.HasIndex(e => e.UserId, "idx_user_roles_user_id");

            entity.HasIndex(e => new { e.UserId, e.AppId, e.EntityValueId }, "uq_user_app_entity").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AppId).HasColumnName("app_id");
            entity.Property(e => e.AppRoleId).HasColumnName("app_role_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.EntityValueId).HasColumnName("entity_value_id");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.App).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.AppId)
                .HasConstraintName("user_roles_app_id_fkey");

            entity.HasOne(d => d.AppRole).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.AppRoleId)
                .HasConstraintName("user_roles_app_role_id_fkey");

            entity.HasOne(d => d.EntityValue).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.EntityValueId)
                .HasConstraintName("user_roles_entity_value_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("user_roles_user_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
