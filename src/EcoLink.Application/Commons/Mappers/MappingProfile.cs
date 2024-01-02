using EcoLink.Application.Investors.DTOs;
using EcoLink.Application.Entrepreneurs.DTOs;
using EcoLink.Application.InvestmentApps.DTOs;
using EcoLink.Application.Representatives.DTOs;
using EcoLink.Application.ProjectManagers.DTOs;
using EcoLink.Application.RepresentationApps.DTOs;
using EcoLink.Application.EntrepreneurshipApps.DTOs;
using EcoLink.Application.ProjectManagementApps.DTOs;
using EcoLink.Application.Users.Commands.CreateUsers;
using EcoLink.Application.Users.Commands.UpdateUsers;
using EcoLink.Application.Investors.Commands.CreateInvestors;
using EcoLink.Application.Investors.Commands.UpdateInvestors;
using EcoLink.Application.Entrepreneurs.Commands.CreateEntrepreneurs;
using EcoLink.Application.Entrepreneurs.Commands.UpdateEntrepreneurs;
using EcoLink.Application.InvestmentApps.Commands.CreateInvestmentApps;
using EcoLink.Application.ProjectManagers.Commands.CreateProjectManagers;
using EcoLink.Application.ProjectManagers.Commands.UpdateProjectManagers;
using EcoLink.Application.Representatives.Commands.CreateRepresentatives;
using EcoLink.Application.Representatives.Commands.UpdateRepresentatives;
using EcoLink.Application.RepresentationApps.Commands.CreateRepresentationApps;
using EcoLink.Application.ProjectManagementApps.Commands.CreateProjectManagementApps;

namespace EcoLink.Application.Commons.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Investor
        CreateMap<Investor, InvestorResultDto>();

        CreateMap<UpdateInvestorCommand, Investor>();
        CreateMap<UpdateInvestorInvestmentAmountByUserIdCommand, Investor>();
        CreateMap<UpdateInvestorIsSubmittedByUserIdCommand, Investor>();
        CreateMap<UpdateInvestorSectorByUserIdCommand, Investor>();

        CreateMap<CreateInvestorCommand, Investor>();
        CreateMap<CreateInvestorWithReturnCommand, Investor>();


        // Project Manager
        CreateMap<ProjectManager, ProjectManagerResultDto>();

        CreateMap<UpdateProjectManagerCommand, ProjectManager>();
        CreateMap<UpdateProjectManagerProjectDirectionByUserIdCommand, ProjectManager>();
        CreateMap<UpdateProjectManagerExpectationByUserIdCommand, ProjectManager>();
        CreateMap<UpdateProjectManagerIsSubmittedByUserIdCommand, ProjectManager>();
        CreateMap<UpdateProjectManagerPurposeVyUserIdCommand, ProjectManager>();

        CreateMap<CreateProjectManagerCommand, ProjectManager>();
        CreateMap<CreateProjectManagerWithReturnCommand, ProjectManager>();


        // Entrepreneur
        CreateMap<Entrepreneur, EntrepreneurResultDto>();

        CreateMap<UpdateEntrepreneurCommand, Entrepreneur>();
        CreateMap<UpdateEntrepreneurHelpTypeByUserIdCommand, Entrepreneur>();
        CreateMap<UpdateEntrepreneurAssetsInvestedByUserIdCommand, Entrepreneur>();
        CreateMap<UpdateEntrepreneurRequiredFundingByUserIdCommand, Entrepreneur>();
        CreateMap<UpdateEntrepreneurIsSubmittedByUserIdCommand, Entrepreneur>();
        CreateMap<UpdateEntrepreneurProjectByUserIdCommand, Entrepreneur>();
        CreateMap<UpdateEntrepreneurSectorByUserIdCommand, Entrepreneur>();

        CreateMap<CreateEntrepreneurCommand, Entrepreneur>();
        CreateMap<CreateEntrepreneurWithReturnCommand, Entrepreneur>();


        // Representative
        CreateMap<Representative, RepresentativeResultDto>();

        CreateMap<UpdateRepresentativeCommand, Representative>();
        CreateMap<UpdateRepresentativeAreaByUserIdCommand, Representative>();
        CreateMap<UpdateRepresentativeExpectationByUserIdCommand, Representative>();
        CreateMap<UpdateRepresentativeIsSubmittedByUserCommand, Representative>();
        CreateMap<UpdateRepresentativePurposeByUserIdCommand, Representative>();

        CreateMap<CreateRepresentativeCommand, Representative>();
        CreateMap<CreateRepresentativeWithReturnCommand, Representative>();


        // User
        CreateMap<User, UserApplyResultDto>();
        CreateMap<User, UserTelegramResultDto>();
        CreateMap<User, UserApplyResultDto>();

        CreateMap<UpdateUserCommand, User>();
        CreateMap<UpdateDateOfBirthCommand, User>();
        CreateMap<UpdateDegreeCommand, User>();
        CreateMap<UpdateEmailCommand, User>();
        CreateMap<UpdateFirstNameCommand, User>();
        CreateMap<UpdateLastNameCommand, User>();
        CreateMap<UpdatePatronomycCommand, User>();
        CreateMap<UpdatePhoneCommand, User>();
        CreateMap<UpdateStateCommand, User>();
        CreateMap<UpdateStateAndProfessionCommand, User>();
        CreateMap<UpdateLanguageCodeCommand, User>();
        CreateMap<UpdateProfessionCommand, User>();
        CreateMap<UpdateAddressCommand, User>();
        CreateMap<UpdateLanguagesCommand, User>();
        CreateMap<UpdateExperienceCommand, User>();

        CreateMap<CreateUserCommand, User>();
        CreateMap<CreateUserWithReturnTgResultCommand, User>();


        // Investment Application
        CreateMap<InvestmentApp, InvestmentAppResultDto>();
        CreateMap<InvestmentApp, InvestmentAppForSheetsDto>();

        CreateMap<CreateInvestmentAppWithReturnCommand, InvestmentApp>();


        // Entrepreneurship Application
        CreateMap<EntrepreneurshipApp, EntrepreneurshipAppResultDto>();
        CreateMap<EntrepreneurshipApp, EntrepreneurshipAppForSheetsDto>();

        CreateMap<CreateEntrepreneurshipAppWithReturnCommand, EntrepreneurshipApp>();


        // Project Management Application
        CreateMap<ProjectManagementApp, ProjectManagementAppResultDto>();
        CreateMap<ProjectManagementApp, ProjectManagementAppForSheetsDto>();

        CreateMap<CreateProjectManagementAppWithReturnCommand, ProjectManagementApp>();


        // Representation Application
        CreateMap<RepresentationApp, RepresentationAppResultDto>();
        CreateMap<RepresentationApp, RepresentationAppForSheetsDto>();

        CreateMap<CreateRepresentationAppWithReturnCommand, RepresentationApp>();
    }
}