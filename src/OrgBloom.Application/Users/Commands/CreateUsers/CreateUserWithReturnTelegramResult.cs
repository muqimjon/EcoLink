using AutoMapper;
using OrgBloom.Domain.Enums;
using OrgBloom.Domain.Entities;
using OrgBloom.Application.Users.DTOs;
using OrgBloom.Application.Commons.Interfaces;

namespace OrgBloom.Application.Users.Commands.CreateUsers;

public record class CreateUserWithReturnTgResultCommand : IRequest<UserTelegramResultDto>
{
    public CreateUserWithReturnTgResultCommand(CreateUserWithReturnTgResultCommand command)
    {
        Phone = command.Phone;
        Email = command.Email;
        IsBot = command.IsBot;
        State = command.State;
        Degree = command.Degree;
        ChatId = command.ChatId;
        Username = command.Username;
        LastName = command.LastName;
        FirstName = command.FirstName;
        TelegramId = command.TelegramId;
        Patronomyc = command.Patronomyc;
        Profession = command.Profession;
        DateOfBirth = command.DateOfBirth;
        LanguageCode = command.LanguageCode;
    }

    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Patronomyc { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string Degree { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public UserProfession Profession { get; set; }
    public long TelegramId { get; set; }
    public string Username { get; set; } = string.Empty;
    public string LanguageCode { get; set; } = string.Empty;
    public long ChatId { get; set; }
    public bool IsBot { get; set; }
    public UserState State { get; set; }
}

public class CreateUserWithReturnTgResultCommandHandler(IRepository<User> repository, IMapper mapper) : IRequestHandler<CreateUserWithReturnTgResultCommand, UserTelegramResultDto>
{
    public async Task<UserTelegramResultDto> Handle(CreateUserWithReturnTgResultCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.TelegramId == request.TelegramId);
        if (entity is not null)
            return default!;

        var user = mapper.Map<User>(request);
        await repository.InsertAsync(user);
        await repository.SaveAsync();

        return mapper.Map<UserTelegramResultDto>(user);
    }
}