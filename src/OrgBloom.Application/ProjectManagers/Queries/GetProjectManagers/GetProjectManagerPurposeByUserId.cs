namespace OrgBloom.Application.ProjectManagers.Queries.GetProjectManagers;

public record GetProjectManagerPurposeByUserIdQuery : IRequest<string>
{
    public GetProjectManagerPurposeByUserIdQuery(long id) { UserId = id; }
    public long UserId { get; set; }
}

public class GetProjectManagerPurposeByUserIdQueryHendler(IRepository<ProjectManager> repository) : 
    IRequestHandler<GetProjectManagerPurposeByUserIdQuery, string>
{
    public async Task<string> Handle(GetProjectManagerPurposeByUserIdQuery request, CancellationToken cancellationToken)
        => (await repository.SelectAsync(i => i.UserId.Equals(request.UserId)) ?? new()).Purpose!;
}
