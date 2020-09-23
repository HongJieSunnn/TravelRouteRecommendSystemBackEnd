using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TravelRouteRecommendSystemBackEnd.Controllers
{
    [ApiController]
    public class ErrorController : ControllerBase
    {
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
                        //TODO:日志
                        return Problem(title: "出现了无法解决的问题，请联系工作人员 There is an unresolved problem.Please contact staff(programmer)");
                    case 2:
                        //TODO:日志
                        return BadRequest(new { Error = "输入的参数错误,或其它难以预料的错误 Input parameter error or some other unexpected errors occur" });
                    case 3:
                        return Ok(new { Error = error.Message });
                    case 4:
                        Response.StatusCode = 500;
                        return Problem(title: "StatusCode:500.出现了未知的错误 Unknown Error Occurs");
                }
            }
            Response.StatusCode = 500;
            //TODO:记录日志
            return Problem(title: "StatusCode:500.出现了未知的错误 Unknown Error Occurs");
        }
    }
}