using EcoLink.Domain.Entities.Investment;
using EcoLink.Domain.Entities.Representation;
using EcoLink.Domain.Entities.Entrepreneurship;
using EcoLink.Domain.Entities.ProjectManagement;

namespace EcoLink.Domain.Entities.Users;

public class User : Auditable
{
    public long? TelegramId { get; set; }
    public string? Username { get; set; }
    public string? LanguageCode { get; set; }
    public long? ChatId { get; set; }
    public bool IsBot { get; set; }
    public State State { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Patronomyc { get; set; }
    public DateTimeOffset? DateOfBirth { get; set; }
    public string? Age { get; set; }
    public string? Degree { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public UserProfession Profession { get; set; }
    public string? Address { get; set; }
    public string? Languages { get; set; }
    public string? Experience { get; set; }
    public Investor? Investment { get; set; }
    public Entrepreneur? Entrepreneurship { get; set; }
    public Representative? Representation { get; set; }
    public ProjectManager? ProjectManagement { get; set; }
}
