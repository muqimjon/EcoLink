using EcoLink.Application.Investors.DTOs;
using EcoLink.Application.Entrepreneurs.DTOs;
using EcoLink.Application.ProjectManagers.DTOs;
using EcoLink.Application.Representatives.DTOs;

namespace EcoLink.Application.Users.Queries.GetUsers;

public record GetUserByTelegramIdQuery : IRequest<UserResultDto>
{
    public GetUserByTelegramIdQuery(long telegramId) { TelegramId = telegramId; }
    public long TelegramId { get; set; }
}

public class GetUserByTelegramIdQueryHandler(IMapper mapper,
    IRepository<User> repository,
    IRepository<Investor> investorRepository,
    IRepository<Entrepreneur> entrepreneurRepository,
    IRepository<Representative> representativeRepository,
    IRepository<ProjectManager> projectManagerRepository) : 
    IRequestHandler<GetUserByTelegramIdQuery, UserResultDto>
{
    public async Task<UserResultDto> Handle(GetUserByTelegramIdQuery request, CancellationToken cancellationToken)
    {
        var resultDto = mapper.Map<UserResultDto>(await repository.SelectAsync(i => i.TelegramId == request.TelegramId))
            ?? throw new NotFoundException($"User is not found with Telegram ID = {request.TelegramId}");

        switch(resultDto.Profession)
        {
            case UserProfession.Investor:
                resultDto.Application = mapper.Map<InvestorResultDto>(await investorRepository.SelectAsync(i => i.UserId == resultDto.Id));
                break;
            case UserProfession.Entrepreneur:
                resultDto.Application = mapper.Map<EntrepreneurResultDto>(await entrepreneurRepository.SelectAsync(i => i.UserId == resultDto.Id));
                break;
            case UserProfession.Representative:
                resultDto.Application = mapper.Map<RepresentativeResultDto>(await representativeRepository.SelectAsync(i => i.UserId == resultDto.Id));
                break;
            case UserProfession.ProjectManager:
                resultDto.Application = mapper.Map<ProjectManagerResultDto>(await projectManagerRepository.SelectAsync(i => i.UserId == resultDto.Id));
                break;
        };
        return resultDto;
    }
}
