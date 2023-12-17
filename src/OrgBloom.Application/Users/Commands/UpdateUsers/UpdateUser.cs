using AutoMapper;
using OrgBloom.Domain.Enums;
using OrgBloom.Domain.Entities.Users;
using OrgBloom.Application.Commons.Helpers;
using OrgBloom.Application.Commons.Constants;
using OrgBloom.Application.Commons.Interfaces;
using OrgBloom.Application.Commons.Exceptions;

namespace OrgBloom.Application.Users.Commands.UpdateUsers;

public record UpdateUserCommand : IRequest<int>
{
    public UpdateUserCommand(UpdateUserCommand command)
    {
        Id = command.Id;
        Phone = command.Phone;
        Email = command.Email;
        IsBot = command.IsBot;
        Degree = command.Degree;
        ChatId = command.ChatId;
        UserName = command.UserName;
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
    public long TelegramId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string LanguageCode { get; set; } = string.Empty;
    public long? ChatId { get; set; }
    public bool IsBot { get; set; }
}

public class UpdateUserCommandHandler(IRepository<User> repository, IMapper mapper) : IRequestHandler<UpdateUserCommand, int>
{
    public async Task<int> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.Id == request.Id)
            ?? throw new NotFoundException($"This User is not found by id: {request.Id} | User update");

        mapper.Map(request, entity);
        entity.UpdatedAt = TimeHelper.GetDateTime();
        entity.DateOfBirth = request.DateOfBirth.AddHours(TimeConstants.UTC);
        repository.Update(entity);
        return await repository.SaveAsync();
    }
}