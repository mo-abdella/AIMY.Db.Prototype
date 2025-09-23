namespace AIMY.Db.Prototype.Infrastructure.Entities;

public partial class ProductUser
{
    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public int UserId { get; set; }

    public int? ProductId { get; set; }

    public int? ClientId { get; set; }

    public int OrganizationId { get; set; }

    public int Id { get; set; }

    public virtual Client? Client { get; set; }

    public virtual Organization Organization { get; set; } = null!;

    public virtual Product? Product { get; set; }

    public virtual User User { get; set; } = null!;
}
