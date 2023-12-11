using AutoMapper;
using OrgBloom.Domain.Entities;
using OrgBloom.Application.Commons.Interfaces;
using OrgBloom.Domain.Enums;

namespace OrgBloom.Application.Users.Commands.CreateUsers;

public record class CreateUserCommand : IRequest<int>
{
    public CreateUserCommand(CreateUserCommand command)
    {
        Phone = command.Phone;
        Email = command.Email;
        Degree = command.Degree;
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
    public int TelegramId { get; set; }
    public string LanguageCode { get; set; } = string.Empty;
}

public class CreateUserCommandHandler(IRepository<User> repository, IMapper mapper) : IRequestHandler<CreateUserCommand, int>
{
    public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.TelegramId == request.TelegramId);
        if (entity is not null)
            throw new();

        await repository.InsertAsync(mapper.Map<User>(request));
        return await repository.SaveAsync();
    }
}