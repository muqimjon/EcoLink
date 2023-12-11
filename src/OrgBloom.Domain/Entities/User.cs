using OrgBloom.Domain.Enums;

namespace OrgBloom.Domain.Entities;

public class User : Auditable
{
    public string? FirstName { get; set; } = string.Empty;
    public string? LastName { get; set; } = string.Empty;
    public string? Patronomyc { get; set; } = string.Empty;
    public DateTime? DateOfBirth { get; set; }
    public string? Degree { get; set; } = string.Empty;
    public string? Phone { get; set; } = string.Empty;
    public string? Email { get; set; } = string.Empty;
    public UserProfession Profession { get; set; } = UserProfession.None;
    public int? TelegramId { get; set; }
    public string? LanguageCode { get; set; } = string.Empty;
}
