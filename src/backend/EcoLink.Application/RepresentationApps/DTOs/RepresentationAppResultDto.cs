﻿namespace EcoLink.Application.RepresentationApps.DTOs;

public class RepresentationAppResultDto
{
    public long Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Age { get; set; } = string.Empty;
    public string Degree { get; set; } = string.Empty;
    public string Languages { get; set; } = string.Empty;
    public string Experience { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Area { get; set; } = string.Empty;
    public string Expectation { get; set; } = string.Empty;
    public string Purpose { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool IsOld { get; set; }
    public long UserId { get; set; }
    public UserResultDto User { get; set; } = default!;
}
