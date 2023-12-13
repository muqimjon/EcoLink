using AutoMapper;
using OrgBloom.Domain.Entities;
using OrgBloom.Application.Users.DTOs;
using OrgBloom.Application.Commons.Interfaces;

namespace OrgBloom.Application.Users.Queries.GetUsers;

public record GetAllUsersQuery : IRequest<IEnumerable<UserApplyResultDto>> { }

public class GetAllUsersQueryHandler(IRepository<User> repository, IMapper mapper) : IRequestHandler<GetAllUsersQuery, IEnumerable<UserApplyResultDto>>
{
    public async Task<IEnumerable<UserApplyResultDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        => await Task.Run(() => mapper.Map<IEnumerable<UserApplyResultDto>>(repository.SelectAll().ToList()));
}
