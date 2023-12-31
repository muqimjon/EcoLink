﻿namespace EcoLink.Application.Entrepreneurs.Commands.UpdateEntrepreneurs;

public record UpdateEntrepreneurRequiredFundingByUserIdCommand : IRequest<int>
{
    public UpdateEntrepreneurRequiredFundingByUserIdCommand(UpdateEntrepreneurRequiredFundingByUserIdCommand command)
    {
        UserId = command.UserId;
        RequiredFunding = command.RequiredFunding;
    }

    public long UserId { get; set; }
    public string RequiredFunding { get; set; } = string.Empty;
}

public class UpdateEntrepreneurRequiredFundingByUserIdCommandHandler(IRepository<Entrepreneur> repository, IMapper mapper) : 
    IRequestHandler<UpdateEntrepreneurRequiredFundingByUserIdCommand, int>
{
    public async Task<int> Handle(UpdateEntrepreneurRequiredFundingByUserIdCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.UserId == request.UserId)
            ?? throw new NotFoundException($"Entrepreneur is not found with id: {request.UserId} | Update Entrepreneur RequiredFunding");

        mapper.Map(request, entity);
        repository.Update(entity);
        return await repository.SaveAsync();
    }
}