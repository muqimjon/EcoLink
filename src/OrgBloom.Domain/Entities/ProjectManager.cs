namespace OrgBloom.Domain.Entities;

public class ProjectManager : Auditable
{
    public string? Languages { get; set; }
    public string? Experience { get; set; }
    public string? Address { get; set; }
    public string? Area { get; set; }
    public string? Expectation { get; set; }
    public string? Purpose { get; set; }
    public bool IsSubmitted { get; set; }

    public long UserId { get; set; }
    public User User { get; set; } = default!;
}
