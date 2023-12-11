using AutoMapper;
using OrgBloom.Domain.Entities;
using OrgBloom.Application.Users.DTOs;
using OrgBloom.Application.Commons.Interfaces;

namespace OrgBloom.Application.Users.Queries.GetUsers;

public record GetAllUsersQuery : IRequest<IEnumerable<UserResultDto>> { }

public class GetAllUsersQueryHandler(IRepository<User> repository, IMapper mapper) : IRequestHandler<GetAllUsersQuery, IEnumerable<UserResultDto>>
{
    public async Task<IEnumerable<UserResultDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        => await Task.Run(() => mapper.Map<IEnumerable<UserResultDto>>(repository.SelectAll().ToList()));
}
