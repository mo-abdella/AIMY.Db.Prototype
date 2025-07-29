using System;
using System.Collections.Generic;

namespace AIMY.Db.Prototype.Infrastructure.Entities;

public partial class Entity
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public Guid? AppId { get; set; }

    public Guid? ParentId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? CreatedBy { get; set; }

    public Guid? UpdatedBy { get; set; }

    public virtual App? App { get; set; }

    public virtual ICollection<EntityValue> EntityValues { get; set; } = new List<EntityValue>();

    public virtual ICollection<Entity> InverseParent { get; set; } = new List<Entity>();

    public virtual Entity? Parent { get; set; }
}
