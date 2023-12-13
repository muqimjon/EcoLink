namespace OrgBloom.Domain.Commons;

public abstract class Auditable
{
    public long Id { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTimeOffset? UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
}
