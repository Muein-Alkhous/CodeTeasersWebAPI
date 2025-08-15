namespace Domain.Entities;

public partial class User : BaseEntity
{

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    /// <summary>
    /// Hashed password
    /// </summary>
    public string Password { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<Submission> Submissions { get; set; } = new List<Submission>();

    public virtual UserStatus? UserStatus { get; set; }
}
