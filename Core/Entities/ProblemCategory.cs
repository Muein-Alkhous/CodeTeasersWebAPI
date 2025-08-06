namespace Core.Entities;

public partial class ProblemCategory
{
    public Guid Id { get; set; }

    public Guid CategoryId { get; set; }

    public Guid ProblemId { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual Problem Problem { get; set; } = null!;
}
