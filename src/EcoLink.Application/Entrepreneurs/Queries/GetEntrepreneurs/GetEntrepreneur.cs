using EcoLink.Application.Entrepreneurs.DTOs;

namespace EcoLink.Application.Entrepreneurs.Queries.GetEntrepreneurs;

public record GetEntrepreneurQuery : IRequest<EntrepreneurResultDto>
{
    public GetEntrepreneurQuery(GetEntrepreneurQuery command) { Id = command.Id; }
    public int Id { get; set; }
}

public class GetEntrepreneurQueryHendler(IRepository<Entrepreneur> repository, IMapper mapper) : 
    IRequestHandler<GetEntrepreneurQuery, EntrepreneurResultDto>
{
    public async Task<EntrepreneurResultDto> Handle(GetEntrepreneurQuery request, CancellationToken cancellationToken)
        => mapper.Map<EntrepreneurResultDto>(await repository.SelectAsync(i => i.Id.Equals(request.Id)));
}
