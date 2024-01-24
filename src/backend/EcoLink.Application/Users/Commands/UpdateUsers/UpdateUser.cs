using EcoLink.Application.Investors.Commands.UpdateInvestors;
using EcoLink.Application.Entrepreneurs.Commands.UpdateEntrepreneurs;
using EcoLink.Application.ProjectManagers.Commands.UpdateProjectManagers;
using EcoLink.Application.Representatives.Commands.UpdateRepresentatives;

namespace EcoLink.Application.Users.Commands.UpdateUsers;

public record UpdateUserCommand : IRequest<int>
{
    public UpdateUserCommand(UpdateUserCommand command)
    {
        Id = command.Id;
        Age = command.Age;
        Phone = command.Phone;
        Email = command.Email;
        State = command.State;
        IsBot = command.IsBot;
        Degree = command.Degree;
        ChatId = command.ChatId;
        Address = command.Address;
        Username = command.Username;
        LastName = command.LastName;
        Languages = command.Languages;
        FirstName = command.FirstName;
        Experience = command.Experience;
        TelegramId = command.TelegramId;
        Patronomyc = command.Patronomyc;
        Profession = command.Profession;
        DateOfBirth = command.DateOfBirth;
        Application = command.Application;
        LanguageCode = command.LanguageCode;
    }

    public long Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Patronomyc { get; set; } = string.Empty;
    public string Age { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string Degree { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Languages { get; set; } = string.Empty;
    public string Experience { get; set; } = string.Empty;
    public UserProfession Profession { get; set; }
    public State State { get; set; }
    public long TelegramId { get; set; }
    public string Username { get; set; } = string.Empty;
    public string LanguageCode { get; set; } = string.Empty;
    public long ChatId { get; set; }
    public bool IsBot { get; set; }
    public dynamic Application { get; set; } = default!;
}

public class UpdateUserCommandHandler(IMapper mapper,
    IRepository<User> repository,
    IMediator mediator) : 
    IRequestHandler<UpdateUserCommand, int>
{
    public async Task<int> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.Id == request.Id)
            ?? throw new NotFoundException($"This User is not found by id: {request.Id} | User update");

        await (request.Profession switch
        {
            UserProfession.Investor => mediator.Send(new UpdateInvestorCommand(request.Application), cancellationToken),
            UserProfession.Entrepreneur => mediator.Send(new UpdateEntrepreneurCommand(request.Application), cancellationToken),
            UserProfession.Representative => mediator.Send(new UpdateRepresentativeCommand(request.Application), cancellationToken),
            UserProfession.ProjectManager => mediator.Send(new UpdateProjectManagerCommand(request.Application), cancellationToken),
            _ => default!
        });

        mapper.Map(request, entity);
        entity.DateOfBirth = request.DateOfBirth.AddHours(TimeConstants.UTC);
        repository.Update(entity);
        return await repository.SaveAsync();
    }
}