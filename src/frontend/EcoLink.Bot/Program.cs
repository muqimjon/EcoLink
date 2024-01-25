using EcoLink.Bot;
using EcoLink.ApiService;

var builder = WebApplication.CreateBuilder(args);

// Add layers
builder.Services.AddApiServices(builder.Configuration);

// Add services
builder.Services.AddThis(configuration: builder.Configuration);

// Build
var app = builder.Build();

// Localization
var supportedCultures = new[] { "uz", "ru", "en" };
var localizationOptions = new RequestLocalizationOptions()
  .SetDefaultCulture(defaultCulture: supportedCultures[0])
  .AddSupportedCultures(cultures: supportedCultures)
  .AddSupportedUICultures(uiCultures: supportedCultures);
app.UseRequestLocalization(options: localizationOptions);

app.Run();