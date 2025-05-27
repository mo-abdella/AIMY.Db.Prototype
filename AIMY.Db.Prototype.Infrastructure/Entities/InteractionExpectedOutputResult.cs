namespace AIMY.Db.Prototype.Infrastructure.Entities;

public partial class InteractionExpectedOutputResult
{
    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime UpdatedAt { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public bool? AiOrQa { get; set; }

    public string OutputType { get; set; } = null!;

    public string? TextOutput { get; set; }

    public decimal? NumberOutput { get; set; }

    public bool? BooleanOutput { get; set; }

    public int UserInteractionId { get; set; }

    public int ExpectedOutputId { get; set; }

    public virtual ExpectedOutput ExpectedOutput { get; set; } = null!;

    public virtual UserInteraction UserInteraction { get; set; } = null!;
}
