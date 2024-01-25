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