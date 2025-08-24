namespace Domain.Entities;

public partial class Description : BaseEntity
{
    public string Content { get; set; } = null!;
    public virtual Problem Problem { get; set; } = null!;
}
