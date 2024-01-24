using EcoLink.ApiService.Models.Investment;
using EcoLink.ApiService.Models.Representation;
using EcoLink.ApiService.Models.Entrepreneurship;
using EcoLink.ApiService.Models.ProjectManagement;

namespace EcoLink.ApiService.Models;

public class UserDto
{
    public long Id { get; set; }
    public long TelegramId { get; set; }
    public string Username { get; set; } = string.Empty;
    public string LanguageCode { get; set; } = string.Empty;
    public long ChatId { get; set; }
    public bool IsBot { get; set; }
    public State State { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Patronomyc { get; set; } = string.Empty;
    public DateTimeOffset DateOfBirth { get; set; }
    public string Age { get; set; } = string.Empty;
    public string Degree { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public UserProfession Profession { get; set; }
    public string Address { get; set; } = string.Empty;
    public string Languages { get; set; } = string.Empty;
    public string Experience { get; set; } = string.Empty;
    public InvestmentDto Investment { get; set; } = default!;
    public EntrepreneurshipDto Entrepreneurship { get; set; } = default!;
    public RepresentationDto Representation { get; set; } = default!;
    public ProjectManagementDto ProjectManagement { get; set; } = default!;
    public DateTimeOffset CreatedAt { get; set; }
}
