using Microsoft.AspNetCore.Mvc;

namespace MyBGList.Controllers
{
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [Route("/error")]  // <-| The HTTP route 
        [HttpGet]          //   | to handle
        public IActionResult Error()
        {
            return Problem();  // <-| The HTTP response 
        }                    //   | to return to the caller
    }
}