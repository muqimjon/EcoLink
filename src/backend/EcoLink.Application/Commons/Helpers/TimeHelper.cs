namespace EcoLink.Application.Commons.Helpers;

public class TimeHelper
{
    public static DateTimeOffset GetDateTime()
        => DateTimeOffset.UtcNow.AddHours(TimeConstants.UTC);
}
