namespace EcoLink.ApiService.Models.Investors;

public class Investor
{
    public string Sector { get; set; } = string.Empty;
    public string InvestmentAmount { get; set; } = string.Empty;
    public bool IsSubmitted { get; set; }
}
