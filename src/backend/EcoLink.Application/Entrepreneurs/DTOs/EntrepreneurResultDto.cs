namespace EcoLink.Application.Entrepreneurs.DTOs;

public class EntrepreneurResultDto
{
    public string Project { get; set; } = string.Empty;
    public string HelpType { get; set; } = string.Empty;
    public string RequiredFunding { get; set; } = string.Empty;
    public string AssetsInvested { get; set; } = string.Empty;
    public bool IsSubmitted { get; set; }
}
