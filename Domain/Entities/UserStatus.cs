namespace Domain.Entities;

public partial class UserStatus : BaseEntity
{
    public Guid Id { get; set; }

    public int Points { get; set; }

    public virtual User IdNavigation { get; set; } = null!;
}
