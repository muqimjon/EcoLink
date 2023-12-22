namespace OrgBloom.Application.Commons.Helpers;

public class TimeHelper
{
    public static DateTimeOffset GetDateTime()
        => DateTimeOffset.UtcNow.AddHours(TimeConstants.UTC);
}
