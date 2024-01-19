using EcoLink.Bot;
using EcoLink.Application;
using EcoLink.Bot.Extensions;
using EcoLink.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddMediatR(cf => cf.RegisterServicesFromAssemblies(assemblies: typeof(Program).Assembly));
builder.Services.AddInfrastructureServices(configuration: builder.Configuration);
builder.Services.AddApplicationServices();

// Get google auth
builder.Services.AddThis(configuration: builder.Configuration);

// Build
var app = builder.Build();

// Automigrate
app.MigrateDatabase();
    
var supportedCultures = new[] { "uz", "ru", "en" };
var localizationOptions = new RequestLocalizationOptions()
  .SetDefaultCulture(supportedCultures[0])
  .AddSupportedCultures(supportedCultures)
  .AddSupportedUICultures(supportedCultures);
app.UseRequestLocalization(localizationOptions);

app.Run();