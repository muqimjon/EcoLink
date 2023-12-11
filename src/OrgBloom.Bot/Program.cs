using Telegram.Bot;
using System.Globalization;
using Telegram.Bot.Polling;
using OrgBloom.Application;
using OrgBloom.Infrastructure;
using OrgBloom.Bot.BotServices;
using Microsoft.AspNetCore.Localization;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddMediatR(cf => cf.RegisterServicesFromAssemblies(typeof(Program).Assembly));
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();

// Get bot token
var token = builder.Configuration.GetValue("BotToken", string.Empty);

// Add bot services
builder.Services.AddSingleton(new TelegramBotClient(token!));
builder.Services.AddSingleton<IUpdateHandler, BotUpdateHandler>();
builder.Services.AddHostedService<BotBackgroundService>();

builder.Services.AddLocalization();

// Build
var app = builder.Build();

// Add localization
var supportedCultures = new[] { "uz-Uz", "en-Us", "ru-Ru" };
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(supportedCultures[0]),
    SupportedCultures = supportedCultures.Select(c => new CultureInfo(c)).ToList(),
    SupportedUICultures = supportedCultures.Select(c => new CultureInfo(c)).ToList()
});

app.Run();