namespace Domain.Entities;

public partial class Description : BaseEntity
{

    public string DescriptionPath { get; set; } = null!;

    public string? ShortDescription { get; set; }

    public virtual Problem IdNavigation { get; set; } = null!;
}
