global using MediatR;
using OrgBloom.Domain.Enums;
using OrgBloom.Application.Users.DTOs;
using OrgBloom.Application.Investors.DTOs;
using OrgBloom.Application.Commons.Mappers;
using OrgBloom.Application.Entrepreneurs.DTOs;
using Microsoft.Extensions.DependencyInjection;
using OrgBloom.Application.ProjectManagers.DTOs;
using OrgBloom.Application.Representatives.DTOs;
using OrgBloom.Application.Users.Queries.GetUsers;
using OrgBloom.Application.Users.Commands.CreateUsers;
using OrgBloom.Application.Users.Commands.UpdateUsers;
using OrgBloom.Application.Users.Commands.DeleteUsers;
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

namespace OrgBloom.Application;

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

        services.AddScoped<IRequestHandler<IsUserNewQuery, bool>, IsUserNewQueryHendler>();


        // Investor
        services.AddScoped<IRequestHandler<CreateInvestorCommand, int>, CreateInvestorCommandHandler>();
        services.AddScoped<IRequestHandler<CreateInvestorWithReturnCommand, InvestorResultDto>, CreateInvestorWithReturnCommandHandler>();

        services.AddScoped<IRequestHandler<UpdateInvestorCommand, int>, UpdateInvestorCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateInvestorSectorCommand, int>, UpdateInvestorSectorCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateInvestorInvestmentAmountCommand, int>, UpdateInvestorInvestmentAmountCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateInvestorIsSubmittedCommand, int>, UpdateInvestorIsSubmittedCommandHandler>();

        services.AddScoped<IRequestHandler<DeleteInvestorCommand, bool>, DeleteInvestorCommandHandler>();

        services.AddScoped<IRequestHandler<GetInvestorQuery, InvestorResultDto>, GetInvestorQueryHendler>();
        services.AddScoped<IRequestHandler<GetInvestorByUserIdQuery, InvestorResultDto>, GetInvestorByUserIdQueryHendler>();
        services.AddScoped<IRequestHandler<GetAllInvestorsQuery, IEnumerable<InvestorResultDto>>, GetAllInvestorsQueryHandler>();


        // Representative
        services.AddScoped<IRequestHandler<CreateRepresentativeCommand, int>, CreateRepresentativeCommandHandler>();
        services.AddScoped<IRequestHandler<CreateRepresentativeWithReturnCommand, RepresentativeResultDto>, CreateRepresentativeWithReturnCommandHandler>();

        services.AddScoped<IRequestHandler<UpdateRepresentativeCommand, int>, UpdateRepresentativeCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateRepresentativeAreaCommand, int>, UpdateRepresentativeAreaCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateRepresentativeExpectationCommand, int>, UpdateRepresentativeExpectationCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateRepresentativePurposeCommand, int>, UpdateRepresentativePurposeCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateRepresentativeIsSubmittedCommand, int>, UpdateRepresentativeIsSubmittedCommandHandler>();

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
        services.AddScoped<IRequestHandler<UpdateProjectManagerProjectDirectionCommand, int>, UpdateProjectManagerAreaCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateProjectManagerExpectationCommand, int>, UpdateProjectManagerExpectationCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateProjectManagerPurposeCommand, int>, UpdateProjectManagerPurposeCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateProjectManagerIsSubmittedCommand, int>, UpdateProjectManagerIsSubmittedCommandHandler>();

        services.AddScoped<IRequestHandler<DeleteProjectManagerCommand, bool>, DeleteProjectManagerCommandHandler>();

        services.AddScoped<IRequestHandler<GetProjectManagerQuery, ProjectManagerResultDto>, GetProjectManagerQueryHendler>();
        services.AddScoped<IRequestHandler<GetProjectManagerByUserIdQuery, ProjectManagerResultDto>, GetProjectManagerByUserIdQueryHendler>();
        services.AddScoped<IRequestHandler<GetProjectManagerProjectDirectionByUserIdQuery, string>, GetProjectManagerProjectDirectionByUserIdQueryHendler>();
        services.AddScoped<IRequestHandler<GetProjectManagerExpectationByUserIdQuery, string>, GetProjectManagerExpectationByUserIdQueryHendler>();
        services.AddScoped<IRequestHandler<GetAllProjectManagersQuery, IEnumerable<ProjectManagerResultDto>>, GetAllProjectManagersQueryHandler>();


        // Entrepreneur
        services.AddScoped<IRequestHandler<CreateEntrepreneurCommand, int>, CreateEntrepreneurCommandHandler>();
        services.AddScoped<IRequestHandler<CreateEntrepreneurWithReturnCommand, EntrepreneurResultDto>, CreateEntrepreneurWithReturnCommandHandler>();

        services.AddScoped<IRequestHandler<UpdateEntrepreneurCommand, int>, UpdateEntrepreneurCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateEntrepreneurIsSubmittedCommand, int>, UpdateEntrepreneurIsSubmittedCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateEntrepreneurAssetsInvestedCommand, int>, UpdateEntrepreneurAssetsInvestedCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateEntrepreneurRequiredFundingCommand, int>, UpdateEntrepreneurRequiredFundingCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateEntrepreneurHelpTypeCommand, int>, UpdateEntrepreneurHelpTypeCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateEntrepreneurProjectCommand, int>, UpdateEntrepreneurProjectCommandHandler>();


        services.AddScoped<IRequestHandler<DeleteEntrepreneurCommand, bool>, DeleteEntrepreneurCommandHandler>();

        services.AddScoped<IRequestHandler<GetEntrepreneurQuery, EntrepreneurResultDto>, GetEntrepreneurQueryHendler>();
        services.AddScoped<IRequestHandler<GetEntrepreneurByUserIdQuery, EntrepreneurResultDto>, GetEntrepreneurByUserIdQueryHendler>();
        services.AddScoped<IRequestHandler<GetEntrepreneurProjectByUserIdQuery, string>, GetEntrepreneurProjectByUserIdQueryHendler>();
        services.AddScoped<IRequestHandler<GetEntrepreneurRequiredFundingByUserIdQuery, string>, GetEntrepreneurRequiredFundingByUserIdQueryHendler>();
        services.AddScoped<IRequestHandler<GetEntrepreneurHelpTypeByUserIdQuery, string>, GetEntrepreneurHelpTypeByUserIdQueryHendler>();
        services.AddScoped<IRequestHandler<GetAllEntrepreneursQuery, IEnumerable<EntrepreneurResultDto>>, GetAllEntrepreneursQueryHandler>();

        return services;
    }
}
