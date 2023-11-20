using AutoFixture;
using AutoMapper;
using FluentAssertions;
using FundaTestAssessment.Api.AutomapperProfile;
using FundaTestAssessment.Api.Controllers;
using FundaTestAssessment.Api.Models;
using FundaTestAssessment.Domain.Models;
using FundaTestAssessment.Domain.Queries;
using FundaTestAssessment.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace FundaTestAssessment.UnitTests.ApiTests
{
	public class RealEstateAgentControllerTests
	{
        private readonly RealEstateAgentController _controller;
        private readonly Fixture _fixture;

        private readonly Mock<IMessageSender> _messageSenderMoq;

        public RealEstateAgentControllerTests()
        {
            _fixture = new Fixture();
            _messageSenderMoq = new Mock<IMessageSender>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapperProfile>();
            });

            var mapper = config.CreateMapper();

            _controller = new RealEstateAgentController(_messageSenderMoq.Object, mapper);
        }

        [Fact]
        public async Task GetTopActive_shouldOkResult()
        {
            var location = "amsterdam";
            var token = new CancellationToken();

            var result = (OkObjectResult)await _controller.GetTopActive(location, token, null);

            result.Should()
                .NotBeNull();
        }

        [Fact]
        public async Task GetTopActive_shouldReturnCollectionOf10RealEstateAgentStats()
        {
            var location = "amsterdam";
            var token = new CancellationToken();

            _messageSenderMoq.Setup(x => x.Query(It.Is<GetTopActiveRealEstateAgentsQuery>(q => q.Location == "location"), token))
                        .ReturnsAsync(_fixture.CreateMany<RealEstateAgent>());

            var result = (OkObjectResult)await _controller.GetTopActive(location, token, null);

            result.Should()
                .NotBeNull();

            result.Value.Should()
                        .BeAssignableTo<IEnumerable<RealEstateAgentStats>>();
        }
    }
}

