namespace Domain.Entities;

public partial class Description : BaseEntity
{
    public byte[] Data { get; set; } = null!;
    public virtual Problem Problem { get; set; } = null!;
}
