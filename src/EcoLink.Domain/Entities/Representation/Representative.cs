namespace EcoLink.Domain.Entities.Representation;

public class Representative : Auditable
{
    public string? Area { get; set; }
    public string? Expectation { get; set; }
    public string? Purpose { get; set; }
    public bool IsSubmitted { get; set; }

    public long UserId { get; set; }
    public User User { get; set; } = default!;
}
