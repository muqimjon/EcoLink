namespace EcoLink.Application.Investors.DTOs;

public class InvestorResultDto
{
    public string Sector { get; set; } = string.Empty;
    public string InvestmentAmount { get; set; } = string.Empty;
    public bool IsSubmitted { get; set; }
}
