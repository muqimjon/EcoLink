using MediatR;
using OrgBloom.Application.Mappers;
using Microsoft.Extensions.DependencyInjection;
using OrgBloom.Application.Commands.Investors.CreateInvestors;
using OrgBloom.Application.Commands.Investors.UpdateInvestors;
using OrgBloom.Application.Commands.Investors.DeleteInvestors;
using OrgBloom.Application.Queries.GetInvestors;
using OrgBloom.Domain.Entities;

namespace OrgBloom.Application;

public static class DependencyInjection
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MappingProfile));
        services.AddScoped<IRequestHandler<CreateInvestorCommand, int>, CreateInvestorCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateInvestorCommand, int>, UpdateInvestorCommandHandler>();
        services.AddScoped<IRequestHandler<DeleteInvestorCommand, bool>, DeleteInvestorCommandHandler>();
        services.AddScoped<IRequestHandler<GetInvestorQuery, Investor>, GetInvestorQueryHendler>();
        services.AddScoped<IRequestHandler<GetAllInvestorsQuery, IEnumerable<Investor>>, GetAllInvestorsQueryHandler>();
    }
}
