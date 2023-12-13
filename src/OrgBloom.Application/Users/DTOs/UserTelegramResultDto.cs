using OrgBloom.Domain.Enums;

namespace OrgBloom.Application.Users.DTOs;

public class UserTelegramResultDto
{
    public long Id { get; set; }
    public int UserId { get; set; }
    public long ChatId { get; set; }
    public bool IsBot { get; set; }
    public long TelegramId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string LanguageCode { get; set; } = string.Empty;
    public State State { get; set; }
}
