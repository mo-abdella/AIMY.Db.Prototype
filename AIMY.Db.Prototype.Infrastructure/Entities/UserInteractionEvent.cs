namespace AIMY.Db.Prototype.Infrastructure.Entities;

public partial class UserInteractionEvent
{
    public int Id { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public string Title { get; set; } = null!;

    public string Message { get; set; } = null!;

    public int UserInteractionId { get; set; }

    public string? ServiceName { get; set; }

    public string? OperationName { get; set; }

    public virtual UserInteraction UserInteraction { get; set; } = null!;
}
