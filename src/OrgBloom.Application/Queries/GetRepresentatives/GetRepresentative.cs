using AutoMapper;
using OrgBloom.Domain.Entities;
using OrgBloom.Application.Interfaces;
using OrgBloom.Application.DTOs.Representatives;

namespace OrgBloom.Application.Queries.GetRepresentatives;

public record GetRepresentativeQuery : IRequest<RepresentativeResultDto>
{
    public GetRepresentativeQuery(GetRepresentativeQuery command) { Id = command.Id; }
    public int Id { get; set; }
}

public class GetRepresentativeQueryHendler(IRepository<Representative> repository, IMapper mapper) : IRequestHandler<GetRepresentativeQuery, RepresentativeResultDto>
{
    public async Task<RepresentativeResultDto> Handle(GetRepresentativeQuery request, CancellationToken cancellationToken)
        => mapper.Map<RepresentativeResultDto>(await repository.SelectAsync(i => i.Id.Equals(request.Id)));
}
