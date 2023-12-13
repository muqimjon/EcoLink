using AutoMapper;
using OrgBloom.Domain.Entities;
using OrgBloom.Application.Commons.Interfaces;
using OrgBloom.Application.Commons.Exceptions;

namespace OrgBloom.Application.Languagess.Commands.CreateLanguagess;

public record CreateLanguagesCommand : IRequest<int>
{
    public CreateLanguagesCommand(CreateLanguagesCommand command)
    {
        Area = command.Area;
        Purpose = command.Purpose;
        Address = command.Address;
        Languages = command.Languages;
        Experience = command.Experience;
        Expectation = command.Expectation;
        UserId = command.UserId;
        IsSubmitted = command.IsSubmitted;
    }

    public string Languages { get; set; } = string.Empty;
    public string Experience { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Area { get; set; } = string.Empty;
    public string Expectation { get; set; } = string.Empty;
    public string Purpose { get; set; } = string.Empty;
    public long UserId { get; set; }
    public bool IsSubmitted { get; set; }
}

public class CreateLanguagesCommandHandler(IRepository<Representative> repository, IMapper mapper) : IRequestHandler<CreateLanguagesCommand, int>
{
    public async Task<int> Handle(CreateLanguagesCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.UserId == request.UserId);
        if (entity is not null)
            throw new AlreadyExistException($"Languages is already exist with user id: {request.UserId} | update representative");

        await repository.InsertAsync(mapper.Map<Representative>(request));
        return await repository.SaveAsync();
    }
}