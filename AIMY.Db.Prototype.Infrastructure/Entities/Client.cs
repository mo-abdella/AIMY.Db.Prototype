using System;
using System.Collections.Generic;

namespace AIMY.Db.Prototype.Infrastructure.Entities;

public partial class Client
{
    public int Id { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public string Name { get; set; } = null!;

    public string Key { get; set; } = null!;

    public int OrganizationId { get; set; }

    public virtual Organization Organization { get; set; } = null!;

    public virtual ICollection<ProductTool> ProductTools { get; set; } = new List<ProductTool>();

    public virtual ICollection<ProductUser> ProductUsers { get; set; } = new List<ProductUser>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
