using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using EcoLink.Infrastructure.Models;
using EcoLink.Application.Commons.Interfaces;
using EcoLink.Application.InvestmentApps.DTOs;
using EcoLink.Application.RepresentationApps.DTOs;
using EcoLink.Application.EntrepreneurshipApps.DTOs;
using EcoLink.Application.ProjectManagementApps.DTOs;

namespace EcoLink.Infrastructure.Repositories;

public partial class SheetsRepository<T>(SheetsConfigure config) : ISheetsRepository<T>
{
    public async Task InsertAsync(T entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        var sheet = entity switch
        {
            InvestmentAppForSheetsDto => "Investment!A:I",
            EntrepreneurshipAppForSheetsDto => "Entrepreneurship!A:L",
            RepresentationAppForSheetsDto => "Representation!A:L",
            ProjectManagementAppForSheetsDto => "ProjectManagement!A:K",
            _ => throw new InvalidOperationException($"Unsupported entity type for send Google Sheets: {entity.GetType()}")
        };

        var properties = typeof(T).GetProperties();
        var oblist = new List<object>();

        foreach (var property in properties)
            oblist.Add(property.GetValue(entity)!);

        var valueRange = new ValueRange
        {
            Values = new List<IList<object>> { oblist }
        };

        var appendRequest = config.Service.Spreadsheets.Values.Append(valueRange, config.SpreadsheetId, sheet);
        appendRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;
        await appendRequest.ExecuteAsync();
    }
}
