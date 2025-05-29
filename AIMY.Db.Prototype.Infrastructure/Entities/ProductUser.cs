namespace AIMY.Db.Prototype.Infrastructure.Entities;

public partial class ProductUser
{
    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public int UserId { get; set; }



    public virtual User User { get; set; } = null!;
    public int? ProductId { get; set; }
    public virtual Product? Product { get; set; } = null!;
    public virtual int? ClientId { get; set; } = null!;
    public virtual Client? Client { get; set; } = null!;
    public virtual int OrganizationId { get; set; }
    public virtual required Organization Organization { get; set; }
}
