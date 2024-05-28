using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace TeamManagementProject_Backend.Controllers
{
    [ApiController]
    public class ErrorHandler : ControllerBase
    {
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("/error")]
        public IActionResult HandleError()
        {
            var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            // Get the exception that occurred
            Exception exceptionThatOccurred = exceptionFeature.Error;

            return Problem(title: exceptionThatOccurred.Message);
        }
    }
}
