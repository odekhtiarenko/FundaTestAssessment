using FundaTestAssessment.Domain.EstateApiClient;
using FundaTestAssessment.Domain.Models;
using FundaTestAssessment.Domain.Queries;
using MediatR;

using EstateApiProperty = FundaTestAssessment.Domain.EstateApiClient.Models.Property;


namespace FundaTestAssessment.Domain.QueryHandlers
{
    public class GetTopActiveRealEstateAgentsQueryHandler : IRequestHandler<GetTopActiveRealEstateAgentsQuery, IEnumerable<RealEstateAgent>>
    {
        private readonly IEstateApiClient _httpClient;

        public GetTopActiveRealEstateAgentsQueryHandler(IEstateApiClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<RealEstateAgent>> Handle(GetTopActiveRealEstateAgentsQuery request, CancellationToken cancellationToken)
        {
            var result = new List<EstateApiProperty>();

            var pages = 1;
            var pageSize = 25;
            var top = 10;

            for (int page = 1; page <= pages; page++)
            {
                cancellationToken.ThrowIfCancellationRequested();

                var repsonse = await _httpClient.GetRealEstates(request.Location, page, pageSize, request.Filter, cancellationToken);
                result.AddRange(repsonse.Objects!);

                pages = repsonse.Paging!.AantalPaginas;
            }

            return result.GroupBy(x => x.MakelaarId,
                                  (id, properies) => new RealEstateAgent()
                                  {
                                      Id = id,
                                      Name = properies.First().MakelaarNaam,
                                      Properties = properies.Select(x => new Property
                                      {
                                          Address = x.Adres,
                                          OfferedSince = x.AangebodenSindsTekst
                                      })
                                  })
                            .OrderByDescending(x => x.Properties!.Count())
                            .Take(top);
        }
    }
}

