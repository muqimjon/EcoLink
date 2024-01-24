namespace EcoLink.ApiService.Models.Investment;

public class InvestmentDto
{
    public long Id { get; set; }
    public string Sector { get; set; } = string.Empty;
    public string InvestmentAmount { get; set; } = string.Empty;
    public bool IsSubmitted { get; set; }
    public long UserId { get; set; }
}
