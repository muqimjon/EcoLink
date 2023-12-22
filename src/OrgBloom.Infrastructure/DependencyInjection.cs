using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Auth.OAuth2;
using Microsoft.EntityFrameworkCore;
using OrgBloom.Infrastructure.Contexts;
using Microsoft.Extensions.Configuration;
using OrgBloom.Infrastructure.Repositories;
using OrgBloom.Application.Commons.Interfaces;
using Microsoft.Extensions.DependencyInjection;


namespace OrgBloom.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Scoped);

        var path = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "client_secrets.json"));

        GoogleCredential credential = GoogleCredential.FromStream(
            new FileStream("../../../client_secret.json", FileMode.Open, FileAccess.Read)).
            CreateScoped(SheetsService.Scope.Spreadsheets);

        services.AddSingleton(new SheetsService(new BaseClientService.Initializer()
        {
            HttpClientInitializer = credential,
            ApplicationName = configuration.GetConnectionString("ApplicationName"),
        }));
            

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        return services;
    }
}

