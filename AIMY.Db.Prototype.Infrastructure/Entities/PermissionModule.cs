using System;
using System.Collections.Generic;

namespace AIMY.Db.Prototype.Infrastructure.Entities;

public partial class PermissionModule
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public Guid? AppId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? CreatedBy { get; set; }

    public Guid? UpdatedBy { get; set; }

    public virtual App? App { get; set; }

    public virtual ICollection<AppPermission> AppPermissions { get; set; } = new List<AppPermission>();
}
