using EcoLink.ApiService.Services.Users;
using Microsoft.Extensions.Configuration;
using EcoLink.ApiService.Interfaces.Users;
using EcoLink.ApiService.Services.Investment;
using EcoLink.ApiService.Interfaces.Investment;
using Microsoft.Extensions.DependencyInjection;
using EcoLink.ApiService.Services.Representation;
using EcoLink.ApiService.Interfaces.Representation;
using EcoLink.ApiService.Services.Entrepreneurship;
using EcoLink.ApiService.Services.ProjectManagement;
using EcoLink.ApiService.Interfaces.Entrepreneurship;
using EcoLink.ApiService.Interfaces.ProjectManagement;

namespace EcoLink.ApiService;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfigurationRoot configuration)
    {
        var baseLink = configuration.GetConnectionString("BaseLink");

        services.AddHttpClient<IUserService, UserService>(client =>
            { client.BaseAddress = new Uri($"{baseLink}api/users/"); });

        services.AddHttpClient<IInvestmentAppService, InvestmentAppService>(client =>
            { client.BaseAddress = new Uri($"{baseLink}api/InvestmentApps/"); });

        services.AddHttpClient<IRepresentationAppService, RepresentationAppService>(client =>
            { client.BaseAddress = new Uri($"{baseLink}api/RepresentationApps/"); });

        services.AddHttpClient<IEntrepreneurshipAppService, EntrepreneurshipAppService>(client =>
            { client.BaseAddress = new Uri($"{baseLink}api/EntrepreneurshipApps/"); });

        services.AddHttpClient<IProjectManagementAppService, ProjectManagementAppService>(client =>
            { client.BaseAddress = new Uri($"{baseLink}api/ProjectManagementApps/"); });

        return services;
    }
}
