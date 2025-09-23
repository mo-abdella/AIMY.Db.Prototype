namespace AIMY.Db.Prototype.Infrastructure.Entities;

public partial class TeamSupportTicket
{
    public int Id { get; set; }

    public string? ProductName { get; set; }

    public string? CallerField { get; set; }

    public double? TimeSpent { get; set; }

    public string? CreatorName { get; set; }

    public string? Action { get; set; }

    public virtual UserInteraction IdNavigation { get; set; } = null!;
}
