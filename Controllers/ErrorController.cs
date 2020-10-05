using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TravelRouteRecommendSystemBackEnd.Model.GetRouteFromCPP;

namespace TravelRouteRecommendSystemBackEnd.Controllers
{
    [ApiController]
    public class ErrorController : ControllerBase
    {
        private readonly ILogger<ErrorController> _logger;
        public ErrorController(ILogger<ErrorController> logger,IUserRequirementFromCSharp userRequirement)
        {
            _logger = logger;
        }

        [Route("/error")]
        public IActionResult Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var error = context.Error;
            if (error.Data.Contains("level"))
            {
                switch (error.Data["level"])
                {
                    case 1:
                        _logger.LogCritical($"\n错误:{{error_level:{1} \nerror_code:{error.Data["error_code"]} \nerror:{error.Message}}}\n");
                        return new ObjectResult(new { Error = "出现了无法解决的问题，请联系工作人员 There is an unresolved problem.Please contact staff(programmer)" });
                    case 2:
                        _logger.LogError($"\n错误:{{error_level:{2} \nerror_code:{error.Data["error_code"]} \nerror:{error.Message}}}\n");
                        return BadRequest(new { Error = "输入的参数错误,或其它难以预料的错误 Input parameter error or some other unexpected errors occur" });
                    case 3:
                        _logger.LogWarning($"\n错误:{{error_level:{3} \nerror_code:{error.Data["error_code"]} \nerror:{error.Message}}}\n");
                        return Ok(new { Error = error.Message });
                    case 4:
                        _logger.LogCritical($"\n错误:{{error_level:{4} \nerror_code:{error.Data["error_code"]} \nerror:{error.Message}}}\n");
                        return new ObjectResult(new { Error = "出现了未知的错误 Unknown Error Occurs" });
                }
            }
            _logger.LogCritical($"\n错误:{{error_level:{1} \nerror_code:{error.Data["error_code"]} \nerror:{error.Message}}}\n");
            return new ObjectResult(new { Error = "出现了未知的错误 Unknown Error Occurs" });
        }
    }
}