namespace OrgBloom.Application.Commons.Helpers;

public class TimeHelper
{
    public static DateTimeOffset GetDateTime()
        => DateTimeOffset.UtcNow.AddHours(TimeConstants.UTC);

    public static int GetAge(DateTimeOffset dateOfBirth)
        => DateTimeOffset.UtcNow.Year - dateOfBirth.Year - (DateTimeOffset.UtcNow < dateOfBirth.AddYears(
            DateTimeOffset.UtcNow.Year - dateOfBirth.Year) ? 1 : 0);
}
