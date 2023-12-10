using AutoMapper;
using MediatR;
using OrgBloom.Domain.Entities;
using OrgBloom.Application.Interfaces;

namespace OrgBloom.Application.Commands.ProjectManagers.UpdateProjectManagers;

public record UpdateProjectManagerCommand : IRequest<int>
{
    public UpdateProjectManagerCommand(UpdateProjectManagerCommand command)
    {
        Phone = command.Phone;
        Email = command.Email;
        Degree = command.Degree;
        Project = command.Project;
        LastName = command.LastName;
        HelpType = command.HelpType;
        CreatedAt = command.CreatedAt;
        FirstName = command.FirstName;
        TelegramId = command.TelegramId;
        Patronomyc = command.Patronomyc;
        Experience = command.Experience;
        DateOfBirth = command.DateOfBirth;
        AssetsInvested = command.AssetsInvested;
        InvestmentAmount = command.InvestmentAmount;
    }

    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Degree { get; set; } = string.Empty;
    public string Project { get; set; } = string.Empty;
    public int TelegramId { get; set; }
    public string LastName { get; set; } = string.Empty;
    public string HelpType { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string Patronomyc { get; set; } = string.Empty;
    public string Experience { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string AssetsInvested { get; set; } = string.Empty;
    public decimal InvestmentAmount { get; set; }
}

public class UpdateProjectManagerCommandHandler(IRepository<ProjectManager> repository, IMapper mapper) : IRequestHandler<UpdateProjectManagerCommand, int>
{
    public Task<int> Handle(UpdateProjectManagerCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult(0);
    }
}