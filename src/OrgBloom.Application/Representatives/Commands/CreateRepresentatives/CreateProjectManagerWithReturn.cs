using AutoMapper;
using OrgBloom.Domain.Entities;
using OrgBloom.Application.Commons.Interfaces;
using OrgBloom.Application.Commons.Exceptions;
using OrgBloom.Application.Representatives.DTOs;

namespace OrgBloom.Application.Representatives.Commands.CreateRepresentatives;

public record CreateRepresentativeWithReturnCommand : IRequest<RepresentativeResultDto>
{
    public CreateRepresentativeWithReturnCommand(CreateRepresentativeWithReturnCommand command)
    {
        Area = command.Area;
        UserId = command.UserId;
        Purpose = command.Purpose;
        Address = command.Address;
        Languages = command.Languages;
        Experience = command.Experience;
        Expectation = command.Expectation;
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

public class CreateRepresentativeWithReturnCommandHandler(IRepository<Representative> repository, IMapper mapper) : IRequestHandler<CreateRepresentativeWithReturnCommand, RepresentativeResultDto>
{
    public async Task<RepresentativeResultDto> Handle(CreateRepresentativeWithReturnCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.UserId == request.UserId);
        if (entity is not null)
            throw new AlreadyExistException($"Representative is already exist create Representative by user id {request.UserId}");

        entity = mapper.Map<Representative>(request);
        await repository.InsertAsync(entity);
        await repository.SaveAsync();

        return mapper.Map<RepresentativeResultDto>(entity);
    }
}