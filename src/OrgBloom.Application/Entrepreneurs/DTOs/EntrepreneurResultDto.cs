using OrgBloom.Domain.Entities;

namespace OrgBloom.Application.Entrepreneurs.DTOs;

public class EntrepreneurResultDto
{
    public long Id { get; set; }
    public string Experience { get; set; } = string.Empty;
    public string Project { get; set; } = string.Empty;
    public string HelpType { get; set; } = string.Empty;
    public decimal? InvestmentAmount { get; set; }
    public string AssetsInvested { get; set; } = string.Empty;

    public long UserId { get; set; }
    public User User { get; set; } = default!;
}
