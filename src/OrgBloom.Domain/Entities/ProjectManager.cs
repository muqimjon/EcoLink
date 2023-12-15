namespace OrgBloom.Domain.Entities;

public class ProjectManager : Auditable
{
    public string? ProjectDirection { get; set; }
    public string? Expectation { get; set; }
    public string? Purpose { get; set; }
    public bool IsSubmitted { get; set; }

    public long UserId { get; set; }
    public User User { get; set; } = default!;
}
