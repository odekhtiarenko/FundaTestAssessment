using FluentAssertions;
using FundaTestAssessment.Api.Models;
using FundaTestAssessment.IntegrationTests.TestHelpers;
using Microsoft.AspNetCore.Mvc.Testing;

namespace FundaTestAssessment.IntegrationTests.Features
{
	public partial class EstateAgentFeature
	{
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _httpClient;

        private readonly string _location = "amsterdam";
        private readonly string _filter = "tuin";

        private IEnumerable<RealEstateAgentStats>? _topAgents;

        public EstateAgentFeature(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _httpClient = _factory.CreateClient();
        }

        private async Task Given_CallToTopRealEstateAgentsByLocation()
        {
            var repsonse = await _httpClient.GetWithExpectedStatusCodeAsync($"/api/RealEstateAgent/{_location}/top-most-active", System.Net.HttpStatusCode.OK);
            _topAgents = await repsonse.Content.DeserializeAsync<IEnumerable<RealEstateAgentStats>>();
        }

        private async Task Given_CallToTopRealEstateAgentsByLocationAndFilter()
        {
            var repsonse = await _httpClient.GetWithExpectedStatusCodeAsync($"/api/RealEstateAgent/{_location}/top-most-active?filter={_filter}", System.Net.HttpStatusCode.OK);
            _topAgents = await repsonse.Content.DeserializeAsync<IEnumerable<RealEstateAgentStats>>();
        }

        private void Then_Top10RealEstateAgentsShoulBeRetrivedInDescendingOrder()
        {
            _topAgents.Should()
                      .BeInDescendingOrder(x => x.PropertiesCount)
                      .And
                      .HaveCount(10);
        }
    }
}

