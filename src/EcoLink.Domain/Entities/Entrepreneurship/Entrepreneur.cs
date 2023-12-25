namespace EcoLink.Domain.Entities.Entrepreneurship;

public class Entrepreneur : Auditable
{
    public string? Sector { get; set; }
    public string? Project { get; set; }
    public string? HelpType { get; set; }
    public string? RequiredFunding { get; set; }
    public string? AssetsInvested { get; set; }
    public bool IsSubmitted { get; set; }

    public long UserId { get; set; }
    public User User { get; set; } = default!;
}
