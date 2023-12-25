using Google.Apis.Sheets.v4;
using Google.Apis.Auth.OAuth2;

namespace EcoLink.Infrastructure.Contexts;

public class GoogleContext
{
    public static GoogleCredential GetCredential()
        => GoogleCredential.FromStream(new FileStream(
            Path.GetFullPath("client_secret.json"), 
            FileMode.Open, FileAccess.Read)).
            CreateScoped(SheetsService.Scope.Spreadsheets);
}