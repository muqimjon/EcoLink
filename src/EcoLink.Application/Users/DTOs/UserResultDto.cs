namespace EcoLink.Application.Users.DTOs;

public class UserResultDto
{
    public long Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Patronomyc { get; set; } = string.Empty;
    public string Degree { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public UserProfession Profession { get; set; }
    public string Address { get; set; } = string.Empty;
    public string Languages { get; set; } = string.Empty;
    public string Experience { get; set; } = string.Empty;
    public int TelegramId { get; set; }
    public string Username { get; set; } = string.Empty;
    public string LanguageCode { get; set; } = string.Empty;
    public long? ChatId { get; set; }
    public bool IsBot { get; set; }
}
