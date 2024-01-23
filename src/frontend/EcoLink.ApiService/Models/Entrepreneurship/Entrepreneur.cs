namespace EcoLink.ApiService.Models.Entrepreneurs;

public class Entrepreneur
{
    public string Project { get; set; } = string.Empty;
    public string HelpType { get; set; } = string.Empty;
    public string RequiredFunding { get; set; } = string.Empty;
    public string AssetsInvested { get; set; } = string.Empty;
    public bool IsSubmitted { get; set; }
}
