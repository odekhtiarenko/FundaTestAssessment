namespace FundaTestAssessment.Domain.Models
{
	public class RealEstateAgent
	{
        public int Id { get; set; }
        public string? Name { get; set; }
        public IEnumerable<Property>? Properties { get; set; }
    }
}

