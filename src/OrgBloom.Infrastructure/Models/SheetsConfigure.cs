using Google.Apis.Sheets.v4;

namespace OrgBloom.Infrastructure.Models;

public class SheetsConfigure
{
    public string SpreadsheetId { get; set; } = string.Empty;
    public SheetsService Service { get; set; } = default!;
    public Sheets Sheets { get; set; } = default!;
}
