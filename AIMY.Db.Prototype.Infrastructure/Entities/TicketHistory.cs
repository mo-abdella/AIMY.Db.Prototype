namespace AIMY.Db.Prototype.Infrastructure.Entities;

public partial class TicketHistory
{
    public int Id { get; set; }

    public int UserInteractionId { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string Description { get; set; } = null!;

    public virtual UserInteraction UserInteraction { get; set; } = null!;
}
