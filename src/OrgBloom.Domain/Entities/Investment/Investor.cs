using OrgBloom.Domain.Entities.Users;

namespace OrgBloom.Domain.Entities.Investment;

public class Investor : Auditable
{
    public string? Sector { get; set; }
    public string? InvestmentAmount { get; set; }
    public bool IsSubmitted { get; set; }

    public long UserId { get; set; }
    public User User { get; set; } = default!;
}
