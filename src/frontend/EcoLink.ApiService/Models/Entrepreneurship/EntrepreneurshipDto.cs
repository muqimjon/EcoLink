namespace EcoLink.ApiService.Models.Entrepreneurship;

public class EntrepreneurshipDto
{
    public long Id { get; set; }
    public string Sector { get; set; } = string.Empty;
    public string Project { get; set; } = string.Empty;
    public string HelpType { get; set; } = string.Empty;
    public string RequiredFunding { get; set; } = string.Empty;
    public string AssetsInvested { get; set; } = string.Empty;
    public bool IsSubmitted { get; set; }
    public long UserId { get; set; }
}
