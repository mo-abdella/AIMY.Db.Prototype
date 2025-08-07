using System;
using System.Collections.Generic;

namespace AIMY.Db.Prototype.Infrastructure.Entities;

public partial class App
{
    public Guid Id { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? CreatedBy { get; set; }

    public Guid? UpdatedBy { get; set; }

    public string Name { get; set; } = null!;

    public string Key { get; set; } = null!;

    public string Url { get; set; } = null!;

    public virtual ICollection<AppPermission> AppPermissions { get; set; } = new List<AppPermission>();

    public virtual ICollection<Entity> Entities { get; set; } = new List<Entity>();

    public virtual ICollection<PermissionModule> PermissionModules { get; set; } = new List<PermissionModule>();

    public virtual ICollection<UserApp> UserApps { get; set; } = new List<UserApp>();

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
