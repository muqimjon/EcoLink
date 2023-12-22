using OrgBloom.Application.Commons.Interfaces;
using OrgBloom.Domain.Entities.Representation;
using OrgBloom.Application.RepresentationApps.DTOs;

namespace OrgBloom.Application.RepresentationApps.Queries.GetRepresentationApp;

public record GetAllRepresentationAppByUserIdQuery : IRequest<IEnumerable<RepresentationAppResultDto>>
{
    public GetAllRepresentationAppByUserIdQuery(long userId) { UserId = userId; }
    public long UserId { get; set; }
}

public class GetAllRepresentationAppByUserIdQueryHendler(IRepository<RepresentationApp> repository, IMapper mapper) : IRequestHandler<GetAllRepresentationAppByUserIdQuery, IEnumerable<RepresentationAppResultDto>>
{
    public async Task<IEnumerable<RepresentationAppResultDto>> Handle(GetAllRepresentationAppByUserIdQuery request, CancellationToken cancellationToken)
    {
        var query = repository.SelectAll(entity => entity.UserId.Equals(request.UserId));
        var entities = await Task.Run(() => query.ToList());

        return mapper.Map<IEnumerable<RepresentationAppResultDto>>(entities);
    }
}
