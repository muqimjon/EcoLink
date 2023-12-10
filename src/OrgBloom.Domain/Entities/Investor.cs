namespace OrgBloom.Domain.Entities;

public class Investor : Human   
{
    public string Sector { get; set; } = string.Empty;
    public decimal InvestmentAmount { get; set; }
}
