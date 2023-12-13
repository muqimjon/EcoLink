using OrgBloom.Domain.Enums;

namespace OrgBloom.Domain.Entities;

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
    public DateTime? DateOfBirth { get; set; }
    public string? Degree { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public UserProfession Profession { get; set; }
}
