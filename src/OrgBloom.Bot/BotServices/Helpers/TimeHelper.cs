namespace OrgBloom.Bot.BotServices.Helpers;

public static class TimeHelper
{
    public static int GetAge(DateTimeOffset dateOfBirth)
        => DateTimeOffset.UtcNow.Year - dateOfBirth.Year - (DateTimeOffset.UtcNow < dateOfBirth.AddYears(
            DateTimeOffset.UtcNow.Year - dateOfBirth.Year) ? 1 : 0);
}
