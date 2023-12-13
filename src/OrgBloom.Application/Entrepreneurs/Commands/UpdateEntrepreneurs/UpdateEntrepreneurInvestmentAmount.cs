using AutoMapper;
using OrgBloom.Domain.Entities;
using OrgBloom.Application.Commons.Interfaces;
using OrgBloom.Application.Commons.Exceptions;

namespace OrgBloom.Application.Entrepreneurs.Commands.UpdateEntrepreneurs;

public record UpdateEntrepreneurInvestmentAmountCommand : IRequest<int>
{
    public UpdateEntrepreneurInvestmentAmountCommand(UpdateEntrepreneurInvestmentAmountCommand command)
    {
        Id = command.Id;
        InvestmentAmount = command.InvestmentAmount;
    }

    public long Id { get; set; }
    public decimal? InvestmentAmount { get; set; }
}

public class UpdateEntrepreneurInvestmentAmountCommandHandler(IRepository<Entrepreneur> repository, IMapper mapper) : IRequestHandler<UpdateEntrepreneurInvestmentAmountCommand, int>
{
    public async Task<int> Handle(UpdateEntrepreneurInvestmentAmountCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.Id == request.Id)
            ?? throw new NotFoundException($"Entrepreneur is not found with id: {request.Id} | Update Entrepreneur InvestmentAmount");

        mapper.Map(request, entity);
        repository.Update(entity);
        return await repository.SaveAsync();
    }
}