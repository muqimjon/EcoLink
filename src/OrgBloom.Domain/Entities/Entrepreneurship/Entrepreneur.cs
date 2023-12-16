using OrgBloom.Domain.Entities.Users;

namespace OrgBloom.Domain.Entities.Entrepreneurship;

public class Entrepreneur : Auditable
{
    public string? Project { get; set; }
    public string? HelpType { get; set; }
    public string? RequiredFunding { get; set; }
    public string? AssetsInvested { get; set; }
    public bool IsSubmitted { get; set; }

    public long UserId { get; set; }
    public User User { get; set; } = default!;
}
