using System;
using System.Collections.Generic;

namespace AIMY.Db.Prototype.Infrastructure.Entities;

public partial class TicketAction
{
    public int Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? TakenAt { get; set; }

    public string? TakenBy { get; set; }

    public string? Type { get; set; }

    public string? Description { get; set; }

    public int UserInteractionId { get; set; }

    public string? HtmlBody { get; set; }

    public bool? IsPublic { get; set; }

    public string? Attachments { get; set; }

    public virtual UserInteraction UserInteraction { get; set; } = null!;
}
