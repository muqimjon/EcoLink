using OrgBloom.Application.Entrepreneurs.DTOs;

namespace OrgBloom.Application.Entrepreneurs.Queries.GetEntrepreneurs;

public record GetAllEntrepreneursQuery : IRequest<IEnumerable<EntrepreneurResultDto>> { }

public class GetAllEntrepreneursQueryHandler(IRepository<Entrepreneur> repository, IMapper mapper) : 
    IRequestHandler<GetAllEntrepreneursQuery, IEnumerable<EntrepreneurResultDto>>
{
    public async Task<IEnumerable<EntrepreneurResultDto>> Handle(GetAllEntrepreneursQuery request, CancellationToken cancellationToken)
        => await Task.Run(() => mapper.Map<IEnumerable<EntrepreneurResultDto>>(repository.SelectAll().ToList()));
}
