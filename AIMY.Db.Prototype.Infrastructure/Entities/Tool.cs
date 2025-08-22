using System;
using System.Collections.Generic;

namespace AIMY.Db.Prototype.Infrastructure.Entities;

public partial class Tool
{
    public int Id { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public string Name { get; set; } = null!;

    public string? Url { get; set; }

    public string? ApiKey { get; set; }

    public byte[]? ApiSecret { get; set; }

    public string AccessType { get; set; } = null!;

    public virtual ICollection<Job> Jobs { get; set; } = new List<Job>();

    public virtual ICollection<ProductTool> ProductTools { get; set; } = new List<ProductTool>();

    public virtual ICollection<UserInteraction> UserInteractions { get; set; } = new List<UserInteraction>();
}
