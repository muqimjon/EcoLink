using OrgBloom.Application.Representatives.DTOs;

namespace OrgBloom.Application.Representatives.Queries.GetRepresentatives;

public record GetRepresentativeQuery : IRequest<RepresentativeResultDto>
{
    public GetRepresentativeQuery(GetRepresentativeQuery command) { Id = command.Id; }
    public long Id { get; set; }
}

public class GetRepresentativeQueryHendler(IRepository<Representative> repository, IMapper mapper) : 
    IRequestHandler<GetRepresentativeQuery, RepresentativeResultDto>
{
    public async Task<RepresentativeResultDto> Handle(GetRepresentativeQuery request, CancellationToken cancellationToken)
        => mapper.Map<RepresentativeResultDto>(await repository.SelectAsync(i => i.Id.Equals(request.Id)));
}
