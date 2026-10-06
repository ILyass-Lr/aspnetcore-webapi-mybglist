using Microsoft.AspNetCore.Mvc;

namespace MyBGList.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProblemsDemoController : ControllerBase
{
    public static class Types
    {
        public const string RateLimited = "https://api.mybglist.com/scalar#rate-limited";
        public const string StaleResource = "https://api.mybglist.com/scalar#stale-resource";
    }

    [HttpGet("rate-limited")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status429TooManyRequests)]
    public IActionResult TriggerRateLimited()
    {
        var problem = new ProblemDetails
        {
            Type = Types.RateLimited,
            Title = "Too many requests.",
            Detail = "You've exceeded the allowed request rate for this endpoint.",
            Status = StatusCodes.Status429TooManyRequests,
        };
        problem.Extensions["retryAfterSeconds"] = 30;
        problem.Extensions["limit"] = 100;
        problem.Extensions["windowSeconds"] = 60;

        return StatusCode(StatusCodes.Status429TooManyRequests, problem);
    }

    [HttpGet("stale-resource")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    public IActionResult TriggerStaleResource()
    {
        var problem = new ProblemDetails
        {
            Type = Types.StaleResource,
            Title = "Resource has been modified since you last fetched it.",
            Detail = "Your update was rejected because the resource changed on the server after you loaded it.",
            Status = StatusCodes.Status409Conflict,
        };
        problem.Extensions["clientVersion"] = 3;
        problem.Extensions["serverVersion"] = 5;

        return Conflict(problem);
    }
}

