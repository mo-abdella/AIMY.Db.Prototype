namespace AIMY.Db.Prototype.Infrastructure.Entities;

public partial class ChatMessage
{
    public int Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public string? SentBy { get; set; }

    public string? ReceivedBy { get; set; }

    public string? Content { get; set; }

    public int UserInteractionId { get; set; }

    public virtual UserInteraction UserInteraction { get; set; } = null!;
}
