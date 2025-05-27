namespace AIMY.Db.Prototype.Infrastructure.Entities;

public partial class Product
{
    public int Id { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public string Name { get; set; } = null!;

    public string Key { get; set; } = null!;

    public int ClientId { get; set; }

    public virtual ICollection<AnalysisRule> AnalysisRules { get; set; } = new List<AnalysisRule>();

    public virtual Client Client { get; set; } = null!;

    public virtual ICollection<Job> Jobs { get; set; } = new List<Job>();

    public virtual ICollection<ProductUser> ProductUsers { get; set; } = new List<ProductUser>();

    public virtual ICollection<Tool> Tools { get; set; } = new List<Tool>();
}
