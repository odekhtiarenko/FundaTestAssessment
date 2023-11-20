using System.Text;
using FundaTestAssessment.Domain.EstateApiClient.Models;
using Newtonsoft.Json;

namespace FundaTestAssessment.Domain.EstateApiClient
{
    public class EstateApiClient : IEstateApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private const string GET_ESTATES_FOR_SALE = "?type=koop&zo=/";

        public EstateApiClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        }

        public async Task<RealEstatesResponse> GetRealEstates(string location, int page, int pagesize, string? filter, CancellationToken token)
        {
            var httpClient = _httpClientFactory.CreateClient(ApiClientConfiguration.ApiClientName);
            var uri = GetUri(location, page, pagesize, filter);
            var result = await httpClient.GetAsync(uri, token);

            var jsonStr = await result.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<RealEstatesResponse>(jsonStr)!;
        }

        private string GetUri(string location, int page, int pagesize, string? filter)
        {
            var sb = new StringBuilder();

            sb.Append($"{GET_ESTATES_FOR_SALE}{location}/");

            if (filter != null)
                sb.Append($"{filter}/");

            sb.Append($"&page={page}&pagesize={pagesize}");

            var uri = sb.ToString();
            return uri;
        }
    }
}

