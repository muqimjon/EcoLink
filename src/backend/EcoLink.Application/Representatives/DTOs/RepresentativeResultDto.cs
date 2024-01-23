namespace EcoLink.Application.Representatives.DTOs;

public class RepresentativeResultDto
{
    public string Area { get; set; } = string.Empty;
    public string Expectation { get; set; } = string.Empty;
    public string Purpose { get; set; } = string.Empty;
    public bool IsSubmitted { get; set; }
}
