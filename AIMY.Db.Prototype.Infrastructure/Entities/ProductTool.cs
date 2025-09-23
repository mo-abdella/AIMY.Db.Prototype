namespace AIMY.Db.Prototype.Infrastructure.Entities;

public partial class ProductTool
{
    public int? ProductId { get; set; }

    public int ToolId { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public int Id { get; set; }

    public int? ClientId { get; set; }

    public int? OrganizationId { get; set; }

    public virtual Client? Client { get; set; }

    public virtual Organization? Organization { get; set; }

    public virtual Product? Product { get; set; }

    public virtual Tool Tool { get; set; } = null!;
}
