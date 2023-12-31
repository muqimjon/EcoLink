﻿namespace EcoLink.Application.Investors.Commands.UpdateInvestors;

public record UpdateInvestorIsSubmittedByUserIdCommand : IRequest<int>
{
    public UpdateInvestorIsSubmittedByUserIdCommand(UpdateInvestorIsSubmittedByUserIdCommand command)
    {
        UserId = command.UserId;
        IsSubmitted = command.IsSubmitted;
    }

    public long UserId { get; set; }
    public bool IsSubmitted { get; set; }
}

public class UpdateInvestorIsSubmittedByUserIdCommandHandler(IRepository<Investor> repository, IMapper mapper) : 
    IRequestHandler<UpdateInvestorIsSubmittedByUserIdCommand, int>
{
    public async Task<int> Handle(UpdateInvestorIsSubmittedByUserIdCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.UserId == request.UserId)
            ?? throw new NotFoundException($"Investor is not found with UserId: {request.UserId} | Update Investor IsSubmitted");

        mapper.Map(request, entity);
        repository.Update(entity);
        return await repository.SaveAsync();
    }
}