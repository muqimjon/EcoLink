namespace OrgBloom.Application.Entrepreneurs.DTOs;

public class EntrepreneurResultDto
{
    public long Id { get; set; }
    public string Project { get; set; } = string.Empty;
    public string HelpType { get; set; } = string.Empty;
    public string RequiredFunding { get; set; } = string.Empty;
    public string AssetsInvested { get; set; } = string.Empty;
    public bool IsSubmitted { get; set; }
    public UserApplyResultDto User { get; set; } = default!;
}
