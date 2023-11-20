using Microsoft.AspNetCore.Mvc;

namespace FundaTestAssessment.Api.Controllers
{
    [Route("api/[controller]")]
    public class RealEstateAgentController : Controller
    {
        [HttpGet("{location}/top-most-active")]
        public async Task<IActionResult> GetTopActive(string location, CancellationToken cancellationToken, string? filter = null)
        {
            return Ok();
        }
    }
}

