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
        public RecommendationsController(ILogger<RecomendationsRepository> logger)
        {
            _logger = logger;
        }
        [HttpPost]
        public async Task<IActionResult> GetRecommendationRoutes([FromBody] UserRequirement userRequirement)
        {
            _logger.LogInformation($"\n输入:{userRequirement}");
            UserRequirementFromCSharp userRequirementFromCSharp = new UserRequirementFromCSharp();
            userRequirement.UserRequirementToUserRequirementFromCSharp(ref userRequirementFromCSharp);
            var rec = new RecomendationsRepository(userRequirementFromCSharp);

            var res = await rec.GetRecommendationsAsync();


            return Ok(res);
        }
    }
}
