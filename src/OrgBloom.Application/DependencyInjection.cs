global using MediatR;
using OrgBloom.Application.Investors.DTOs;
using OrgBloom.Application.Commons.Mappers;
using OrgBloom.Application.Entrepreneurs.DTOs;
using Microsoft.Extensions.DependencyInjection;
using OrgBloom.Application.ProjectManagers.DTOs;
using OrgBloom.Application.Representatives.DTOs;
using OrgBloom.Application.Investors.Queries.GetInvestors;
using OrgBloom.Application.Investors.Commands.CreateInvestors;
using OrgBloom.Application.Investors.Commands.DeleteInvestors;
using OrgBloom.Application.Investors.Commands.UpdateInvestors;
using OrgBloom.Application.Entrepreneurs.Queries.GetEntrepreneurs;
using OrgBloom.Application.ProjectManagers.Queries.GetProjectManagers;
using OrgBloom.Application.Representatives.Queries.GetRepresentatives;
using OrgBloom.Application.Entrepreneurs.Commands.CreateEntrepreneurs;
using OrgBloom.Application.Entrepreneurs.Commands.UpdateEntrepreneurs;
using OrgBloom.Application.Entrepreneurs.Commands.DeleteEntrepreneurs;
using OrgBloom.Application.ProjectManagers.Commands.CreateProjectManagers;
using OrgBloom.Application.ProjectManagers.Commands.DeleteProjectManagers;
using OrgBloom.Application.ProjectManagers.Commands.UpdateProjectManagers;
using OrgBloom.Application.Representatives.Commands.CreateRepresentatives;
using OrgBloom.Application.Representatives.Commands.DeleteRepresentatives;
using OrgBloom.Application.Representatives.Commands.UpdateRepresentatives;
using OrgBloom.Application.Users.Commands.CreateUsers;
using OrgBloom.Application.Users.Commands.UpdateUsers;
using OrgBloom.Application.Users.Commands.DeleteUsers;
using OrgBloom.Application.Users.Queries.GetUsers;
using OrgBloom.Application.Users.DTOs;

namespace OrgBloom.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
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

        services.AddScoped<IRequestHandler<CreateUserCommand, int>, CreateUserCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateUserCommand, int>, UpdateUserCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateLanguageCodeCommand, int>, UpdateUserLanguageCodeCommandHandler>();
        services.AddScoped<IRequestHandler<DeleteUserCommand, bool>, DeleteUserCommandHandler>();
        services.AddScoped<IRequestHandler<GetUserByIdQuery, UserResultDto>, GetUserQueryHendler>();
        services.AddScoped<IRequestHandler<IsUserExistByTelegramIdQuery, bool>, IsUserExistByTelegramIdQueryHendler>();
        services.AddScoped<IRequestHandler<GetAllUsersQuery, IEnumerable<UserResultDto>>, GetAllUsersQueryHandler>();
        services.AddScoped<IRequestHandler<GetUserByTelegramIdQuery, UserTelegramResultDto>, GetUserByTelegramIdQueryHendler>();
        services.AddScoped<IRequestHandler<GetLanguageCodeByTelegramIdQuery, string>, GetLanguageCodeByTelegramIdQueryHendler>();

        return services;
    }
}
