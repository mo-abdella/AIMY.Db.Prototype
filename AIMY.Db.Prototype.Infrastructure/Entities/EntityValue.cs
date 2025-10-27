using System;
using System.Collections.Generic;

namespace AIMY.Db.Prototype.Infrastructure.Entities;

public partial class EntityValue
{
    public Guid Id { get; set; }

    public string Value { get; set; } = null!;

    public Guid? EntityId { get; set; }

    public Guid? ParentId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? CreatedBy { get; set; }

    public Guid? UpdatedBy { get; set; }

    public string? ExternalId { get; set; }

    public virtual Entity? Entity { get; set; }

    public virtual ICollection<EntityValue> InverseParent { get; set; } = new List<EntityValue>();

    public virtual EntityValue? Parent { get; set; }

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
