using Microsoft.AspNetCore.Mvc;

namespace DTBitzen.Controllers
{
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {
        [Route("/error")]
        public IActionResult GlobalErrorHandler()
        {
            return Problem();
        }
    }
}
