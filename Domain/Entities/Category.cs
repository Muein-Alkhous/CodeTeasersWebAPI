namespace Domain.Entities;

public partial class Category : BaseEntity
{
    

    public string Title { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<ProblemCategory> ProblemCategories { get; set; } = new List<ProblemCategory>();
}
