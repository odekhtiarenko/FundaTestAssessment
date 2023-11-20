using System.Text.Json;

namespace FundaTestAssessment.IntegrationTests.TestHelpers
{
    public static class HttpContentHelper
    {
        public static async Task<T> DeserializeAsync<T>(this HttpContent httpcontent)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<T>(await httpcontent.ReadAsStringAsync(), options);
        }
    }
}

