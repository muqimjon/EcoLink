using EcoLink.Bot;
using EcoLink.ApiService;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddThis(configuration: builder.Configuration);

// Add API services
builder.Services.AddApiServices(builder.Configuration);

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