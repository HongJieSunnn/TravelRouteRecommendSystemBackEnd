using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelRouteRecommendSystemBackEnd.Model;
using TravelRouteRecommendSystemBackEnd.Model.GetRouteFromCPP;
using TravelRouteRecommendSystemBackEnd.Services;

namespace TravelRouteRecommendSystemBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecommendationsController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetRecommendationRoutes([FromBody]UserRequirementFromCSharp userRequirement)
        {
            var rec = new RecomendationsRepository(userRequirement);

            var res = await rec.GetRecommendationsAsync();
            return Ok(res);
        }
    }
}
