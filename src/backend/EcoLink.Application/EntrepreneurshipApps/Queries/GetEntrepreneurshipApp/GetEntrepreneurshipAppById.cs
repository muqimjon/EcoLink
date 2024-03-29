﻿using EcoLink.Application.EntrepreneurshipApps.DTOs;

namespace EcoLink.Application.EntrepreneurshipApps.Queries.GetEntrepreneurshipApp;

public record GetEntrepreneurshipAppByIdCommand : IRequest<EntrepreneurshipAppResultDto>
{
    public GetEntrepreneurshipAppByIdCommand(long id) { Id = id; }
    public long Id { get; set; }
}

public class GetEntrepreneurshipAppByIdCommandHandler(IRepository<EntrepreneurshipApp> repository, IMapper mapper) : 
    IRequestHandler<GetEntrepreneurshipAppByIdCommand, EntrepreneurshipAppResultDto>
{
    public async Task<EntrepreneurshipAppResultDto> Handle(GetEntrepreneurshipAppByIdCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.Id.Equals(request.Id));

        return mapper.Map<EntrepreneurshipAppResultDto>(entity);
    }
}
