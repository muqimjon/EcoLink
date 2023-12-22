using OrgBloom.Application.RepresentationApps.DTOs;

namespace OrgBloom.Application.RepresentationApps.Queries.GetRepresentationApp;

public record GetRepresentationAppByIdQuery : IRequest<RepresentationAppResultDto>
{
    public GetRepresentationAppByIdQuery(long id) { Id = id; }
    public long Id { get; set; }
}

public class GetRepresentationAppByIdQueryHendler(IRepository<RepresentationApp> repository, IMapper mapper) : 
    IRequestHandler<GetRepresentationAppByIdQuery, RepresentationAppResultDto>
{
    public async Task<RepresentationAppResultDto> Handle(GetRepresentationAppByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.Id.Equals(request.Id));
        return mapper.Map<RepresentationAppResultDto>(entity);
    }
}
