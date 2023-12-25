namespace EcoLink.Application.Representatives.DTOs;

public class RepresentativeResultDto
{
    public long Id { get; set; }
    public string Area { get; set; } = string.Empty;
    public string Expectation { get; set; } = string.Empty;
    public string Purpose { get; set; } = string.Empty;
    public UserApplyResultDto User { get; set; } = default!;
    public bool IsSubmitted { get; set; }
}
