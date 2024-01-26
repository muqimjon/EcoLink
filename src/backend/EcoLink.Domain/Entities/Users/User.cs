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
    public string? Sector { get; set; }
    public string? Project { get; set; }
    public string? HelpType { get; set; }
    public string? RequiredFunding { get; set; }
    public string? AssetsInvested { get; set; }
    public string? InvestmentAmount { get; set; }
    public string? Expectation { get; set; }
    public string? Purpose { get; set; }
    public string? Area { get; set; }
}
