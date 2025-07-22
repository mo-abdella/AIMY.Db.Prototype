using System;
using System.Collections.Generic;

namespace AIMY.Db.Prototype.Infrastructure.Entities;

public partial class EntityValue
{
    public int Id { get; set; }

    public string Value { get; set; } = null!;

    public int? EntityId { get; set; }

    public int? ParentId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public virtual Entity? Entity { get; set; }

    public virtual ICollection<EntityValue> InverseParent { get; set; } = new List<EntityValue>();

    public virtual EntityValue? Parent { get; set; }

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
