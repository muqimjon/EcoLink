﻿namespace OrgBloom.Domain.Entities;

public class ProjectManager : Auditable
{
    public string? Languages { get; set; } = string.Empty;
    public string? Experience { get; set; } = string.Empty;
    public string? Address { get; set; } = string.Empty;
    public string? Area { get; set; } = string.Empty;
    public string? Expectation { get; set; } = string.Empty;
    public string? Purpose { get; set; } = string.Empty;

    public long? UserId { get; set; }
    public User User { get; set; } = default!;
}
