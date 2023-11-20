using AutoFixture;
using AutoMapper;
using FluentAssertions;
using FundaTestAssessment.Api.AutomapperProfile;
using FundaTestAssessment.Api.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace FundaTestAssessment.UnitTests.ApiTests
{
	public class RealEstateAgentControllerTests
	{
        private readonly RealEstateAgentController _controller;
        private readonly Fixture _fixture;

        public RealEstateAgentControllerTests()
		{
            _fixture = new Fixture();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapperProfile>();
            });

            var mapper = config.CreateMapper();

            _controller = new RealEstateAgentController( mapper);
        }

        [Fact]
        public async Task GetTopActive_shouldOkResult()
        {
            var location = "amsterdam";
            var token = new CancellationToken();

            var result = (OkResult)await _controller.GetTopActive(location, token, null);

            result.Should()
                .NotBeNull();
        }
    }
}

