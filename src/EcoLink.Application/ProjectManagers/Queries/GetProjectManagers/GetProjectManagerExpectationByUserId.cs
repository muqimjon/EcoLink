﻿namespace EcoLink.Application.ProjectManagers.Queries.GetProjectManagers;

public record GetProjectManagerExpectationByUserIdQuery : IRequest<string>
{
    public GetProjectManagerExpectationByUserIdQuery(long id) { UserId = id; }
    public long UserId { get; set; }
}

public class GetProjectManagerExpectationByUserIdQueryHendler(IRepository<ProjectManager> repository) : 
    IRequestHandler<GetProjectManagerExpectationByUserIdQuery, string>
{
    public async Task<string> Handle(GetProjectManagerExpectationByUserIdQuery request, CancellationToken cancellationToken)
        => (await repository.SelectAsync(i => i.UserId.Equals(request.UserId)) ?? new()).Expectation!;
}
