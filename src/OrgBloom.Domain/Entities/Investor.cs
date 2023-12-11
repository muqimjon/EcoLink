namespace OrgBloom.Domain.Entities;

public class Investor : Auditable
{
    public string? Sector { get; set; } = string.Empty;
    public decimal? InvestmentAmount { get; set; }

    public long? UserId { get; set; }
    public User User { get; set; } = default!;
}
