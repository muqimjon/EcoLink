namespace EcoLink.ApiService.Models.Representation;

public class RepresentationDto
{
    public long Id { get; set; }
    public string Area { get; set; } = string.Empty;
    public string Expectation { get; set; } = string.Empty;
    public string Purpose { get; set; } = string.Empty;
    public bool IsSubmitted { get; set; }
    public long UserId { get; set; }
}
