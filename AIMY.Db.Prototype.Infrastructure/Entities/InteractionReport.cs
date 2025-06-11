using System;
using System.Collections.Generic;

namespace AIMY.Db.Prototype.Infrastructure.Entities;

public partial class InteractionReport
{
    public int Id { get; set; }

    public string IssueReported { get; set; } = null!;

    public string ActionTaken { get; set; } = null!;

    public virtual UserInteraction IdNavigation { get; set; } = null!;
}
