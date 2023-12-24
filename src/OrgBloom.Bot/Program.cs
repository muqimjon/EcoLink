using Telegram.Bot.Polling;
using OrgBloom.Application;
using OrgBloom.Infrastructure;
using OrgBloom.Bot.Extensions;
using OrgBloom.Bot.BotServices;
using OrgBloom.Bot.BotServices.Commons;

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

app.MigrateDatabase();
    
var supportedCultures = new[] { "uz", "ru", "en" };
var localizationOptions = new RequestLocalizationOptions()
  .SetDefaultCulture(supportedCultures[0])
  .AddSupportedCultures(supportedCultures)
  .AddSupportedUICultures(supportedCultures);
app.UseRequestLocalization(localizationOptions);

app.Run();