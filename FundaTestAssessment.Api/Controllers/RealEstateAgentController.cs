using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace FundaTestAssessment.Api.Controllers
{
    [Route("api/[controller]")]
    public class RealEstateAgentController : Controller
    {
        private readonly IMapper _mapper;

        public RealEstateAgentController(IMapper mapper)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));;
        }

        [HttpGet("{location}/top-most-active")]
        public async Task<IActionResult> GetTopActive(string location, CancellationToken cancellationToken, string? filter = null)
        {
            return Ok();
        }
    }
}

