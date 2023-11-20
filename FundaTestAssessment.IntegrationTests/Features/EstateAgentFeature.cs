using LightBDD.Framework.Scenarios;
using LightBDD.XUnit2;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace FundaTestAssessment.IntegrationTests.Features
{
    public partial class EstateAgentFeature : FeatureFixture,
                                              IClassFixture<WebApplicationFactory<Program>>
	{
        [Scenario]
        public async Task TopRealEstateAgents()
        {
            await Runner.AddAsyncSteps(_ => Given_CallToTopRealEstateAgentsByLocation())
                        .AddSteps(Then_Top10RealEstateAgentsShoulBeRetrivedInDescendingOrder)
                                                .RunAsync();
        }

        [Scenario]
        public async Task TopRealEstateAgentsFilter()
        {
            await Runner.AddAsyncSteps(_ => Given_CallToTopRealEstateAgentsByLocationAndFilter())
                        .AddSteps(Then_Top10RealEstateAgentsShoulBeRetrivedInDescendingOrder)
                                                .RunAsync();
        }
    }
}

