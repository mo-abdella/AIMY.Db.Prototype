using System;
using System.Collections.Generic;

namespace AIMY.Db.Prototype.Infrastructure.Entities;

public partial class Entity
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int? AppId { get; set; }

    public int? ParentId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public virtual App? App { get; set; }

    public virtual ICollection<EntityValue> EntityValues { get; set; } = new List<EntityValue>();

    public virtual ICollection<Entity> InverseParent { get; set; } = new List<Entity>();

    public virtual Entity? Parent { get; set; }
}
