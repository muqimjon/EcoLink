namespace EcoLink.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MappingProfile));


        // User
        services.AddScoped<IRequestHandler<CreateUserCommand, int>, CreateUserCommandHandler>();
        services.AddScoped<IRequestHandler<CreateUserWithReturnCommand, UserResultDto>, CreateUserWithReturnCommandHandler>();

        services.AddScoped<IRequestHandler<UpdateUserCommand, int>, UpdateUserCommandHandler>();

        services.AddScoped<IRequestHandler<DeleteUserCommand, bool>, DeleteUserCommandHandler>();

        services.AddScoped<IRequestHandler<GetUserQuery, UserResultDto>, GetUserQueryHandler>();
        services.AddScoped<IRequestHandler<GetAllUsersQuery, IEnumerable<UserResultDto>>, GetAllUsersQueryHandler>();
        services.AddScoped<IRequestHandler<CreateUserWithReturnCommand, UserResultDto>, CreateUserWithReturnCommandHandler>();


        // Investor
        services.AddScoped<IRequestHandler<CreateInvestorCommand, int>, CreateInvestorCommandHandler>();
        services.AddScoped<IRequestHandler<CreateInvestorWithReturnCommand, InvestorResultDto>, CreateInvestorWithReturnCommandHandler>();

        services.AddScoped<IRequestHandler<UpdateInvestorCommand, int>, UpdateInvestorCommandHandler>();

        services.AddScoped<IRequestHandler<DeleteInvestorCommand, bool>, DeleteInvestorCommandHandler>();

        services.AddScoped<IRequestHandler<GetInvestorQuery, InvestorResultDto>, GetInvestorQueryHandler>();
        services.AddScoped<IRequestHandler<GetInvestorByUserIdQuery, InvestorResultDto>, GetInvestorByUserIdQueryHandler>();
        services.AddScoped<IRequestHandler<GetAllInvestorsQuery, IEnumerable<InvestorResultDto>>, GetAllInvestorsQueryHandler>();


        // Representation
        services.AddScoped<IRequestHandler<CreateRepresentativeCommand, int>, CreateRepresentativeCommandHandler>();
        services.AddScoped<IRequestHandler<CreateRepresentativeWithReturnCommand, RepresentativeResultDto>, CreateRepresentativeWithReturnCommandHandler>();

        services.AddScoped<IRequestHandler<UpdateRepresentativeCommand, int>, UpdateRepresentativeCommandHandler>();

        services.AddScoped<IRequestHandler<DeleteRepresentativeCommand, bool>, DeleteRepresentativeCommandHandler>();

        services.AddScoped<IRequestHandler<GetRepresentativeQuery, RepresentativeResultDto>, GetRepresentativeQueryHandler>();
        services.AddScoped<IRequestHandler<GetRepresentativeByUserIdQuery, RepresentativeResultDto>, GetRepresentativeByUserIdQueryHandler>();
        services.AddScoped<IRequestHandler<GetAllRepresentativesQuery, IEnumerable<RepresentativeResultDto>>, GetAllRepresentativesQueryHandler>();


        // ProjectManagement
        services.AddScoped<IRequestHandler<CreateProjectManagerCommand, int>, CreateProjectManagerCommandHandler>();
        services.AddScoped<IRequestHandler<CreateProjectManagerWithReturnCommand, ProjectManagerResultDto>, CreateProjectManagerWithReturnCommandHandler>();

        services.AddScoped<IRequestHandler<UpdateProjectManagerCommand, int>, UpdateProjectManagerCommandHandler>();

        services.AddScoped<IRequestHandler<DeleteProjectManagerCommand, bool>, DeleteProjectManagerCommandHandler>();

        services.AddScoped<IRequestHandler<GetProjectManagerQuery, ProjectManagerResultDto>, GetProjectManagerQueryHandler>();
        services.AddScoped<IRequestHandler<GetProjectManagerByUserIdQuery, ProjectManagerResultDto>, GetProjectManagerByUserIdQueryHandler>();
        services.AddScoped<IRequestHandler<GetAllProjectManagersQuery, IEnumerable<ProjectManagerResultDto>>, GetAllProjectManagersQueryHandler>();


        // Entrepreneurship
        services.AddScoped<IRequestHandler<CreateEntrepreneurCommand, int>, CreateEntrepreneurCommandHandler>();
        services.AddScoped<IRequestHandler<CreateEntrepreneurWithReturnCommand, EntrepreneurResultDto>, CreateEntrepreneurWithReturnCommandHandler>();

        services.AddScoped<IRequestHandler<UpdateEntrepreneurCommand, int>, UpdateEntrepreneurCommandHandler>();

        services.AddScoped<IRequestHandler<DeleteEntrepreneurCommand, bool>, DeleteEntrepreneurCommandHandler>();

        services.AddScoped<IRequestHandler<GetEntrepreneurQuery, EntrepreneurResultDto>, GetEntrepreneurQueryHandler>();
        services.AddScoped<IRequestHandler<GetEntrepreneurByUserIdQuery, EntrepreneurResultDto>, GetEntrepreneurByUserIdQueryHandler>();
        services.AddScoped<IRequestHandler<GetAllEntrepreneursQuery, IEnumerable<EntrepreneurResultDto>>, GetAllEntrepreneursQueryHandler>();


        // Investment Application
        services.AddScoped<IRequestHandler<CreateInvestmentAppWithReturnCommand, InvestmentAppResultDto>, CreateInvestmentAppWithReturnCommandHandler>();

        services.AddScoped<IRequestHandler<GetInvestmentAppQuery, InvestmentAppResultDto>, GetInvestmentAppQueryHandler>();
        services.AddScoped<IRequestHandler<GetAllInvestmentAppsByUserIdQuery, IEnumerable<InvestmentAppResultDto>>, GetAllInvestmentAppsByUserIdQueryHandler>();


        // Entrepreneurship Application
        services.AddScoped<IRequestHandler<CreateEntrepreneurshipAppWithReturnCommand, EntrepreneurshipAppResultDto>, CreateEntrepreneurshipAppWithReturnCommandHandler>();

        services.AddScoped<IRequestHandler<GetEntrepreneurshipAppByIdCommand, EntrepreneurshipAppResultDto>, GetEntrepreneurshipAppByIdCommandHandler>();
        services.AddScoped<IRequestHandler<GetAllEntrepreneurshipAppsByUserIdQuery, IEnumerable<EntrepreneurshipAppResultDto>>, GetAllEntrepreneurshipAppsByUserIdQueryHandler>();


        // Project Management Application
        services.AddScoped<IRequestHandler<CreateProjectManagementAppWithReturnCommand, ProjectManagementAppResultDto>, CreateProjectManagementAppWithReturnCommandHandler>();

        services.AddScoped<IRequestHandler<GetProjectManagementAppQuery, ProjectManagementAppResultDto>, GetProjectManagementAppQueryHandler>();
        services.AddScoped<IRequestHandler<GetAllProjectManagementAppsByUserIdQuery, IEnumerable<ProjectManagementAppResultDto>>, GetAllProjectManagementAppsByUserIdQueryHandler>();


        // Representation Application
        services.AddScoped<IRequestHandler<CreateRepresentationAppWithReturnCommand, RepresentationAppResultDto>, CreateRepresentationAppWithReturnCommandHandler>();

        services.AddScoped<IRequestHandler<GetRepresentationAppByIdQuery, RepresentationAppResultDto>, GetRepresentationAppByIdQueryHandler>();
        services.AddScoped<IRequestHandler<GetAllRepresentationAppsByUserIdQuery, IEnumerable<RepresentationAppResultDto>>, GetAllRepresentationAppsByUserIdQueryHandler>();

        return services;
    }
}
