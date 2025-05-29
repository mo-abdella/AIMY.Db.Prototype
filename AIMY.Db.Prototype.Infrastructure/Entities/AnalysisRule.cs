using AIMY.Db.Prototype.Infrastructure.Shared.Enums;

namespace AIMY.Db.Prototype.Infrastructure.Entities;

public partial class AnalysisRule
{
    public int Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public decimal? Weight { get; set; }

    public string? Type { get; set; }

    public InteractionTypeEnum? InteractionType { get; set; }


    public int? AnalysisRuleId { get; set; }

    public string? RuleInteractionType { get; set; }

    public virtual AnalysisRule? AnalysisRuleNavigation { get; set; }

    public virtual ICollection<ExpectedOutput> ExpectedOutputs { get; set; } = new List<ExpectedOutput>();

    public virtual ICollection<InteractionAnalysisRuleResult> InteractionAnalysisRuleResults { get; set; } = new List<InteractionAnalysisRuleResult>();

    public virtual ICollection<AnalysisRule> InverseAnalysisRuleNavigation { get; set; } = new List<AnalysisRule>();

    public int ProductId { get; set; }
    public virtual Product Product { get; set; } = null!;
    public virtual List<Product> Products { get; set; } = null!;
}
