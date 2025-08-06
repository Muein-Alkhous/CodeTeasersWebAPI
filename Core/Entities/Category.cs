namespace Core.Entities;

public partial class Category
{
    public Guid Id { get; set; }

    public string Title { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<ProblemCategory> ProblemCategories { get; set; } = new List<ProblemCategory>();
}
