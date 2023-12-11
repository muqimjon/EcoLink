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
        CreateMap<CreateInvestorCommand, Investor>();
        CreateMap<UpdateInvestorCommand, Investor>();
        CreateMap<Investor, InvestorResultDto>();

        // Project Manager
        CreateMap<ProjectManager, ProjectManagerResultDto>();
        CreateMap<CreateProjectManagerCommand, ProjectManager>();
        CreateMap<UpdateProjectManagerCommand, ProjectManager>();

        // Entrepreneur
        CreateMap<Entrepreneur, EntrepreneurResultDto>();
        CreateMap<CreateEntrepreneurCommand, Entrepreneur>();
        CreateMap<UpdateEntrepreneurCommand, Entrepreneur>();

        // Representative
        CreateMap<Representative, RepresentativeResultDto>();
        CreateMap<CreateRepresentativeCommand, Representative>();
        CreateMap<UpdateRepresentativeCommand, Representative>();

        // User
        CreateMap<User, UserResultDto>();
        CreateMap<CreateUserCommand, User>();
        CreateMap<UpdateUserCommand, User>();
    }
}