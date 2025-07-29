using System;
using System.Collections.Generic;

namespace AIMY.Db.Prototype.Infrastructure.Entities;

public partial class AppPermission
{
    public Guid Id { get; set; }

    public string PermissionName { get; set; } = null!;

    public string Resource { get; set; } = null!;

    public string Action { get; set; } = null!;

    public Guid? AppId { get; set; }

    public Guid? PermissionModuleId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? CreatedBy { get; set; }

    public Guid? UpdatedBy { get; set; }

    public virtual App? App { get; set; }

    public virtual ICollection<AppRolesPermission> AppRolesPermissions { get; set; } = new List<AppRolesPermission>();

    public virtual PermissionModule? PermissionModule { get; set; }
}
