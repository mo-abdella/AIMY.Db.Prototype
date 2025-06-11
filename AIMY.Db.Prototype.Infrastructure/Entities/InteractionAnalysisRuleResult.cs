using System;
using System.Collections.Generic;

namespace AIMY.Db.Prototype.Infrastructure.Entities;

public partial class InteractionAnalysisRuleResult
{
    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime UpdatedAt { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public string? Type { get; set; }

    public decimal? Score { get; set; }

    public int UserInteractionId { get; set; }

    public int AnalysisRuleId { get; set; }

    public string? Comment { get; set; }

    public decimal? QaScore { get; set; }

    public string? QaReview { get; set; }

    public string? Reason { get; set; }

    public virtual AnalysisRule AnalysisRule { get; set; } = null!;

    public virtual UserInteraction UserInteraction { get; set; } = null!;
}
