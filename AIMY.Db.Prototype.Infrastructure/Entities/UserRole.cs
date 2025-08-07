using System;
using System.Collections.Generic;

namespace AIMY.Db.Prototype.Infrastructure.Entities;

public partial class UserRole
{
    public Guid Id { get; set; }

    public Guid? UserId { get; set; }

    public Guid? EntityValueId { get; set; }

    public Guid? AppRoleId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? CreatedBy { get; set; }

    public Guid? UpdatedBy { get; set; }

    public Guid? AppId { get; set; }

    public virtual App? App { get; set; }

    public virtual Role? AppRole { get; set; }

    public virtual EntityValue? EntityValue { get; set; }

    public virtual User? User { get; set; }
}
