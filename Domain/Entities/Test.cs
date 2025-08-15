namespace Domain.Entities;

public partial class Test : BaseEntity
{

    public Guid ProblemId { get; set; }

    public string Language { get; set; } = null!;

    public string TestPath { get; set; } = null!;

    public int NumCases { get; set; }

    public virtual Problem Problem { get; set; } = null!;
}
