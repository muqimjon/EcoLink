namespace EcoLink.Application.EntrepreneurshipApps.Queries.GetEntrepreneurshipApp;

public record GetAllEntrepreneurshipAppsByUserIdQuery : IRequest<IEnumerable<EntrepreneurshipAppResultDto>>
{
    public GetAllEntrepreneurshipAppsByUserIdQuery(long userId) { UserId = userId; }
    public long UserId { get; set; }
}

public class GetAllEntrepreneurshipAppsByUserIdQueryHandler(IRepository<EntrepreneurshipApp> repository, IMapper mapper) : 
    IRequestHandler<GetAllEntrepreneurshipAppsByUserIdQuery, IEnumerable<EntrepreneurshipAppResultDto>>
{
    public async Task<IEnumerable<EntrepreneurshipAppResultDto>> Handle(GetAllEntrepreneurshipAppsByUserIdQuery request, CancellationToken cancellationToken)
    {
        var query = repository.SelectAll(expression: entity => entity.UserId.Equals(request.UserId), includes: [ "User" ]);
        var entities = await Task.Run(() => query.ToList());

        return mapper.Map<IEnumerable<EntrepreneurshipAppResultDto>>(entities);
    }
}
