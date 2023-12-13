using AutoMapper;
using OrgBloom.Domain.Entities;
using OrgBloom.Application.Commons.Interfaces;
using OrgBloom.Application.Commons.Exceptions;

namespace OrgBloom.Application.Users.Commands.UpdateUsers;

public record UpdatePatronomycCommand : IRequest<int>
{
    public UpdatePatronomycCommand(UpdatePatronomycCommand command)
    {
        Id = command.Id;
        Patronomyc = command.Patronomyc;
    }

    public long Id { get; set; }
    public string Patronomyc { get; set; } = string.Empty;
}

public class UpdatePatronomycCommandHandler(IRepository<User> repository, IMapper mapper) : IRequestHandler<UpdatePatronomycCommand, int>
{
    public async Task<int> Handle(UpdatePatronomycCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.Id == request.Id)
            ?? throw new NotFoundException($"This User is not found by id: {request.Id} | update ptronmyc");

        mapper.Map(request, entity);
        repository.Update(entity);
        return await repository.SaveAsync();
    }
}