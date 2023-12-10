using AutoMapper;
using OrgBloom.Domain.Entities;
using OrgBloom.Application.DTOs.Investors;
using OrgBloom.Application.Commands.Investors.CreateInvestors;
using OrgBloom.Application.Commands.Investors.UpdateInvestors;

namespace OrgBloom.Application.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Investor
        CreateMap<CreateInvestorCommand, Investor>();
        CreateMap<UpdateInvestorCommand, Investor>();
        CreateMap<Investor, InvestorResultDto>();


    }
}
