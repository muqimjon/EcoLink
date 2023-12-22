using OrgBloom.Application.Commons.Interfaces;
using OrgBloom.Domain.Entities.Entrepreneurship;
using OrgBloom.Application.EntrepreneurshipApps.DTOs;

namespace OrgBloom.Application.EntrepreneurshipApps.Queries.GetEntrepreneurshipApp;

public record GetAllEntrepreneurshipAppByUserIdQuery : IRequest<IEnumerable<EntrepreneurshipAppResultDto>>
{
    public GetAllEntrepreneurshipAppByUserIdQuery(long userId) { UserId = userId; }
    public long UserId { get; set; }
}

public class GetAllEntrepreneurshipAppByUserIdQueryHendler(IRepository<EntrepreneurshipApp> repository, IMapper mapper) : IRequestHandler<GetAllEntrepreneurshipAppByUserIdQuery, IEnumerable<EntrepreneurshipAppResultDto>>
{
    public async Task<IEnumerable<EntrepreneurshipAppResultDto>> Handle(GetAllEntrepreneurshipAppByUserIdQuery request, CancellationToken cancellationToken)
    {
        var query = repository.SelectAll(entity => entity.UserId.Equals(request.UserId));
        var entities = await Task.Run(() => query.ToList());

        return mapper.Map<IEnumerable<EntrepreneurshipAppResultDto>>(entities);
    }
}
