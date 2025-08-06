namespace Core.Entities;

public partial class UserStatus
{
    public Guid Id { get; set; }

    public int Points { get; set; }

    public virtual User IdNavigation { get; set; } = null!;
}
