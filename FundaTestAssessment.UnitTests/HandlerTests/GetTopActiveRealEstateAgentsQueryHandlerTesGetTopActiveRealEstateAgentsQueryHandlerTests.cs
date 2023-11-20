using AutoFixture;
using FluentAssertions;
using FundaTestAssessment.Domain.EstateApiClient;
using FundaTestAssessment.Domain.EstateApiClient.Models;
using FundaTestAssessment.Domain.Models;
using FundaTestAssessment.Domain.Queries;
using FundaTestAssessment.Domain.QueryHandlers;
using Moq;

namespace FundaTestAssessment.UnitTests.HandlerTests
{
    public class GetTopActiveRealEstateAgentsQueryHandlerTests
    {

        private readonly GetTopActiveRealEstateAgentsQueryHandler _handler;
        private readonly IFixture _fixture;
        private readonly Mock<IEstateApiClient> _httpClientMoq;

        public GetTopActiveRealEstateAgentsQueryHandlerTests()
        {
            _fixture = new Fixture();
            _httpClientMoq = new Mock<IEstateApiClient>();

            _handler = new GetTopActiveRealEstateAgentsQueryHandler(_httpClientMoq.Object);
        }

        [Theory]
        [InlineData("amsterdam", null)]
        [InlineData("amsterdam", "Tuin")]
        public async Task Handle_ShouldReturnCollectionOfTopActiveRealEstateAgents(string location, string filter)
        {
            var response = _fixture.Create<RealEstatesResponse>();
            var token = new CancellationToken();

            _httpClientMoq.Setup(x => x.GetRealEstates(location, It.IsAny<int>(), It.IsAny<int>(), filter, token))
                .ReturnsAsync(response);

            var result = await _handler.Handle(new GetTopActiveRealEstateAgentsQuery(location, filter), token)!;

            result.Should()
                .BeAssignableTo<IEnumerable<RealEstateAgent>>();

            for (int i = 0; i < result.Count() - 1; i++)
            {
                result.ElementAt(i).Properties.Count().Should()
                    .BeGreaterOrEqualTo(result.ElementAt(i + 1).Properties.Count());
            }
        }
    }
}

