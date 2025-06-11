using System;
using System.Collections.Generic;

namespace AIMY.Db.Prototype.Infrastructure.Entities;

public partial class AnalysisRuleProduct
{
    public int AnalysisRuleId { get; set; }

    public int ProductId { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public virtual AnalysisRule AnalysisRule { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
