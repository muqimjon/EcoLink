using AutoMapper;
using OrgBloom.Domain.Entities;
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

namespace OrgBloom.Application.Commons.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Investor
        CreateMap<Investor, InvestorResultDto>();

        CreateMap<UpdateInvestorCommand, Investor>();

        CreateMap<CreateInvestorCommand, Investor>();
        CreateMap<CreateInvestorWithReturnCommand, Investor>();


        // Project Manager
        CreateMap<ProjectManager, ProjectManagerResultDto>();

        CreateMap<CreateProjectManagerCommand, ProjectManager>();

        CreateMap<UpdateProjectManagerCommand, ProjectManager>();


        // Entrepreneur
        CreateMap<Entrepreneur, EntrepreneurResultDto>();

        CreateMap<UpdateEntrepreneurCommand, Entrepreneur>();

        CreateMap<CreateEntrepreneurCommand, Entrepreneur>();


        // Representative
        CreateMap<Representative, RepresentativeResultDto>();

        CreateMap<UpdateRepresentativeCommand, Representative>();

        CreateMap<CreateRepresentativeCommand, Representative>();


        // User
        CreateMap<User, UserApplyResultDto>();
        CreateMap<User, UserTelegramResultDto>();

        CreateMap<UpdateUserCommand, User>();
        CreateMap<UpdateStateCommand, User>();
        CreateMap<UpdateLanguageCodeCommand, User>();
        CreateMap<UpdateProfessionCommand, User>();

        CreateMap<CreateUserCommand, User>();
        CreateMap<CreateUserWithReturnTgResultCommand, User>();
    }
}