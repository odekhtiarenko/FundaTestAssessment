using Polly;
using Polly.Extensions.Http;

namespace FundaTestAssessment.Api.RetryPoliciesConfiguration
{
    public static class PollyRetryPolicies
    {
        public static void AddRetryPolicies(this IHttpClientBuilder httpBuilder)
        {
            httpBuilder.SetHandlerLifetime(TimeSpan.FromMinutes(5))
                .AddPolicyHandler(HttpPolicyExtensions
                                    .HandleTransientHttpError()
                                    .OrResult(x => x.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                                    .OrResult(x => (int)x.StatusCode >= 500)
                                    .WaitAndRetryAsync(10, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))))
                .AddPolicyHandler(HttpPolicyExtensions.HandleTransientHttpError()
                                    .OrResult(x => x.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
                                    .WaitAndRetryAsync(10, retryAttempt => TimeSpan.FromSeconds(Math.Pow(10, retryAttempt))));

        }
    }
}

