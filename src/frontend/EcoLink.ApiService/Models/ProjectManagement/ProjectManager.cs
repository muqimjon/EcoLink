namespace EcoLink.ApiService.Models.ProjectManagers;

public class ProjectManager
{
    public string ProjectDirection { get; set; } = string.Empty;
    public string Expectation { get; set; } = string.Empty;
    public string Purpose { get; set; } = string.Empty;
    public bool IsSubmitted { get; set; }
}
