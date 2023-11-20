using FundaTestAssessment.Domain.EstateApiClient.Models;

namespace FundaTestAssessment.Domain.EstateApiClient
{
    public interface IEstateApiClient
	{
        Task<RealEstatesResponse> GetRealEstates(string location,
                                                 int page,
                                                 int pagesize,
                                                 string? filter,
                                                 CancellationToken token);
    }
}

