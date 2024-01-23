using EcoLink.Application.Investors.DTOs;
using EcoLink.Application.Commons.Mappers;
using EcoLink.Application.Entrepreneurs.DTOs;
using Microsoft.Extensions.DependencyInjection;
using EcoLink.Application.InvestmentApps.DTOs;
using EcoLink.Application.ProjectManagers.DTOs;
using EcoLink.Application.Representatives.DTOs;
using EcoLink.Application.Users.Queries.GetUsers;
using EcoLink.Application.RepresentationApps.DTOs;
using EcoLink.Application.EntrepreneurshipApps.DTOs;
using EcoLink.Application.ProjectManagementApps.DTOs;
using EcoLink.Application.Users.Commands.CreateUsers;
using EcoLink.Application.Users.Commands.DeleteUsers;
using EcoLink.Application.Users.Commands.UpdateUsers;
using EcoLink.Application.Investors.Queries.GetInvestors;
using EcoLink.Application.Investors.Commands.CreateInvestors;
using EcoLink.Application.Investors.Commands.DeleteInvestors;
using EcoLink.Application.Investors.Commands.UpdateInvestors;
using EcoLink.Application.Entrepreneurs.Queries.GetEntrepreneurs;
using EcoLink.Application.InvestmentApps.Queries.GetInvestmentApp;
using EcoLink.Application.Entrepreneurs.Commands.CreateEntrepreneurs;
using EcoLink.Application.Entrepreneurs.Commands.DeleteEntrepreneurs;
using EcoLink.Application.Entrepreneurs.Commands.UpdateEntrepreneurs;
using EcoLink.Application.ProjectManagers.Queries.GetProjectManagers;
using EcoLink.Application.Representatives.Queries.GetRepresentatives;
using EcoLink.Application.InvestmentApps.Commands.CreateInvestmentApps;
using EcoLink.Application.ProjectManagers.Commands.CreateProjectManagers;
using EcoLink.Application.ProjectManagers.Commands.DeleteProjectManagers;
using EcoLink.Application.ProjectManagers.Commands.UpdateProjectManagers;
using EcoLink.Application.RepresentationApps.Queries.GetRepresentationApp;
using EcoLink.Application.Representatives.Commands.CreateRepresentatives;
using EcoLink.Application.Representatives.Commands.DeleteRepresentatives;
using EcoLink.Application.Representatives.Commands.UpdateRepresentatives;
using EcoLink.Application.EntrepreneurshipApps.Queries.GetEntrepreneurshipApp;
using EcoLink.Application.RepresentationApps.Commands.CreateRepresentationApps;
using EcoLink.Application.ProjectManagementApps.Queries.GetProjectManagementApp;
using EcoLink.Application.ProjectManagementApps.Commands.CreateProjectManagementApps;

namespace EcoLink.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MappingProfile));


        // User
        services.AddScoped<IRequestHandler<CreateUserCommand, int>, CreateUserCommandHandler>();
        services.AddScoped<IRequestHandler<CreateUserWithReturnTgResultCommand, UserTelegramResultDto>, CreateUserWithReturnTgResultCommandHandler>();

        services.AddScoped<IRequestHandler<UpdateUserCommand, int>, UpdateUserCommandHandler>();

        services.AddScoped<IRequestHandler<DeleteUserCommand, bool>, DeleteUserCommandHandler>();

        services.AddScoped<IRequestHandler<GetUserByIdQuery, UserResultDto>, GetUserByIdQueryHandler>();
        services.AddScoped<IRequestHandler<GetUserForApplicationQuery, UserApplyResultDto>, GetUserForApplicationQueryHandler>();
        services.AddScoped<IRequestHandler<GetUserByTelegramIdQuery, UserTelegramResultDto>, GetUserByTelegramIdQueryHandler>();


        // Investor
        services.AddScoped<IRequestHandler<CreateInvestorCommand, int>, CreateInvestorCommandHandler>();
        services.AddScoped<IRequestHandler<CreateInvestorWithReturnCommand, InvestorResultDto>, CreateInvestorWithReturnCommandHandler>();

        services.AddScoped<IRequestHandler<UpdateInvestorCommand, int>, UpdateInvestorCommandHandler>();

        services.AddScoped<IRequestHandler<DeleteInvestorCommand, bool>, DeleteInvestorCommandHandler>();

        services.AddScoped<IRequestHandler<GetInvestorQuery, InvestorResultDto>, GetInvestorQueryHandler>();
        services.AddScoped<IRequestHandler<GetInvestorByUserIdQuery, InvestorResultDto>, GetInvestorByUserIdQueryHandler>();
        services.AddScoped<IRequestHandler<GetAllInvestorsQuery, IEnumerable<InvestorResultDto>>, GetAllInvestorsQueryHandler>();


        // Representative
        services.AddScoped<IRequestHandler<CreateRepresentativeCommand, int>, CreateRepresentativeCommandHandler>();
        services.AddScoped<IRequestHandler<CreateRepresentativeWithReturnCommand, RepresentativeResultDto>, CreateRepresentativeWithReturnCommandHandler>();

        services.AddScoped<IRequestHandler<UpdateRepresentativeCommand, int>, UpdateRepresentativeCommandHandler>();

        services.AddScoped<IRequestHandler<DeleteRepresentativeCommand, bool>, DeleteRepresentativeCommandHandler>();

        services.AddScoped<IRequestHandler<GetRepresentativeQuery, RepresentativeResultDto>, GetRepresentativeQueryHandler>();
        services.AddScoped<IRequestHandler<GetRepresentativeByUserIdQuery, RepresentativeResultDto>, GetRepresentativeByUserIdQueryHandler>();
        services.AddScoped<IRequestHandler<GetAllRepresentativesQuery, IEnumerable<RepresentativeResultDto>>, GetAllRepresentativesQueryHandler>();


        // ProjectManager
        services.AddScoped<IRequestHandler<CreateProjectManagerCommand, int>, CreateProjectManagerCommandHandler>();
        services.AddScoped<IRequestHandler<CreateProjectManagerWithReturnCommand, ProjectManagerResultDto>, CreateProjectManagerWithReturnCommandHandler>();

        services.AddScoped<IRequestHandler<UpdateProjectManagerCommand, int>, UpdateProjectManagerCommandHandler>();

        services.AddScoped<IRequestHandler<DeleteProjectManagerCommand, bool>, DeleteProjectManagerCommandHandler>();

        services.AddScoped<IRequestHandler<GetProjectManagerQuery, ProjectManagerResultDto>, GetProjectManagerQueryHandler>();
        services.AddScoped<IRequestHandler<GetProjectManagerByUserIdQuery, ProjectManagerResultDto>, GetProjectManagerByUserIdQueryHandler>();
        services.AddScoped<IRequestHandler<GetAllProjectManagersQuery, IEnumerable<ProjectManagerResultDto>>, GetAllProjectManagersQueryHandler>();


        // Entrepreneur
        services.AddScoped<IRequestHandler<CreateEntrepreneurCommand, int>, CreateEntrepreneurCommandHandler>();
        services.AddScoped<IRequestHandler<CreateEntrepreneurWithReturnCommand, EntrepreneurResultDto>, CreateEntrepreneurWithReturnCommandHandler>();

        services.AddScoped<IRequestHandler<UpdateEntrepreneurCommand, int>, UpdateEntrepreneurCommandHandler>();

        services.AddScoped<IRequestHandler<DeleteEntrepreneurCommand, bool>, DeleteEntrepreneurCommandHandler>();

        services.AddScoped<IRequestHandler<GetEntrepreneurQuery, EntrepreneurResultDto>, GetEntrepreneurQueryHandler>();
        services.AddScoped<IRequestHandler<GetEntrepreneurByUserIdQuery, EntrepreneurResultDto>, GetEntrepreneurByUserIdQueryHandler>();
        services.AddScoped<IRequestHandler<GetAllEntrepreneursQuery, IEnumerable<EntrepreneurResultDto>>, GetAllEntrepreneursQueryHandler>();


        // Investment Application
        services.AddScoped<IRequestHandler<CreateInvestmentAppWithReturnCommand, InvestmentAppResultDto>, CreateInvestmentAppWithReturnCommandHandler>();

        services.AddScoped<IRequestHandler<GetInvestmentAppQuery, InvestmentAppResultDto>, GetInvestmentAppQueryHandler>();
        services.AddScoped<IRequestHandler<GetAllInvestmentAppsByUserIdQuery, IEnumerable<InvestmentAppResultDto>>, GetAllInvestmentAppsByUserIdQueryHandler>();


        // Entrepreneurship Application
        services.AddScoped<IRequestHandler<CreateEntrepreneurshipAppWithReturnCommand, EntrepreneurshipAppResultDto>, CreateEntrepreneurshipAppWithReturnCommandHandler>();

        services.AddScoped<IRequestHandler<GetEntrepreneurshipAppByIdCommand, EntrepreneurshipAppResultDto>, GetEntrepreneurshipAppByIdCommandHandler>();
        services.AddScoped<IRequestHandler<GetAllEntrepreneurshipAppsByUserIdQuery, IEnumerable<EntrepreneurshipAppResultDto>>, GetAllEntrepreneurshipAppsByUserIdQueryHandler>();


        // Project Management Application
        services.AddScoped<IRequestHandler<CreateProjectManagementAppWithReturnCommand, ProjectManagementAppResultDto>, CreateProjectManagementAppWithReturnCommandHandler>();

        services.AddScoped<IRequestHandler<GetProjectManagementAppQuery, ProjectManagementAppResultDto>, GetProjectManagementAppQueryHandler>();
        services.AddScoped<IRequestHandler<GetAllProjectManagementAppsByUserIdQuery, IEnumerable<ProjectManagementAppResultDto>>, GetAllProjectManagementAppsByUserIdQueryHandler>();


        // Representation Application
        services.AddScoped<IRequestHandler<CreateRepresentationAppWithReturnCommand, RepresentationAppResultDto>, CreateRepresentationAppWithReturnCommandHandler>();

        services.AddScoped<IRequestHandler<GetRepresentationAppByIdQuery, RepresentationAppResultDto>, GetRepresentationAppByIdQueryHandler>();
        services.AddScoped<IRequestHandler<GetAllRepresentationAppsByUserIdQuery, IEnumerable<RepresentationAppResultDto>>, GetAllRepresentationAppsByUserIdQueryHandler>();

        return services;
    }
}
