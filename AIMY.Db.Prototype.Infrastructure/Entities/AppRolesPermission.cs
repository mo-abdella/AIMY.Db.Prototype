using System;
using System.Collections.Generic;

namespace AIMY.Db.Prototype.Infrastructure.Entities;

public partial class AppRolesPermission
{
    public int Id { get; set; }

    public int? AppRoleId { get; set; }

    public int? AppPermissionId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public virtual AppPermission? AppPermission { get; set; }

    public virtual Role? AppRole { get; set; }
}
