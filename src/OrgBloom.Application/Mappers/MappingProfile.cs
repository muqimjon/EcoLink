using AutoMapper;
using OrgBloom.Domain.Entities;
using OrgBloom.Application.Commands.Investors.CreateInvestors;

namespace OrgBloom.Application.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateInvestorCommand, Investor>();
    }
}
