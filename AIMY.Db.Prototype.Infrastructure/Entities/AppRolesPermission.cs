using System;
using System.Collections.Generic;

namespace AIMY.Db.Prototype.Infrastructure.Entities;

public partial class AppRolesPermission
{
    public Guid Id { get; set; }

    public Guid? AppRoleId { get; set; }

    public Guid? AppPermissionId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? CreatedBy { get; set; }

    public Guid? UpdatedBy { get; set; }

    public virtual AppPermission? AppPermission { get; set; }

    public virtual Role? AppRole { get; set; }
}
