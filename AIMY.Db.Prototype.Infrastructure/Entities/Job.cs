using System;
using System.Collections.Generic;

namespace AIMY.Db.Prototype.Infrastructure.Entities;

public partial class Job
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime LastRunTime { get; set; }

    public int Status { get; set; }

    public string? ResultMessage { get; set; }

    public int? ToolId { get; set; }

    public int? ProductId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public string UpdatedBy { get; set; } = null!;

    public virtual Product? Product { get; set; }

    public virtual Tool? Tool { get; set; }
}
