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
        services.AddScoped<IRequestHandler<UpdatePhoneCommand, int>, UpdatePhoneCommandHandler>();
        services.AddScoped<IRequestHandler<UpdatePatronomycCommand, int>, UpdatePatronomycCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateLastNameCommand, int>, UpdateLastNameCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateFirstNameCommand, int>, UpdateFirstNameCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateEmailCommand, int>, UpdateEmailCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateDegreeCommand, int>, UpdateDegreeCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateDateOfBirthCommand, int>, UpdateDateOfBirthCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateLanguageCodeCommand, int>, UpdateLanguageCodeCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateStateCommand, int>, UpdateStateCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateStateAndProfessionCommand, int>, UpdateStateAndProfessionCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateProfessionCommand, int>, UpdateProfessionCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateAddressCommand, int>, UpdateAddressCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateLanguagesCommand, int>, UpdateLanguagesCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateExperienceCommand, int>, UpdateExperienceCommandHandler>();

        services.AddScoped<IRequestHandler<DeleteUserCommand, bool>, DeleteUserCommandHandler>();

        services.AddScoped<IRequestHandler<GetUserByIdQuery, UserApplyResultDto>, GetUserQueryHendler>();
        services.AddScoped<IRequestHandler<GetAllUsersQuery, IEnumerable<UserApplyResultDto>>, GetAllUsersQueryHandler>();
        services.AddScoped<IRequestHandler<GetUserByTelegramIdQuery, UserTelegramResultDto>, GetUserByTelegramIdQueryHendler>();
        services.AddScoped<IRequestHandler<GetLanguageCodeByIdQuery, string>, GetLanguageCodeByIdQueryHendler>();
        services.AddScoped<IRequestHandler<GetStateQuery, State>, GetStateQueryHendler>();
        services.AddScoped<IRequestHandler<GetProfessionQuery, UserProfession>, GetProfessionQueryHendler>();
        services.AddScoped<IRequestHandler<GetEmailQuery, string>, GetEmailQueryHendler>();
        services.AddScoped<IRequestHandler<GetAddressQuery, string>, GetAddressQueryHendler>();
        services.AddScoped<IRequestHandler<GetLanguagesQuery, string>, GetLanguagesQueryHendler>();
        services.AddScoped<IRequestHandler<GetExperienceQuery, string>, GetExperienceQueryHendler>();
        services.AddScoped<IRequestHandler<GetDateOfBirthQuery, DateTimeOffset>, GetDateOfBirthQueryHendler>();

        services.AddScoped<IRequestHandler<IsUserNewQuery, bool>, IsUserNewQueryHendler>();


        // Investor
        services.AddScoped<IRequestHandler<CreateInvestorCommand, int>, CreateInvestorCommandHandler>();
        services.AddScoped<IRequestHandler<CreateInvestorWithReturnCommand, InvestorResultDto>, CreateInvestorWithReturnCommandHandler>();

        services.AddScoped<IRequestHandler<UpdateInvestorCommand, int>, UpdateInvestorCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateInvestorSectorByUserIdCommand, int>, UpdateInvestorSectorByUserIdCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateInvestorInvestmentAmountByUserIdCommand, int>, UpdateInvestorInvestmentAmountByUserIdCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateInvestorIsSubmittedByUserIdCommand, int>, UpdateInvestorIsSubmittedByUserIdCommandHandler>();

        services.AddScoped<IRequestHandler<DeleteInvestorCommand, bool>, DeleteInvestorCommandHandler>();

        services.AddScoped<IRequestHandler<GetInvestorQuery, InvestorResultDto>, GetInvestorQueryHendler>();
        services.AddScoped<IRequestHandler<GetInvestmentAmountByUserIdQuery, string>, GetInvestmentAmountByUserIdQueryHendler>();
        services.AddScoped<IRequestHandler<GetInvestorByUserIdQuery, InvestorResultDto>, GetInvestorByUserIdQueryHendler>();
        services.AddScoped<IRequestHandler<GetAllInvestorsQuery, IEnumerable<InvestorResultDto>>, GetAllInvestorsQueryHandler>();


        // Representative
        services.AddScoped<IRequestHandler<CreateRepresentativeCommand, int>, CreateRepresentativeCommandHandler>();
        services.AddScoped<IRequestHandler<CreateRepresentativeWithReturnCommand, RepresentativeResultDto>, CreateRepresentativeWithReturnCommandHandler>();

        services.AddScoped<IRequestHandler<UpdateRepresentativeCommand, int>, UpdateRepresentativeCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateRepresentativeAreaByUserIdCommand, int>, UpdateRepresentativeAreaByUserIdCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateRepresentativeExpectationByUserIdCommand, int>, UpdateRepresentativeExpectationByUserIdCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateRepresentativePurposeByUserIdCommand, int>, UpdateRepresentativePurposeByUserIdCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateRepresentativeIsSubmittedByUserCommand, int>, UpdateRepresentativeIsSubmittedCommandByUserHandler>();

        services.AddScoped<IRequestHandler<DeleteRepresentativeCommand, bool>, DeleteRepresentativeCommandHandler>();

        services.AddScoped<IRequestHandler<GetRepresentativeQuery, RepresentativeResultDto>, GetRepresentativeQueryHendler>();
        services.AddScoped<IRequestHandler<GetRepresentativePurposeByUserIdQuery, string>, GetRepresentativePurposeByUserIdQueryHendler>();
        services.AddScoped<IRequestHandler<GetRepresentativeAreaByUserIdQuery, string>, GetRepresentativeAreaByUserIdQueryHendler>();
        services.AddScoped<IRequestHandler<GetRepresentativeExpectationByUserIdQuery, string>, GetRepresentativeExpectationByUserIdQueryHendler>();
        services.AddScoped<IRequestHandler<GetRepresentativeByUserIdQuery, RepresentativeResultDto>, GetRepresentativeByUserIdQueryHendler>();
        services.AddScoped<IRequestHandler<GetAllRepresentativesQuery, IEnumerable<RepresentativeResultDto>>, GetAllRepresentativesQueryHandler>();


        // ProjectManager
        services.AddScoped<IRequestHandler<CreateProjectManagerCommand, int>, CreateProjectManagerCommandHandler>();
        services.AddScoped<IRequestHandler<CreateProjectManagerWithReturnCommand, ProjectManagerResultDto>, CreateProjectManagerWithReturnCommandHandler>();

        services.AddScoped<IRequestHandler<UpdateProjectManagerCommand, int>, UpdateProjectManagerCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateProjectManagerProjectDirectionByUserIdCommand, int>, UpdateProjectManagerAreaByUserIdCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateProjectManagerExpectationByUserIdCommand, int>, UpdateProjectManagerExpectationByUserIdCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateProjectManagerPurposeVyUserIdCommand, int>, UpdateProjectManagerPurposeByUserIdCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateProjectManagerIsSubmittedByUserIdCommand, int>, UpdateProjectManagerIsSubmittedByUserIdCommandHandler>();

        services.AddScoped<IRequestHandler<DeleteProjectManagerCommand, bool>, DeleteProjectManagerCommandHandler>();

        services.AddScoped<IRequestHandler<GetProjectManagerQuery, ProjectManagerResultDto>, GetProjectManagerQueryHendler>();
        services.AddScoped<IRequestHandler<GetProjectManagerByUserIdQuery, ProjectManagerResultDto>, GetProjectManagerByUserIdQueryHendler>();
        services.AddScoped<IRequestHandler<GetProjectManagerProjectDirectionByUserIdQuery, string>, GetProjectManagerProjectDirectionByUserIdQueryHendler>();
        services.AddScoped<IRequestHandler<GetProjectManagerExpectationByUserIdQuery, string>, GetProjectManagerExpectationByUserIdQueryHendler>();
        services.AddScoped<IRequestHandler<GetProjectManagerPurposeByUserIdQuery, string>, GetProjectManagerPurposeByUserIdQueryHendler>();
        services.AddScoped<IRequestHandler<GetAllProjectManagersQuery, IEnumerable<ProjectManagerResultDto>>, GetAllProjectManagersQueryHandler>();


        // Entrepreneur
        services.AddScoped<IRequestHandler<CreateEntrepreneurCommand, int>, CreateEntrepreneurCommandHandler>();
        services.AddScoped<IRequestHandler<CreateEntrepreneurWithReturnCommand, EntrepreneurResultDto>, CreateEntrepreneurWithReturnCommandHandler>();

        services.AddScoped<IRequestHandler<UpdateEntrepreneurCommand, int>, UpdateEntrepreneurCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateEntrepreneurIsSubmittedByUserIdCommand, int>, UpdateEntrepreneurIsSubmittedByUserIdCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateEntrepreneurAssetsInvestedByUserIdCommand, int>, UpdateEntrepreneurAssetsInvestedByUserIdCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateEntrepreneurRequiredFundingByUserIdCommand, int>, UpdateEntrepreneurRequiredFundingByUserIdCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateEntrepreneurSectorByUserIdCommand, int>, UpdateEntrepreneurSectorByUserIdCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateEntrepreneurHelpTypeByUserIdCommand, int>, UpdateEntrepreneurHelpTypeByUserIdCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateEntrepreneurProjectByUserIdCommand, int>, UpdateEntrepreneurProjectCommandHandler>();

        services.AddScoped<IRequestHandler<DeleteEntrepreneurCommand, bool>, DeleteEntrepreneurCommandHandler>();

        services.AddScoped<IRequestHandler<GetEntrepreneurQuery, EntrepreneurResultDto>, GetEntrepreneurQueryHendler>();
        services.AddScoped<IRequestHandler<GetEntrepreneurByUserIdQuery, EntrepreneurResultDto>, GetEntrepreneurByUserIdQueryHendler>();
        services.AddScoped<IRequestHandler<GetEntrepreneurProjectByUserIdQuery, string>, GetEntrepreneurProjectByUserIdQueryHendler>();
        services.AddScoped<IRequestHandler<GetEntrepreneurRequiredFundingByUserIdQuery, string>, GetEntrepreneurRequiredFundingByUserIdQueryHendler>();
        services.AddScoped<IRequestHandler<GetEntrepreneurAssetsInvestedByUserIdQuery, string>, GetEntrepreneurAssetsInvestedByUserIdQueryHendler>();
        services.AddScoped<IRequestHandler<GetEntrepreneurHelpTypeByUserIdQuery, string>, GetEntrepreneurHelpTypeByUserIdQueryHendler>();
        services.AddScoped<IRequestHandler<GetAllEntrepreneursQuery, IEnumerable<EntrepreneurResultDto>>, GetAllEntrepreneursQueryHandler>();


        // Investment Application
        services.AddScoped<IRequestHandler<CreateInvestmentAppWithReturnCommand, InvestmentAppResultDto>, CreateInvestmentAppWithReturnCommandHandler>();

        services.AddScoped<IRequestHandler<GetInvestmentAppByIdQuery, InvestmentAppResultDto>, GetInvestmentAppByIdQueryHendler>();
        services.AddScoped<IRequestHandler<GetAllInvestmentAppsByUserIdQuery, IEnumerable<InvestmentAppResultDto>>, GetAllInvestmentAppsByUserIdQueryHendler>();


        // Entrepreneurship Application
        services.AddScoped<IRequestHandler<CreateEntrepreneurshipAppWithReturnCommand, EntrepreneurshipAppResultDto>, CreateEntrepreneurshipAppWithReturnCommandHandler>();

        services.AddScoped<IRequestHandler<GetEntrepreneurshipAppByIdCommand, EntrepreneurshipAppResultDto>, GetEntrepreneurshipAppByIdCommandHendler>();
        services.AddScoped<IRequestHandler<GetAllEntrepreneurshipAppsByUserIdQuery, IEnumerable<EntrepreneurshipAppResultDto>>, GetAllEntrepreneurshipAppsByUserIdQueryHendler>();


        // Project Management Application
        services.AddScoped<IRequestHandler<CreateProjectManagementAppWithReturnCommand, ProjectManagementAppResultDto>, CreateProjectManagementAppWithReturnCommandHandler>();

        services.AddScoped<IRequestHandler<GetProjectManagementAppByIdCommand, ProjectManagementAppResultDto>, GetProjectManagementAppByIdCommandHendler>();
        services.AddScoped<IRequestHandler<GetAllProjectManagementAppsByUserIdQuery, IEnumerable<ProjectManagementAppResultDto>>, GetAllProjectManagementAppsByUserIdQueryHendler>();


        // Representation Application
        services.AddScoped<IRequestHandler<CreateRepresentationAppWithReturnCommand, RepresentationAppResultDto>, CreateRepresentationAppWithReturnCommandHandler>();

        services.AddScoped<IRequestHandler<GetRepresentationAppByIdQuery, RepresentationAppResultDto>, GetRepresentationAppByIdQueryHendler>();
        services.AddScoped<IRequestHandler<GetAllRepresentationAppsByUserIdQuery, IEnumerable<RepresentationAppResultDto>>, GetAllRepresentationAppsByUserIdQueryHendler>();

        return services;
    }
}
