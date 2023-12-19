using AutoMapper;
using OrgBloom.Application.Users.DTOs;
using OrgBloom.Application.Investors.DTOs;
using OrgBloom.Application.Entrepreneurs.DTOs;
using OrgBloom.Application.ProjectManagers.DTOs;
using OrgBloom.Application.Representatives.DTOs;
using OrgBloom.Application.Users.Commands.CreateUsers;
using OrgBloom.Application.Users.Commands.UpdateUsers;
using OrgBloom.Application.Investors.Commands.CreateInvestors;
using OrgBloom.Application.Investors.Commands.UpdateInvestors;
using OrgBloom.Application.Entrepreneurs.Commands.CreateEntrepreneurs;
using OrgBloom.Application.Entrepreneurs.Commands.UpdateEntrepreneurs;
using OrgBloom.Application.ProjectManagers.Commands.CreateProjectManagers;
using OrgBloom.Application.ProjectManagers.Commands.UpdateProjectManagers;
using OrgBloom.Application.Representatives.Commands.CreateRepresentatives;
using OrgBloom.Application.Representatives.Commands.UpdateRepresentatives;
using OrgBloom.Domain.Entities.Representation;
using OrgBloom.Domain.Entities.Entrepreneurship;
using OrgBloom.Domain.Entities.ProjectManagement;
using OrgBloom.Domain.Entities.Investment;
using OrgBloom.Domain.Entities.Users;

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
    }
}