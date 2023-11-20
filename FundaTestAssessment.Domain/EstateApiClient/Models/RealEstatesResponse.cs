using FundaTestAssessment.Domain.Models;

namespace FundaTestAssessment.Domain.EstateApiClient.Models
{
    public class RealEstatesResponse
    {
        public IEnumerable<Property>? Objects { get; set; }
        public Paging? Paging { get; set; }
    }
}

