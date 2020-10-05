using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TravelRouteRecommendSystemBackEnd.Model.GetRouteFromCPP;
using TravelRouteRecommendSystemBackEnd.Services;

namespace TravelRouteRecommendSystemBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecommendationsController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IHttpClientFactory _httpClient;
        public RecommendationsController(ILogger<RecomendationsRepository> logger,IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClient = httpClientFactory;
        }
        [HttpPost]
        public async Task<IActionResult> GetRecommendationRoutes([FromBody] UserRequirement userRequirement)
        {
            _logger.LogInformation($"\n输入:{userRequirement}");
            var userRequirementFromCSharp = await userRequirement.UserRequirementToUserRequirementFromCSharp(_httpClient);

            var rec = new RecomendationsRepository(userRequirementFromCSharp,_httpClient);
            var res = await rec.GetRecommendationsAsync();

            return Ok(res);
        }
    }
}
