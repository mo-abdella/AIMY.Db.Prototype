using System;
using System.Collections.Generic;

namespace AIMY.Db.Prototype.Infrastructure.Entities;

public partial class AppPermission
{
    public int Id { get; set; }

    public string PermissionName { get; set; } = null!;

    public string Resource { get; set; } = null!;

    public string Action { get; set; } = null!;

    public int? AppId { get; set; }

    public int? PermissionModuleId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public virtual App? App { get; set; }

    public virtual ICollection<AppRolesPermission> AppRolesPermissions { get; set; } = new List<AppRolesPermission>();

    public virtual PermissionModule? PermissionModule { get; set; }
}
