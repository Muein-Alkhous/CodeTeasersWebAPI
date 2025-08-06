namespace Core.Entities;

public partial class Description
{
    public Guid Id { get; set; }

    public string DescriptionPath { get; set; } = null!;

    public string? ShortDescription { get; set; }

    public DateTime CreatedAt { get; set; }

    public bool IsDeleted { get; set; }

    public virtual Problem IdNavigation { get; set; } = null!;
}
