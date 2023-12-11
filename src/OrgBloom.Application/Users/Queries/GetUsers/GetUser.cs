using AutoMapper;
using OrgBloom.Domain.Entities;
using OrgBloom.Application.Users.DTOs;
using OrgBloom.Application.Commons.Interfaces;

namespace OrgBloom.Application.Users.Queries.GetUsers;

public record GetUserQuery : IRequest<UserResultDto>
{
    public GetUserQuery(GetUserQuery command) { Id = command.Id; }
    public int Id { get; set; }
}

public class GetUserQueryHendler(IRepository<User> repository, IMapper mapper) : IRequestHandler<GetUserQuery, UserResultDto>
{
    public async Task<UserResultDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
        => mapper.Map<UserResultDto>(await repository.SelectAsync(i => i.Id.Equals(request.Id)));
}
