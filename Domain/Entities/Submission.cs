namespace Domain.Entities;

public partial class Submission
{
    public Guid Id { get; set; }

    public Guid ProblemId { get; set; }

    public Guid UserId { get; set; }

    public string Language { get; set; } = null!;

    public int Points { get; set; }

    public int PassedTests { get; set; }

    public DateTime CreatedAt { get; set; }

    public bool IsDeleted { get; set; }

    public virtual Problem Problem { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
