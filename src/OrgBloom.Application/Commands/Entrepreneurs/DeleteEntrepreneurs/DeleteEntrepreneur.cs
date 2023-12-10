﻿using MediatR;
using AutoMapper;
using OrgBloom.Domain.Entities;
using OrgBloom.Application.Interfaces;

namespace OrgBloom.Application.Commands.Entrepreneurs.DeleteEntrepreneurs;

public record DeleteEntrepreneurCommand : IRequest<bool>
{
    public DeleteEntrepreneurCommand(DeleteEntrepreneurCommand command) { Id = command.Id; }
    public long Id { get; set; }
}

public class DeleteEntrepreneurCommandHandler(IRepository<Entrepreneur> repository) : IRequestHandler<DeleteEntrepreneurCommand, bool>
{
    public async Task<bool> Handle(DeleteEntrepreneurCommand request, CancellationToken cancellationToken)
    {
        repository.Delete(x => x.Id == request.Id);
        return await repository.SaveAsync() > 0;
    }
}