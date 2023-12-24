using OrgBloom.Application.Investors.DTOs;
using OrgBloom.Application.Entrepreneurs.DTOs;
using OrgBloom.Application.InvestmentApps.DTOs;
using OrgBloom.Application.Representatives.DTOs;
using OrgBloom.Application.ProjectManagers.DTOs;
using OrgBloom.Application.RepresentationApps.DTOs;
using OrgBloom.Application.EntrepreneurshipApps.DTOs;
using OrgBloom.Application.ProjectManagementApps.DTOs;
using OrgBloom.Application.Users.Commands.CreateUsers;
using OrgBloom.Application.Users.Commands.UpdateUsers;
using OrgBloom.Application.Investors.Commands.CreateInvestors;
using OrgBloom.Application.Investors.Commands.UpdateInvestors;
using OrgBloom.Application.Entrepreneurs.Commands.CreateEntrepreneurs;
using OrgBloom.Application.Entrepreneurs.Commands.UpdateEntrepreneurs;
using OrgBloom.Application.InvestmentApps.Commands.CreateInvestmentApps;
using OrgBloom.Application.ProjectManagers.Commands.CreateProjectManagers;
using OrgBloom.Application.ProjectManagers.Commands.UpdateProjectManagers;
using OrgBloom.Application.Representatives.Commands.CreateRepresentatives;
using OrgBloom.Application.Representatives.Commands.UpdateRepresentatives;
using OrgBloom.Application.RepresentationApps.Commands.CreateRepresentationApps;
using OrgBloom.Application.ProjectManagementApps.Commands.CreateProjectManagementApps;

namespace OrgBloom.Application.Commons.Mappers;

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