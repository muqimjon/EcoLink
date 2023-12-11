using AutoMapper;
using OrgBloom.Domain.Entities;
using OrgBloom.Application.Commons.Interfaces;
using OrgBloom.Domain.Enums;

namespace OrgBloom.Application.Users.Commands.UpdateUsers;

public record UpdateUserCommand : IRequest<int>
{
    public UpdateUserCommand(UpdateUserCommand command)
    {
        Id = command.Id;
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

    public long Id { get; set; }
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

public class UpdateUserCommandHandler(IRepository<User> repository, IMapper mapper) : IRequestHandler<UpdateUserCommand, int>
{
    public async Task<int> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.Id == request.Id)
            ?? throw new();

        mapper.Map(request, entity);
        repository.Update(entity);
        return await repository.SaveAsync();
    }
}