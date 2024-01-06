using EcoLink.Application;
using EcoLink.Application.Commons.Interfaces;
using EcoLink.Bot.Extensions;
using EcoLink.Infrastructure;
using EcoLink.Infrastructure.Models;
using EcoLink.Infrastructure.Repositories;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers();

builder.Environment.ApplicationName = builder.Configuration.GetValue("ApplicationName", string.Empty)!;

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services
builder.Services.AddMediatR(cf => cf.RegisterServicesFromAssemblies(typeof(Program).Assembly));
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();

var googleAuthSettings = new GoogleAuthSettings();
builder.Configuration.Bind("GoogleAuth", googleAuthSettings);
var g = JsonConvert.SerializeObject(googleAuthSettings);

// Add sheets
var configur = new SheetsConfigure()
{
    SpreadsheetId = builder.Configuration.GetValue("SpreadsheetId", string.Empty)!,
    Service = new SheetsService(new BaseClientService.Initializer()
    {
        HttpClientInitializer = GoogleCredential.FromJson(g),
        ApplicationName = googleAuthSettings.project_id,
    }),
};
builder.Services.AddSingleton(configur);
builder.Services.AddScoped(typeof(ISheetsRepository<>), typeof(SheetsRepository<>));

var app = builder.Build();

app.MigrateDatabase();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
