namespace EcoLink.Application.Users.Queries.GetUsers;

public record GetUserForApplicationQuery : IRequest<UserApplyResultDto>
{
    public GetUserForApplicationQuery(long id) { Id = id; }
    public long Id { get; set; }
}

public class GetUserForApplicationQueryHandler(IMapper mapper,
    IRepository<User> repository,
    IRepository<Investor> investorRepository,   
    IRepository<Entrepreneur> entrepreneurRepository,
    IRepository<ProjectManager> projectManagerRepository,
    IRepository<Representative> representativeRepository) : 
    IRequestHandler<GetUserForApplicationQuery, UserApplyResultDto>
{
    public async Task<UserApplyResultDto> Handle(GetUserForApplicationQuery request, CancellationToken cancellationToken)
    {
        var entity = mapper.Map<UserApplyResultDto>(await repository.SelectAsync(i => i.Id.Equals(request.Id)))
        ?? throw new NotFoundException($"User is not found with ID = {request.Id}");

        entity.Application = entity.Profession switch
        {
            UserProfession.Investor => investorRepository.SelectAsync(i => i.Id.Equals(request.Id)),
            UserProfession.Entrepreneur => entrepreneurRepository.SelectAsync(i => i.Id.Equals(request.Id)),
            UserProfession.ProjectManager => projectManagerRepository.SelectAsync(i => i.Id.Equals(request.Id)),
            UserProfession.Representative => representativeRepository.SelectAsync(i => i.Id.Equals(request.Id)),
            _ => default!
        };

        return entity;
    }
}
