namespace EcoLink.ApiService.Models.ProjectManagement;

public class ProjectManagementDto
{
    public long Id { get; set; }
    public string Area { get; set; } = string.Empty;
    public string ProjectDirection { get; set; } = string.Empty;
    public string Expectation { get; set; } = string.Empty;
    public string Purpose { get; set; } = string.Empty;
    public bool IsSubmitted { get; set; }
    public long UserId { get; set; }
}
