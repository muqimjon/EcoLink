using EcoLink.Application.ProjectManagementApps.Commands.CreateProjectManagement;
using EcoLink.Application.RepresentationApps.Commands.CreateRepresentation;

namespace EcoLink.Application.Commons.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // User
        CreateMap<User, UserResultDto>();

        CreateMap<UpdateUserCommand, User>();

        CreateMap<CreateUserCommand, User>();
        CreateMap<CreateUserWithReturnCommand, User>();


        // Investment Application
        CreateMap<InvestmentApp, InvestmentAppResultDto>();
        CreateMap<InvestmentApp, InvestmentAppForSheetsDto>();

        CreateMap<UpdateInvestmentStatusCommand, InvestmentApp>();

        CreateMap<CreateInvestmentWithReturnCommand, InvestmentApp>();


        // Entrepreneurship Application
        CreateMap<EntrepreneurshipApp, EntrepreneurshipAppResultDto>();
        CreateMap<EntrepreneurshipApp, EntrepreneurshipAppForSheetsDto>();

        CreateMap<UpdateEntrepreneurshipStatusCommand, EntrepreneurshipApp>();

        CreateMap<CreateEntrepreneurshipWithReturnCommand, EntrepreneurshipApp>();


        // Project Management Application
        CreateMap<ProjectManagementApp, ProjectManagementAppResultDto>();
        CreateMap<ProjectManagementApp, ProjectManagementAppForSheetsDto>();

        CreateMap<UpdateProjectManagementStatusCommand, ProjectManagementApp>();

        CreateMap<CreateProjectManagementWithReturnCommand, ProjectManagementApp>();


        // Representation Application
        CreateMap<RepresentationApp, RepresentationAppResultDto>();
        CreateMap<RepresentationApp, RepresentationAppForSheetsDto>();

        CreateMap<UpdateRepresentationStatusCommand, RepresentationApp>();

        CreateMap<CreateRepresentationWithReturnCommand, RepresentationApp>();
    }
}