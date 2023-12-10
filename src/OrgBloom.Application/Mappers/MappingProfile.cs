using AutoMapper;
using OrgBloom.Domain.Entities;
using OrgBloom.Application.DTOs.Investors;
using OrgBloom.Application.DTOs.Entrepreneurs;
using OrgBloom.Application.DTOs.ProjectManagers;
using OrgBloom.Application.DTOs.Representatives;
using OrgBloom.Application.Commands.Investors.CreateInvestors;
using OrgBloom.Application.Commands.Investors.UpdateInvestors;
using OrgBloom.Application.Commands.Entrepreneurs.UpdateEntrepreneurs;
using OrgBloom.Application.Commands.Entrepreneurs.CreateEntrepreneurs;
using OrgBloom.Application.Commands.ProjectManagers.UpdateProjectManagers;
using OrgBloom.Application.Commands.ProjectManagers.CreateProjectManagers;
using OrgBloom.Application.Commands.Representatives.UpdateRepresentatives;
using OrgBloom.Application.Commands.Representatives.CreateRepresentatives;

namespace OrgBloom.Application.Mappers;

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
    }
}