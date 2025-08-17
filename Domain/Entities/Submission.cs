namespace Domain.Entities;

public partial class Submission : BaseEntity
{

    public Guid ProblemId { get; set; }

    public Guid UserId { get; set; }

    public string Language { get; set; } = null!;

    public int Points { get; set; }

    public int PassedTests { get; set; }

    public virtual Problem Problem { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
