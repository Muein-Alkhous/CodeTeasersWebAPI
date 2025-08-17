namespace Domain.Entities;

public partial class Problem : BaseEntity
{
    
    public string Title { get; set; } = null!;

    public string Difficulty { get; set; } = null!;
    
    public virtual Description? Description { get; set; }

    public virtual ICollection<ProblemCategory> ProblemCategories { get; set; } = new List<ProblemCategory>();

    public virtual ICollection<Submission> Submissions { get; set; } = new List<Submission>();

    public virtual ICollection<Template> Templates { get; set; } = new List<Template>();

    public virtual ICollection<Test> Tests { get; set; } = new List<Test>();
}
