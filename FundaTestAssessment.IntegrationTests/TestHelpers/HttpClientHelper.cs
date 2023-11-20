using FluentAssertions;
using System.Net;

namespace FundaTestAssessment.IntegrationTests.TestHelpers
{
    public static class HttpClientHelper
    {
        public static async Task<HttpResponseMessage> GetWithExpectedStatusCodeAsync(this HttpClient httpClient, string url, HttpStatusCode expectedStatusCode)
        {
            var response = await httpClient.GetAsync(url);
            response.StatusCode.Should().Be(expectedStatusCode);

            return response;
        }
    }
}

