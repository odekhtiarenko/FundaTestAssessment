using AutoMapper;
using FundaTestAssessment.Api.Models;
using FundaTestAssessment.Domain.Models;

namespace FundaTestAssessment.Api.AutomapperProfile
{

    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<RealEstateAgent, RealEstateAgentStats>()
                .ForMember(dest => dest.PropertiesCount, opt => opt.MapFrom(agent => agent.Properties!.Count()));
        }
    }
}

