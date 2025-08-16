using System.Text.Json.Serialization;

namespace Domain.Entities;

public partial class UserStatus : BaseEntity
{

    public int Points { get; set; }
    
    public string Rank { get; set; } = null!;
    
    public virtual User IdNavigation { get; set; } = null!;
}
