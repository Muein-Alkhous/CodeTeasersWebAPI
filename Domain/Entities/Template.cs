namespace Domain.Entities;

public partial class Template : BaseEntity
{

    public Guid ProblemId { get; set; }

    public string TemplatePath { get; set; } = null!;

    public string Language { get; set; } = null!;

    public virtual Problem Problem { get; set; } = null!;
}
