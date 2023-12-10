global using MediatR;
using OrgBloom.Application.Mappers;
using OrgBloom.Application.DTOs.Investors;
using OrgBloom.Application.DTOs.Entrepreneurs;
using Microsoft.Extensions.DependencyInjection;
using OrgBloom.Application.DTOs.ProjectManagers;
using OrgBloom.Application.Queries.GetInvestors;
using OrgBloom.Application.DTOs.Representatives;
using OrgBloom.Application.Queries.GetEntrepreneurs;
using OrgBloom.Application.Queries.GetRepresentatives;
using OrgBloom.Application.Queries.GetProjectManagers;
using OrgBloom.Application.Commands.Investors.CreateInvestors;
using OrgBloom.Application.Commands.Investors.UpdateInvestors;
using OrgBloom.Application.Commands.Investors.DeleteInvestors;
using OrgBloom.Application.Commands.Entrepreneurs.CreateEntrepreneurs;
using OrgBloom.Application.Commands.Entrepreneurs.UpdateEntrepreneurs;
using OrgBloom.Application.Commands.Entrepreneurs.DeleteEntrepreneurs;
using OrgBloom.Application.Commands.Representatives.CreateRepresentatives;
using OrgBloom.Application.Commands.Representatives.UpdateRepresentatives;
using OrgBloom.Application.Commands.Representatives.DeleteRepresentatives;
using OrgBloom.Application.Commands.ProjectManagers.CreateProjectManagers;
using OrgBloom.Application.Commands.ProjectManagers.UpdateProjectManagers;
using OrgBloom.Application.Commands.ProjectManagers.DeleteProjectManagers;

namespace OrgBloom.Application;

public static class DependencyInjection
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MappingProfile));
        services.AddScoped<IRequestHandler<CreateInvestorCommand, int>, CreateInvestorCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateInvestorCommand, int>, UpdateInvestorCommandHandler>();
        services.AddScoped<IRequestHandler<DeleteInvestorCommand, bool>, DeleteInvestorCommandHandler>();
        services.AddScoped<IRequestHandler<GetInvestorQuery, InvestorResultDto>, GetInvestorQueryHendler>();
        services.AddScoped<IRequestHandler<GetAllInvestorsQuery, IEnumerable<InvestorResultDto>>, GetAllInvestorsQueryHandler>();

        services.AddScoped<IRequestHandler<CreateRepresentativeCommand, int>, CreateRepresentativeCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateRepresentativeCommand, int>, UpdateRepresentativeCommandHandler>();
        services.AddScoped<IRequestHandler<DeleteRepresentativeCommand, bool>, DeleteRepresentativeCommandHandler>();
        services.AddScoped<IRequestHandler<GetRepresentativeQuery, RepresentativeResultDto>, GetRepresentativeQueryHendler>();
        services.AddScoped<IRequestHandler<GetAllRepresentativesQuery, IEnumerable<RepresentativeResultDto>>, GetAllRepresentativesQueryHandler>();

        services.AddScoped<IRequestHandler<CreateProjectManagerCommand, int>, CreateProjectManagerCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateProjectManagerCommand, int>, UpdateProjectManagerCommandHandler>();
        services.AddScoped<IRequestHandler<DeleteProjectManagerCommand, bool>, DeleteProjectManagerCommandHandler>();
        services.AddScoped<IRequestHandler<GetProjectManagerQuery, ProjectManagerResultDto>, GetProjectManagerQueryHendler>();
        services.AddScoped<IRequestHandler<GetAllProjectManagersQuery, IEnumerable<ProjectManagerResultDto>>, GetAllProjectManagersQueryHandler>();

        services.AddScoped<IRequestHandler<CreateEntrepreneurCommand, int>, CreateEntrepreneurCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateEntrepreneurCommand, int>, UpdateEntrepreneurCommandHandler>();
        services.AddScoped<IRequestHandler<DeleteEntrepreneurCommand, bool>, DeleteEntrepreneurCommandHandler>();
        services.AddScoped<IRequestHandler<GetEntrepreneurQuery, EntrepreneurResultDto>, GetEntrepreneurQueryHendler>();
        services.AddScoped<IRequestHandler<GetAllEntrepreneursQuery, IEnumerable<EntrepreneurResultDto>>, GetAllEntrepreneursQueryHandler>();
    }
}
