using System;
using System.Collections.Generic;

namespace AIMY.Db.Prototype.Infrastructure.Entities;

public partial class User
{
    public int Id { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public string Email { get; set; } = null!;

    public int? SupervisorId { get; set; }

    public virtual ICollection<User> InverseSupervisor { get; set; } = new List<User>();

    public virtual ICollection<ProductUser> ProductUsers { get; set; } = new List<ProductUser>();

    public virtual User? Supervisor { get; set; }

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
