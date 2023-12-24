using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using OrgBloom.Infrastructure.Models;
using OrgBloom.Application.Commons.Interfaces;
using OrgBloom.Application.InvestmentApps.DTOs;
using OrgBloom.Application.RepresentationApps.DTOs;
using OrgBloom.Application.EntrepreneurshipApps.DTOs;
using OrgBloom.Application.ProjectManagementApps.DTOs;

namespace OrgBloom.Infrastructure.Repositories;

public partial class SheetsRepository<T>(SheetsConfigure config) : ISheetsRepository<T>
{
    public async Task InsertAsync(T entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        var sheet = entity switch
        {
            InvestmentAppResultDto => "Investment!A:I",
            EntrepreneurshipAppResultDto => "Entrepreneurship!A:L",
            RepresentationAppResultDto => "Representation!A:",
            ProjectManagementAppResultDto => "ProjectManagement!A:",
            _ => throw new InvalidOperationException($"Unsupported entity type: {entity.GetType()}")
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
