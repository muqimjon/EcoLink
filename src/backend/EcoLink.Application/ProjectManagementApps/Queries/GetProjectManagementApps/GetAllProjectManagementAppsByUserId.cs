﻿using EcoLink.Application.ProjectManagementApps.DTOs;

namespace EcoLink.Application.ProjectManagementApps.Queries.GetProjectManagementApp;

public record GetAllProjectManagementAppsByUserIdQuery : IRequest<IEnumerable<ProjectManagementAppResultDto>>
{
    public GetAllProjectManagementAppsByUserIdQuery(long userId) { UserId = userId; }
    public long UserId { get; set; }
}

public class GetAllProjectManagementAppsByUserIdQueryHandler(IRepository<ProjectManagementApp> repository, IMapper mapper) : 
    IRequestHandler<GetAllProjectManagementAppsByUserIdQuery, IEnumerable<ProjectManagementAppResultDto>>
{
    public async Task<IEnumerable<ProjectManagementAppResultDto>> Handle(GetAllProjectManagementAppsByUserIdQuery request, CancellationToken cancellationToken)
    {
        var query = repository.SelectAll(entity => entity.UserId.Equals(request.UserId), includes: ["User"]);
        var entities = await Task.Run(() => query.ToList());

        return mapper.Map<IEnumerable<ProjectManagementAppResultDto>>(entities);
    }
}
