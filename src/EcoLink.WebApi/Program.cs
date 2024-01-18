using EcoLink.WebApi;
using EcoLink.Application;
using EcoLink.Bot.Extensions;
using EcoLink.Infrastructure;
using EcoLink.Infrastructure.Models;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers();

//builder.Environment.ApplicationName = builder.Configuration.GetValue("ApplicationName", string.Empty)!;

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services
builder.Services.AddMediatR(cf => cf.RegisterServicesFromAssemblies(typeof(Program).Assembly));
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();

// Get google auth
var googleAuthSettings = new GoogleAuthSettings();
builder.Configuration.GetSection("GoogleAuth").Bind(instance: googleAuthSettings);
builder.Services.AddThis(googleAuthSettings: googleAuthSettings, configuration: builder.Configuration);

// Build
var app = builder.Build();

// Automigrate
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
