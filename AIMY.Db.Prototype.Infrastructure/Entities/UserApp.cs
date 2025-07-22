using System;
using System.Collections.Generic;

namespace AIMY.Db.Prototype.Infrastructure.Entities;

public partial class UserApp
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int? AppId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public virtual App? App { get; set; }

    public virtual User? User { get; set; }
}
