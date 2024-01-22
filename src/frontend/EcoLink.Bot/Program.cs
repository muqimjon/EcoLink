using EcoLink.Bot;
using EcoLink.Application;
using EcoLink.Bot.Extensions;
using EcoLink.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add layers
builder.Services.AddMediatR(cf => cf.RegisterServicesFromAssemblies(assemblies: typeof(Program).Assembly));
builder.Services.AddInfrastructureServices(configuration: builder.Configuration);
builder.Services.AddApplicationServices();

// Add services
builder.Services.AddThis(configuration: builder.Configuration);

// Build
var app = builder.Build();

// Automigrate
app.MigrateDatabase();

// Localization
var supportedCultures = new[] { "uz", "ru", "en" };
var localizationOptions = new RequestLocalizationOptions()
  .SetDefaultCulture(defaultCulture: supportedCultures[0])
  .AddSupportedCultures(cultures: supportedCultures)
  .AddSupportedUICultures(uiCultures: supportedCultures);
app.UseRequestLocalization(options: localizationOptions);

app.Run();