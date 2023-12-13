using AutoMapper;
using OrgBloom.Domain.Entities;
using OrgBloom.Application.Commons.Interfaces;
using OrgBloom.Application.Commons.Exceptions;

namespace OrgBloom.Application.Investors.Commands.UpdateInvestors;

public record UpdateInvestorSectorCommand : IRequest<int>
{
    public UpdateInvestorSectorCommand(UpdateInvestorSectorCommand command)
    {
        Id = command.Id;
        Sector = command.Sector;
    }

    public long Id { get; set; }
    public string Sector { get; set; } = string.Empty;
}

public class UpdateInvestorSectorCommandHandler(IRepository<Investor> repository, IMapper mapper) : IRequestHandler<UpdateInvestorSectorCommand, int>
{
    public async Task<int> Handle(UpdateInvestorSectorCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.Id == request.Id)
            ?? throw new NotFoundException($"Investor is not found with UserId: {request.Id} | Update Investor Sector");

        mapper.Map(request, entity);
        repository.Update(entity);
        return await repository.SaveAsync();
    }
}