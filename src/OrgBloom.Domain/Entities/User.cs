using OrgBloom.Domain.Enums;

namespace OrgBloom.Domain.Entities;

public class User : Auditable
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; } 
    public string? Patronomyc { get; set; } 
    public DateTime? DateOfBirth { get; set; }
    public string? Degree { get; set; } 
    public string? Phone { get; set; } 
    public string? Email { get; set; } 
    public UserProfession Profession { get; set; }
    public int TelegramId { get; set; }
    public string? LanguageCode { get; set; } 
}
