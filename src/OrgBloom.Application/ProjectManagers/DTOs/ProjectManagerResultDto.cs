namespace OrgBloom.Application.ProjectManagers.DTOs;

public class ProjectManagerResultDto
{
    public long Id { get; set; }
    public string ProjectDirection { get; set; } = string.Empty;
    public string Expectation { get; set; } = string.Empty;
    public string Purpose { get; set; } = string.Empty;
    public bool IsSubmitted { get; set; }
    public UserApplyResultDto User { get; set; } = default!;
}
