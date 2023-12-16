using OrgBloom.Application.Commons.Interfaces;
using OrgBloom.Domain.Entities.ProjectManagement;

namespace OrgBloom.Application.ProjectManagers.Queries.GetProjectManagers;

public record GetProjectManagerProjectDirectionByUserIdQuery : IRequest<string>
{
    public GetProjectManagerProjectDirectionByUserIdQuery(long id) { UserId = id; }
    public long UserId { get; set; }
}

public class GetProjectManagerProjectDirectionByUserIdQueryHendler(IRepository<ProjectManager> repository) : IRequestHandler<GetProjectManagerProjectDirectionByUserIdQuery, string>
{
    public async Task<string> Handle(GetProjectManagerProjectDirectionByUserIdQuery request, CancellationToken cancellationToken)
        => (await repository.SelectAsync(i => i.UserId.Equals(request.UserId)) ?? new()).ProjectDirection!;
}
