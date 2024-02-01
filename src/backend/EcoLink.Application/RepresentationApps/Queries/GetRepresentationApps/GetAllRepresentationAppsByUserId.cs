namespace EcoLink.Application.RepresentationApps.Queries.GetRepresentationApp;

public record GetAllRepresentationAppsByUserIdQuery : IRequest<IEnumerable<RepresentationAppResultDto>>
{
    public GetAllRepresentationAppsByUserIdQuery(long userId) { UserId = userId; }
    public long UserId { get; set; }
}

public class GetAllRepresentationAppsByUserIdQueryHandler(IRepository<RepresentationApp> repository, IMapper mapper) : 
    IRequestHandler<GetAllRepresentationAppsByUserIdQuery, IEnumerable<RepresentationAppResultDto>>
{
    public async Task<IEnumerable<RepresentationAppResultDto>> Handle(GetAllRepresentationAppsByUserIdQuery request, CancellationToken cancellationToken)
    {
        var query = repository.SelectAll(entity => entity.UserId.Equals(request.UserId), includes: ["User"]);
        var entities = await Task.Run(() => query.ToList());

        return mapper.Map<IEnumerable<RepresentationAppResultDto>>(entities);
    }
}
