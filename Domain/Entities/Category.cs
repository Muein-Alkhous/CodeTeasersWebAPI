namespace Domain.Entities;

public partial class Category : BaseEntity
{
    public string Title { get; set; } = null!;

    public virtual ICollection<ProblemCategory> ProblemCategories { get; init; } = new List<ProblemCategory>();
}
