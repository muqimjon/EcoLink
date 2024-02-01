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
        services.AddScoped<IRequestHandler<GetUserByTelegramIdQuery, UserResultDto>, GetUserByTelegramIdQueryHandler>();


        // Investment
        services.AddScoped<IRequestHandler<CreateInvestmentWithReturnCommand, InvestmentAppResultDto>, CreateInvestmentAppWithReturnCommandHandler>();

        services.AddScoped<IRequestHandler<UpdateInvestmentStatusCommand, InvestmentAppResultDto>, UpdateInvestmentStatusCommandHandler>();

        services.AddScoped<IRequestHandler<GetInvestmentAppQuery, InvestmentAppResultDto>, GetInvestmentAppQueryHandler>();
        services.AddScoped<IRequestHandler<GetAllInvestmentAppsByUserIdQuery, IEnumerable<InvestmentAppResultDto>>, GetAllInvestmentAppsByUserIdQueryHandler>();


        // Entrepreneurship
        services.AddScoped<IRequestHandler<CreateEntrepreneurshipWithReturnCommand, EntrepreneurshipAppResultDto>, CreateEntrepreneurshipAppWithReturnCommandHandler>();

        services.AddScoped<IRequestHandler<UpdateEntrepreneurshipStatusCommand, EntrepreneurshipAppResultDto>, UpdateEntrepreneurshipStatusCommandHandler>();

        services.AddScoped<IRequestHandler<GetEntrepreneurshipAppByIdCommand, EntrepreneurshipAppResultDto>, GetEntrepreneurshipAppByIdCommandHandler>();
        services.AddScoped<IRequestHandler<GetAllEntrepreneurshipAppsByUserIdQuery, IEnumerable<EntrepreneurshipAppResultDto>>, GetAllEntrepreneurshipAppsByUserIdQueryHandler>();


        // Project Management
        services.AddScoped<IRequestHandler<CreateProjectManagementWithReturnCommand, ProjectManagementAppResultDto>, CreateProjectManagementAppWithReturnCommandHandler>();

        services.AddScoped<IRequestHandler<UpdateProjectManagementStatusCommand, ProjectManagementAppResultDto>, UpdateProjectManagementStatusCommandHandler>();

        services.AddScoped<IRequestHandler<GetProjectManagementAppQuery, ProjectManagementAppResultDto>, GetProjectManagementAppQueryHandler>();
        services.AddScoped<IRequestHandler<GetAllProjectManagementAppsByUserIdQuery, IEnumerable<ProjectManagementAppResultDto>>, GetAllProjectManagementAppsByUserIdQueryHandler>();


        // Representation
        services.AddScoped<IRequestHandler<CreateRepresentationWithReturnCommand, RepresentationAppResultDto>, CreateRepresentationAppWithReturnCommandHandler>();

        services.AddScoped<IRequestHandler<UpdateRepresentationStatusCommand, RepresentationAppResultDto>, UpdateRepresentationStatusCommandHandler>();

        services.AddScoped<IRequestHandler<GetRepresentationAppByIdQuery, RepresentationAppResultDto>, GetRepresentationAppByIdQueryHandler>();
        services.AddScoped<IRequestHandler<GetAllRepresentationAppsByUserIdQuery, IEnumerable<RepresentationAppResultDto>>, GetAllRepresentationAppsByUserIdQueryHandler>();

        return services;
    }
}
