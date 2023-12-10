using AutoMapper;
using OrgBloom.Domain.Entities;
using OrgBloom.Application.Interfaces;
using OrgBloom.Application.DTOs.Entrepreneurs;

namespace OrgBloom.Application.Queries.GetEntrepreneurs;

public record GetAllEntrepreneursQuery : IRequest<IEnumerable<EntrepreneurResultDto>> { }

public class GetAllEntrepreneursQueryHandler(IRepository<Entrepreneur> repository, IMapper mapper) : IRequestHandler<GetAllEntrepreneursQuery, IEnumerable<EntrepreneurResultDto>>
{
    public async Task<IEnumerable<EntrepreneurResultDto>> Handle(GetAllEntrepreneursQuery request, CancellationToken cancellationToken)
    => await Task.Run(() => mapper.Map<IEnumerable<EntrepreneurResultDto>>(repository.SelectAll().ToList()));
}
