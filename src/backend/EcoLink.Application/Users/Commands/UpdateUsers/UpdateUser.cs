namespace EcoLink.Application.Users.Commands.UpdateUsers;

public record UpdateUserCommand : IRequest<int>
{
    public UpdateUserCommand(UpdateUserCommand command)
    {
        Id = command.Id;
        Age = command.Age;
        Area = command.Area;
        Phone = command.Phone;
        Email = command.Email;
        IsBot = command.IsBot;
        State = command.State;
        Degree = command.Degree;
        ChatId = command.ChatId;
        Sector = command.Sector;
        Address = command.Address;
        Address = command.Address;
        Project = command.Project;
        Purpose = command.Purpose;
        Username = command.Username;
        LastName = command.LastName;
        HelpType = command.HelpType;
        MessageId = command.MessageId;
        Languages = command.Languages;
        FirstName = command.FirstName;
        TelegramId = command.TelegramId;
        Patronomyc = command.Patronomyc;
        Experience = command.Experience;
        Profession = command.Profession;
        DateOfBirth = command.DateOfBirth;
        Expectation = command.Expectation;
        LanguageCode = command.LanguageCode;
        AssetsInvested = command.AssetsInvested;
        RequiredFunding = command.RequiredFunding;
        InvestmentAmount = command.InvestmentAmount;
        ProjectDirection = command.ProjectDirection;
    }

    public long Id { get; set; }
    public long TelegramId { get; set; }
    public string Username { get; set; } = string.Empty;
    public string LanguageCode { get; set; } = string.Empty;
    public long ChatId { get; set; }
    public bool IsBot { get; set; }
    public int MessageId { get; set; }
    public State State { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Patronomyc { get; set; } = string.Empty;
    public DateTimeOffset DateOfBirth { get; set; }
    public string Age { get; set; } = string.Empty;
    public string Degree { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public UserProfession Profession { get; set; }
    public string Address { get; set; } = string.Empty;
    public string Languages { get; set; } = string.Empty;
    public string Experience { get; set; } = string.Empty;
    public string Sector { get; set; } = string.Empty;
    public string Project { get; set; } = string.Empty;
    public string HelpType { get; set; } = string.Empty;
    public string RequiredFunding { get; set; } = string.Empty;
    public string AssetsInvested { get; set; } = string.Empty;
    public string InvestmentAmount { get; set; } = string.Empty;
    public string ProjectDirection { get; set; } = string.Empty;
    public string Expectation { get; set; } = string.Empty;
    public string Purpose { get; set; } = string.Empty;
    public string Area { get; set; } = string.Empty;
}

public class UpdateUserCommandHandler(IMapper mapper,
    IRepository<User> repository) : 
    IRequestHandler<UpdateUserCommand, int>
{
    public async Task<int> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.Id == request.Id)
            ?? throw new NotFoundException($"This User is not found by id: {request.Id} | User update");

        mapper.Map(request, entity);
        entity.DateOfBirth = request.DateOfBirth.AddHours(TimeConstants.UTC);

        repository.Update(entity);
        return await repository.SaveAsync();
    }
}