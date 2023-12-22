using OrgBloom.Application.Commons.Interfaces;
using OrgBloom.Domain.Entities.Representation;
using OrgBloom.Application.RepresentationApps.DTOs;

namespace OrgBloom.Application.RepresentationApps.Queries.GetRepresentationApp;

public record GetRepresentationAppByIdCommand : IRequest<RepresentationAppResultDto>
{
    public GetRepresentationAppByIdCommand(long id) { Id = id; }
    public long Id { get; set; }
}

public class GetRepresentationAppByIdCommandHendler(IRepository<RepresentationApp> repository, IMapper mapper) : IRequestHandler<GetRepresentationAppByIdCommand, RepresentationAppResultDto>
{
    public async Task<RepresentationAppResultDto> Handle(GetRepresentationAppByIdCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.Id.Equals(request.Id));
        return mapper.Map<RepresentationAppResultDto>(entity);
    }
}
