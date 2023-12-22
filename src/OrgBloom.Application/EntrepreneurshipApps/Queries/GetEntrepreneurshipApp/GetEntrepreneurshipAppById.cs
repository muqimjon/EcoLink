using OrgBloom.Application.EntrepreneurshipApps.DTOs;

namespace OrgBloom.Application.EntrepreneurshipApps.Queries.GetEntrepreneurshipApp;

public record GetEntrepreneurshipAppByIdCommand : IRequest<EntrepreneurshipAppResultDto>
{
    public GetEntrepreneurshipAppByIdCommand(long id) { Id = id; }
    public long Id { get; set; }
}

public class GetEntrepreneurshipAppByIdCommandHendler(IRepository<EntrepreneurshipApp> repository, IMapper mapper) : 
    IRequestHandler<GetEntrepreneurshipAppByIdCommand, EntrepreneurshipAppResultDto>
{
    public async Task<EntrepreneurshipAppResultDto> Handle(GetEntrepreneurshipAppByIdCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.Id.Equals(request.Id));

        return mapper.Map<EntrepreneurshipAppResultDto>(entity);
    }
}
