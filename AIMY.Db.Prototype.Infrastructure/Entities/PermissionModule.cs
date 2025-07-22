using System;
using System.Collections.Generic;

namespace AIMY.Db.Prototype.Infrastructure.Entities;

public partial class PermissionModule
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int? AppId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public virtual App? App { get; set; }

    public virtual ICollection<AppPermission> AppPermissions { get; set; } = new List<AppPermission>();
}
