namespace AIMY.Db.Prototype.Infrastructure.Entities;

public partial class UserInteraction
{
    public int Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public string? AgentEmail { get; set; }

    public int? AgentId { get; set; }

    public string? Status { get; set; }

    public string? TextContent { get; set; }

    public int? CallDuration { get; set; }

    public string? CallDirection { get; set; }

    public int? CallHoldDuration { get; set; }

    public string? TicketNumber { get; set; }

    public string? TicketDescription { get; set; }

    public string? TicketTitle { get; set; }

    public string? InteractionType { get; set; }

    public int ToolId { get; set; }

    public decimal? SentimentAnalysisScoreAi { get; set; }

    public decimal? SentimentAnalysisScoreQa { get; set; }

    public decimal? MistakeAnalysisScoreAi { get; set; }

    public decimal? MistakeAnalysisScoreQa { get; set; }

    public string? SemanticAnalysisAi { get; set; }

    public string? SemanticAnalysisQa { get; set; }

    public string? TicketGroup { get; set; }

    public List<string>? TicketTags { get; set; }

    public string? TicketTypeName { get; set; }

    public long? FirstResponseTimeTaken { get; set; }

    public DateTime? ExternalDateCreated { get; set; }

    public DateTime? ExternalDateClosed { get; set; }

    public string? ExternalStatus { get; set; }

    public string? ExternalId { get; set; }

    public string? AgentName { get; set; }

    public int ProductId { get; set; }

    public string? SatisfactionScore { get; set; }

    public string? OtherParty { get; set; }

    public string? ExternalInteractionUrl { get; set; }

    public string? InteractionUrl { get; set; }

    public virtual ICollection<ChatMessage> ChatMessages { get; set; } = new List<ChatMessage>();

    public virtual ICollection<InteractionAnalysisRuleResult> InteractionAnalysisRuleResults { get; set; } = new List<InteractionAnalysisRuleResult>();

    public virtual ICollection<InteractionExpectedOutputResult> InteractionExpectedOutputResults { get; set; } = new List<InteractionExpectedOutputResult>();

    public virtual InteractionReport? InteractionReport { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual TeamSupportTicket? TeamSupportTicket { get; set; }

    public virtual ICollection<TicketAction> TicketActions { get; set; } = new List<TicketAction>();

    public virtual ICollection<TicketHistory> TicketHistories { get; set; } = new List<TicketHistory>();

    public virtual Tool Tool { get; set; } = null!;

    public virtual ICollection<UserInteractionEvent> UserInteractionEvents { get; set; } = new List<UserInteractionEvent>();

    public virtual ICollection<ZendeskTicket> ZendeskTickets { get; set; } = new List<ZendeskTicket>();
}
