using EcoLink.Application.Entrepreneurs.DTOs;

namespace EcoLink.Application.Entrepreneurs.Queries.GetEntrepreneurs;

public record GetEntrepreneurQuery : IRequest<EntrepreneurResultDto>
{
    public GetEntrepreneurQuery(long id) { Id = id; }
    public long Id { get; set; }
}

public class GetEntrepreneurQueryHandler(IRepository<Entrepreneur> repository, IMapper mapper) : 
    IRequestHandler<GetEntrepreneurQuery, EntrepreneurResultDto>
{
    public async Task<EntrepreneurResultDto> Handle(GetEntrepreneurQuery request, CancellationToken cancellationToken)
        => mapper.Map<EntrepreneurResultDto>(await repository.SelectAsync(i => i.Id.Equals(request.Id)));
}
