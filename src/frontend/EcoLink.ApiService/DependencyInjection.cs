using EcoLink.ApiService.Constants;
using EcoLink.ApiService.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EcoLink.ApiService;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient<IUserService, UserService>(client =>
        {
            client.BaseAddress = new Uri($"{configuration["BaseUrl"]}api/Users/");
        });

        return services;
    }
}
