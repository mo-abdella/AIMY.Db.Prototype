using System;
using System.Collections.Generic;

namespace AIMY.Db.Prototype.Infrastructure.Entities;

public partial class Role
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Key { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? CreatedBy { get; set; }

    public Guid? UpdatedBy { get; set; }

    public bool IsSystemRole { get; set; }

    public virtual ICollection<AppRolesPermission> AppRolesPermissions { get; set; } = new List<AppRolesPermission>();

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
