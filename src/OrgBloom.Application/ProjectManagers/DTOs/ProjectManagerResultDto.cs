using OrgBloom.Application.Users.DTOs;
using OrgBloom.Domain.Entities;

namespace OrgBloom.Application.ProjectManagers.DTOs;

public class ProjectManagerResultDto
{
    public long Id { get; set; }
    public string Languages { get; set; } = string.Empty;
    public string Experience { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Area { get; set; } = string.Empty;
    public string Expectation { get; set; } = string.Empty;
    public string Purpose { get; set; } = string.Empty;
    public bool IsSubmitted { get; set; }
    public UserApplyResultDto User { get; set; } = default!;
}
