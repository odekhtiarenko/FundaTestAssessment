using AutoFixture;
using AutoMapper;
using FluentAssertions;
using FundaTestAssessment.Api.AutomapperProfile;
using FundaTestAssessment.Api.Models;
using FundaTestAssessment.Domain.Models;

namespace FundaTestAssessment.UnitTests.MapperProfileTests
{
    public class MapperProfileTests
    {
        private readonly IMapper _mapper;
        private readonly IFixture _fixture;

        public MapperProfileTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapperProfile>();
            });

            _mapper = config.CreateMapper();

            _fixture = new Fixture();
        }

        [Fact]
        public void Map_RealEstateAgent_To_RealEstateAgentStats()
        {
            var input = _fixture.Create<RealEstateAgent>();

            var expected = new RealEstateAgentStats();
            expected.Id = input.Id;
            expected.Name = input.Name;
            expected.PropertiesCount = input.Properties!.Count();

            var result = _mapper.Map<RealEstateAgentStats>(input);

            result.Should()
                  .BeEquivalentTo(expected);
        }

        [Fact]
        public void Map_ListRealEstateAgent_To_ListRealEstateAgentStats()
        {
            var inputAgent = _fixture.Create<RealEstateAgent>();

            var input = new List<RealEstateAgent> { inputAgent, inputAgent, inputAgent };

            var expectedAgentStats = new RealEstateAgentStats();
            expectedAgentStats.Id = inputAgent.Id;
            expectedAgentStats.Name = inputAgent.Name;
            expectedAgentStats.PropertiesCount = inputAgent.Properties!.Count();

            var expected = new List<RealEstateAgentStats> { expectedAgentStats, expectedAgentStats, expectedAgentStats };

            var result = _mapper.Map<IEnumerable<RealEstateAgentStats>>(input);

            result.Should()
                  .BeEquivalentTo(expected);
        }
    }

}

