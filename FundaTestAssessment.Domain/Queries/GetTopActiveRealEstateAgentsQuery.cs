using FundaTestAssessment.Domain.Models;
using MediatR;

namespace FundaTestAssessment.Domain.Queries
{
    public class GetTopActiveRealEstateAgentsQuery : IRequest<IEnumerable<RealEstateAgent>>
    {
        public string Location { get; }
        public string? Filter { get; }

        public GetTopActiveRealEstateAgentsQuery(string location, string? filter)
        {
            Location = location;
            Filter = filter;
        }
    }
}

