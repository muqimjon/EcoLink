using AutoMapper;
using OrgBloom.Domain.Entities;
using OrgBloom.Application.Commons.Interfaces;

namespace OrgBloom.Application.Entrepreneurs.Queries.GetEntrepreneurs;

public record GetEntrepreneurHelpTypeByUserIdQuery : IRequest<string>
{
    public GetEntrepreneurHelpTypeByUserIdQuery(long id) { UserId = id; }
    public long UserId { get; set; }
}

public class GetEntrepreneurHelpTypeByUserIdQueryHendler(IRepository<Entrepreneur> repository) : IRequestHandler<GetEntrepreneurHelpTypeByUserIdQuery, string>
{
    public async Task<string> Handle(GetEntrepreneurHelpTypeByUserIdQuery request, CancellationToken cancellationToken)
        => (await repository.SelectAsync(i => i.UserId.Equals(request.UserId)) ?? new()).HelpType!;
}
