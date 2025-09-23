namespace AIMY.Db.Prototype.Infrastructure.Entities;

public partial class ZendeskTicket
{
    public long TicketId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string Subject { get; set; } = null!;

    public string? Description { get; set; }

    public string? InternalStatus { get; set; }

    public string? PublicStatus { get; set; }

    public string? Assignee { get; set; }

    public long? RequesterId { get; set; }

    public long? SubmitterId { get; set; }

    public long? OrganizationId { get; set; }

    public string? TicketGroup { get; set; }

    public string? Priority { get; set; }

    public string? Type { get; set; }

    public string? Channel { get; set; }

    public long? FirstResponseTimeTaken { get; set; }

    public long? TimeSpent { get; set; }

    public List<string>? Tags { get; set; }

    public string? Url { get; set; }

    public DateTime? GeneratedTimestamp { get; set; }

    public long? JiraTicketId { get; set; }

    public int? UserInteractionId { get; set; }

    public string? ArticleId { get; set; }

    public string? OrganizationName { get; set; }

    public string? ResolutionSummary { get; set; }

    public virtual UserInteraction? UserInteraction { get; set; }
}
