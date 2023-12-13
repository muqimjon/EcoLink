namespace OrgBloom.Domain.Entities;

public class Entrepreneur : Auditable
{
    public string? Experience { get; set; }
    public string? Project { get; set; }
    public string? HelpType { get; set; }
    public decimal? InvestmentAmount { get; set; }
    public string? AssetsInvested { get; set; }
    public bool IsSubmitted { get; set; }

    public long UserId { get; set; }
    public User User { get; set; } = default!;
}
