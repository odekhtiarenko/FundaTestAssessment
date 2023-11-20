using AutoMapper;
using FundaTestAssessment.Api.Models;
using FundaTestAssessment.Domain.Queries;
using FundaTestAssessment.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace FundaTestAssessment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RealEstateAgentController : Controller
    {
        private readonly IMessageSender _messageSender;
        private readonly IMapper _mapper;

        public RealEstateAgentController(IMessageSender messageSender, IMapper mapper)
        {
            _messageSender = messageSender ?? throw new ArgumentNullException(nameof(messageSender));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("{location}/top-most-active")]
        public async Task<IActionResult> GetTopActive(string location, CancellationToken cancellationToken, string? filter = null)
        {
            var realEstateAgents = await _messageSender.Query(new GetTopActiveRealEstateAgentsQuery(location, filter), cancellationToken);
            return Ok(_mapper.Map<IEnumerable<RealEstateAgentStats>>(realEstateAgents));
        }
    }
}

