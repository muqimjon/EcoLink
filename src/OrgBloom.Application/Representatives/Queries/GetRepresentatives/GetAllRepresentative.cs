using AutoMapper;
using OrgBloom.Application.Commons.Interfaces;
using OrgBloom.Application.Representatives.DTOs;
using OrgBloom.Domain.Entities.Representation;

namespace OrgBloom.Application.Representatives.Queries.GetRepresentatives;

public record GetAllRepresentativesQuery : IRequest<IEnumerable<RepresentativeResultDto>> { }

public class GetAllRepresentativesQueryHandler(IRepository<Representative> repository, IMapper mapper) : IRequestHandler<GetAllRepresentativesQuery, IEnumerable<RepresentativeResultDto>>
{
    public async Task<IEnumerable<RepresentativeResultDto>> Handle(GetAllRepresentativesQuery request, CancellationToken cancellationToken)
    => await Task.Run(() => mapper.Map<IEnumerable<RepresentativeResultDto>>(repository.SelectAll().ToList()));
}
