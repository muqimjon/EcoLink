using AutoMapper;
using OrgBloom.Application.Queries.GetInvestors;
using OrgBloom.Domain.Entities;

namespace OrgBloom.Application.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<InvestorGetAllCommand, Investor>();
    }
}
