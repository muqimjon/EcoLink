using AutoMapper;
using OrgBloom.Domain.Enums;
using OrgBloom.Domain.Entities;
using OrgBloom.Application.Commons.Interfaces;

namespace OrgBloom.Application.Users.Commands.CreateUsers;

public record class CreateUserCommand : IRequest<int>
{
    public CreateUserCommand(CreateUserCommand command)
    {
        Phone = command.Phone;
        Email = command.Email;
        IsBot = command.IsBot;
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
        State = command.State;
        Address = command.Address;
        Languages = command.Languages;
        Experience = command.Experience;
    }

    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Patronomyc { get; set; } = string.Empty;
    public DateTimeOffset DateOfBirth { get; set; }
    public string Degree { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public UserProfession Profession { get; set; }
    public string Address { get; set; } = string.Empty;
    public string Languages { get; set; } = string.Empty;
    public string Experience { get; set; } = string.Empty;
    public long TelegramId { get; set; }
    public string Username { get; set; } = string.Empty;
    public string LanguageCode { get; set; } = string.Empty;
    public long ChatId { get; set; }
    public bool IsBot { get; set; }
    public State State { get; set; }
}

public class CreateUserCommandHandler(IRepository<User> repository, IMapper mapper) : IRequestHandler<CreateUserCommand, int>
{
    public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.TelegramId == request.TelegramId);
        if (entity is not null)
            throw new($"User Already exist user command create with telegram id: {request.TelegramId} | create user");

        await repository.InsertAsync(mapper.Map<User>(request));
        return await repository.SaveAsync();
    }
}