using OrgBloom.Domain.Entities;

namespace OrgBloom.Application.Investors.DTOs;

public class InvestorResultDto
{
    public long Id { get; set; }
    public string Sector { get; set; } = string.Empty;
    public string InvestmentAmount { get; set; } = string.Empty;

    public long UserId { get; set; }
    public User User { get; set; } = default!;
}
