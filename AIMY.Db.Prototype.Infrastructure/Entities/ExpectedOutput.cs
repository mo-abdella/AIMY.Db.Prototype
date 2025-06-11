using System;
using System.Collections.Generic;

namespace AIMY.Db.Prototype.Infrastructure.Entities;

public partial class ExpectedOutput
{
    public int Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? OutputType { get; set; }

    public int? AnalysisRuleId { get; set; }

    public virtual AnalysisRule? AnalysisRule { get; set; }

    public virtual ICollection<InteractionExpectedOutputResult> InteractionExpectedOutputResults { get; set; } = new List<InteractionExpectedOutputResult>();
}
