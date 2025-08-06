namespace Core.Entities;

public partial class Template
{
    public Guid Id { get; set; }

    public Guid ProblemId { get; set; }

    public string TemplatePath { get; set; } = null!;

    public string Language { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public bool IsDeleted { get; set; }

    public virtual Problem Problem { get; set; } = null!;
}
